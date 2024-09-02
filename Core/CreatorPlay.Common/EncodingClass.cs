using WebApi_VivoValoriza.BouncyCrypto.Crypto;
using WebApi_VivoValoriza.BouncyCrypto.Crypto.Encodings;
using WebApi_VivoValoriza.BouncyCrypto.Crypto.Engines;
using Microsoft.Extensions.Logging;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Security.Cryptography.X509Certificates;
using System.Text;

namespace CreatorPlay.Common;

public static class EncodingClass
{
	private static readonly string _key = "87fhe3f9qwhffn9femh982qe7fh9qe8f";
	private static readonly string _iv = "6179e3fe9bb628b0";

	public static bool GravaTempoExecucao { get; set; }
	public static bool GravaLog { get; set; }
	public static bool Erro { get; set; }
	public static ILogger LoggerClass { get; set; }
	public static string CertificadoJWTSSOString { get; set; }
	public static string PrivateKeyPath { get; set; }
	public static AsymmetricCipherKeyPair PrivateKey { get; set; }
	public static string PublicKeyPath { get; set; }
	public static AsymmetricKeyParameter PublicKey { get; set; }

	public static string EncryptString(string Texto)
	{
		string sResult = "";
		var decryptEngine = new Pkcs1Encoding(new RsaEngine());
		decryptEngine.Init(true, PublicKey);

		byte[] dBytes = Encoding.UTF8.GetBytes(Texto);// String2ByteArray(Texto);

		try
		{
			byte[] bytesToDecrypt = decryptEngine.ProcessBlock(dBytes, 0, dBytes.Length);
			sResult = ByteArrayToString(bytesToDecrypt);
		}
		catch
		{//(Exception ex) {
		 // block incorrect - The private key will be incorrect. It could be that it's not linked up with the correct PGP public key encrypted data object.
			sResult = "";
		}
		return sResult;
	}

	public static string DecryptString(string TextoBase64)
	{
		Erro = false;
		byte[] dBytes = StringToByteArray(TextoBase64);
		var decryptEngine = new Pkcs1Encoding(new RsaEngine());
		decryptEngine.Init(false, PrivateKey.Private);

		string decrypted = "";
		bool blEqual = true;
		if (blEqual)
		{
			try
			{
				byte[] bytesToDecrypt = decryptEngine.ProcessBlock(dBytes, 0, dBytes.Length);
				decrypted = Encoding.UTF8.GetString(bytesToDecrypt);
			}
			catch (Exception ex)
			{
				Erro = true;
				decrypted = ex.Message;
			}
		}

		return decrypted;
	}

	public static string CipherString(string cipherText)
	{
		return EncryptStringToBytesAes(cipherText, Encoding.UTF8.GetBytes(_key), Encoding.UTF8.GetBytes(_iv));
	}

	public static string CipherString(string cipherText, string Key)
	{
		return EncryptStringToBytesAes(cipherText, Encoding.UTF8.GetBytes(Key), Encoding.UTF8.GetBytes(_iv));
	}

	public static string CipherString(string cipherText, string Key, string IV)
	{
		return EncryptStringToBytesAes(cipherText, Encoding.UTF8.GetBytes(Key), Encoding.UTF8.GetBytes(IV));
	}

	public static string EncryptStringToBytesAes(string cipherText, byte[] key, byte[] iv)
	{
		if (cipherText == null || cipherText.Length <= 0)
		{
			Erro = true;
			return "Texto não pode ser nulo!";
		}
		if (key == null || key.Length <= 0)
		{
			Erro = true;
			return "Chave não pode ser nula!";
		}
		MemoryStream msEncrypt;
		RijndaelManaged aesAlg = null;
		try
		{
			aesAlg = new RijndaelManaged { Mode = CipherMode.CBC, KeySize = 256, BlockSize = 128, Key = key, IV = iv };
			ICryptoTransform encryptor = aesAlg.CreateEncryptor(aesAlg.Key, aesAlg.IV);
			msEncrypt = new MemoryStream();
			using (var csEncrypt = new CryptoStream(msEncrypt, encryptor, CryptoStreamMode.Write))
			{
				using (var swEncrypt = new StreamWriter(csEncrypt))
				{
					swEncrypt.Write(cipherText);
					swEncrypt.Flush();
					swEncrypt.Close();
				}
			}
		}
		finally
		{
			if (aesAlg != null)
				aesAlg.Clear();
		}
		string sReturn = ByteArrayToHexString(msEncrypt.ToArray());
		return sReturn;
	}

	public static string DecipherString(string cipherText)
	{
		Erro = false;
		return DecryptStringFromBytesAes(StringToByteArray(cipherText), Encoding.UTF8.GetBytes(_key), Encoding.UTF8.GetBytes(_iv));
	}

	public static string DecipherString(string cipherText, string Secret)
	{
		Erro = false;
		return DecryptStringFromBytesAes(StringToByteArray(cipherText), Encoding.UTF8.GetBytes(Secret), Encoding.UTF8.GetBytes(_iv));
	}

	public static string DecipherString(string cipherText, string Secret, string IV)
	{
		Erro = false;
		return DecryptStringFromBytesAes(StringToByteArray(cipherText), Encoding.UTF8.GetBytes(Secret), Encoding.UTF8.GetBytes(IV));
	}

