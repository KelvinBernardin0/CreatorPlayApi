using WebApi_VivoValoriza.BouncyCrypto.OpenSsl;

namespace CreatorPlay.Application.Common.Models;

public class PasswordFinder : IPasswordFinder
{
    private string v;

    public PasswordFinder(string v)
    {
        this.v = v;
    }

    public char[] GetPassword()
    {
        return v.ToCharArray();
    }
}