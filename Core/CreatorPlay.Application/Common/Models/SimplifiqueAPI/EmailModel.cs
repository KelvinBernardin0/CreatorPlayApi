namespace CreatorPlay.Application.Common.Models.SimplifiqueAPI;

public class EmailModel
{
    public bool ManterCopia { get; set; } = false;
    public string TituloEmail { get; set; } = "Alteração de Senha - CreatorPlay";
    public string CorpoEmail { get; set; }
    public string EmailReplyTo { get; set; }
    public List<Anexo> Anexos { get; } = new List<Anexo>();
    public List<string> ListaEmailPara { get; } = new List<string>();
    public List<string> ListaEmailCopia { get; } = new List<string>();
    public List<string> ListaEmailCopiaOculta { get; } = new List<string>();
    public EmailCredentials EmailCredentials { get; set; }


    public void AddAttachment(params string[] attachmentsPath)
    {
        foreach (var attachment in attachmentsPath)
        {
            Anexos.Add(new Anexo(Path.GetFileName(attachment), File.ReadAllBytes(attachment)));
        }
    }

    public void AddEmailTo(params string[] emailsTo)
    {
        foreach (string email in emailsTo) { ListaEmailPara.Add(email); }
    }

    public void AddEmailCopy(params string[] emailsCopy)
    {
        foreach (string email in emailsCopy) { ListaEmailCopia.Add(email); }
    }

    public void AddHiddenEmailCopy(params string[] emailsHiddenEmailCopy)
    {
        foreach (string email in emailsHiddenEmailCopy) { ListaEmailCopiaOculta.Add(email); }
    }
}