	public static string DecryptStringFromBytesAes(byte[] cipherText, byte[] key, byte[] iv)
	{
		if (cipherText == null || cipherText.Length <= 0)
		{
			Erro = true;
			return "Texto não pode ser nulo!";
		}
		if (key == null || key.Length <= 0)
		{
			Erro = true;
			return "Chave não pode ser nula!";
		}
		if (iv == null || iv.Length <= 0)
		{
			Erro = true;
			return "IV não pode ser nula!";
		}
		RijndaelManaged aesAlg = null;
		string sRetorno = "";
		try
		{
			aesAlg = new RijndaelManaged { Mode = CipherMode.CBC, KeySize = 256, BlockSize = 128, Key = key, IV = iv, Padding = PaddingMode.None };
			ICryptoTransform decryptor = aesAlg.CreateDecryptor(aesAlg.Key, aesAlg.IV);
			using (var msDecrypt = new MemoryStream(cipherText))
			{
				using (var csDecrypt = new CryptoStream(msDecrypt, decryptor, CryptoStreamMode.Read))
				{
					using (var srDecrypt = new StreamReader(csDecrypt))
					{
						sRetorno = srDecrypt.ReadToEnd().Trim();
						srDecrypt.Close();
					}
				}
			}
		}
		catch (Exception ex)
		{
			Erro = true;
			LoggerClass.LogCritical($"Erro ao Decipher JSON Web Token. Erro: {ex.Message}.");
			sRetorno = $"Erro ao ler Token";
		}
		finally
		{
			// Clear the RijndaelManaged object.
			if (aesAlg != null)
				aesAlg.Clear();
		}
		return sRetorno;
	}

	static public void GetTokenClaim<T>(T Token, Microsoft.IdentityModel.JsonWebTokens.JsonWebToken token)
	{
		Claim _claim;
		Erro = false;
		foreach (System.Reflection.PropertyInfo item in Token.GetType().GetProperties())
		{
			string _name = item.Name;
			if (item.CustomAttributes.Count() > 0)
			{
				foreach (System.Reflection.CustomAttributeData _attr in item.CustomAttributes)
				{
					if (_attr.AttributeType.Name == "DisplayNameAttribute")
					{
						if (_attr.ConstructorArguments.Count > 0)
						{
							_name = _attr.ConstructorArguments[0].Value.ToString();
							break;
						}
					}
				}
			}
			if (!token.TryGetClaim(_name, out _claim))
			{
				Erro = true;
				Token.GetType().GetProperty(item.Name).SetValue(Token, "");
			}
			else
			{
				Token.GetType().GetProperty(item.Name).SetValue(Token, _claim.Value);
			}
		}
	}

	public static string ByteArrayToHexString(byte[] Bytes)
	{
		var Result = new StringBuilder(Bytes.Length * 2);
		string HexAlphabet = "0123456789ABCDEF";
		foreach (byte B in Bytes)
		{
			Result.Append(HexAlphabet[(int)(B >> 4)]);
			Result.Append(HexAlphabet[(int)(B & 0xF)]);
		}
		return Result.ToString();
	}

	public static byte[] StringToByteArray(String hex)
	{
		int NumberChars = hex.Length / 2;
		byte[] bytes = new byte[NumberChars];
		using (var sr = new StringReader(hex))
		{
			for (int i = 0; i < NumberChars; i++)
			{
				bytes[i] = Convert.ToByte(new string(new char[2] { (char)sr.Read(), (char)sr.Read() }), 16);
			}
		}
		return bytes;
	}

	public static string GenerateRandomKey(int length)
	{
		string sRetorno = "";

		var r = new Random();
		for (int i = 0; i < length / 2; i++)
		{
			sRetorno += r.Next(0, 255).ToString("X").PadLeft(2, '0');
		}

		return sRetorno.ToLower();
	}

	public static string ByteArrayToString(byte[] Entrada)
	{
		string sSaida = "";
		for (int i = 0; i < Entrada.Length; i++)
		{
			sSaida += Entrada[i].ToString("X2");
		}
		return sSaida;
	}

	static public X509Certificate2 CertificadoJWTSSO
	{
		get
		{
			if (CertificadoJWTSSOString?.Length > 0)
			{
				return new X509Certificate2(String2ByteArray(CertificadoJWTSSOString));
			}
			else
			{
				return null;
			}
		}
	}

	static public byte[] String2ByteArray(string Entrada)
	{
		byte[] b = null;
		if (Entrada?.Length > 0)
		{
			b = Encoding.ASCII.GetBytes(Entrada);
		}
		return b;
	}

	public static string ByteArray2String(byte[] Entrada)
	{
		return Encoding.ASCII.GetString(Entrada);
	}

	public static string EncodeTo64(int toEncode)
	{
		return EncodeTo64(toEncode.ToString());
	}

	public static string EncodeTo64(string toEncode)
	{
		string sEncode = toEncode;
		byte[] toEncodeAsBytes = Encoding.ASCII.GetBytes(toEncode);
		string returnValue = Convert.ToBase64String(toEncodeAsBytes);
		returnValue = EncryptString(returnValue);
		return returnValue;
	}

	public static string DecodeFrom64(string encodedData)
	{
		string returnValue = "";
		if (encodedData != null)
		{
			encodedData = DecryptString(encodedData);
			if (Erro)
			{
				return "";
			}
			byte[] encodedDataAsBytes = Convert.FromBase64String(encodedData);
			returnValue = Encoding.ASCII.GetString(encodedDataAsBytes).Trim();
		}
		return returnValue;
	}

	public static DateTime UnixTimeStampToDateTime(double unixTimeStamp)
	{
		// Unix timestamp is seconds past epoch
		var dtDateTime = new DateTime(1970, 1, 1, 0, 0, 0, 0, System.DateTimeKind.Utc);
		dtDateTime = dtDateTime.AddSeconds(unixTimeStamp).ToLocalTime();
		return dtDateTime;
	}
}
