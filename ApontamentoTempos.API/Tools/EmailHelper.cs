using System.Collections.Generic;
using System.Net;
using System.Net.Mail;
using System.Threading.Tasks;

namespace ApontamentoTempos.API.Tools
{
    public static class EmailHelper
    {
        public static async Task EnvioEmail(Email remetente, List<string> destinatarios, string assunto, string mensagem)
        {
            try
            {
                var message = new MailMessage();

                //message.Attachments anexos

                if (destinatarios != null)
                {
                    if (destinatarios.Count == 0)
                    {
                        return;
                    }

                    foreach (string s in destinatarios)
                    {
                        message.To.Add(s);
                    }
                }

                message.Subject = assunto;
                message.Body = mensagem;
                message.IsBodyHtml = true;
                message.From = new MailAddress(remetente.EnderecoEmail);

                using (var smtpClient = new SmtpClient())
                {
                    smtpClient.Host = remetente.ServidorSmtp;
                    smtpClient.Port = remetente.PortaSmtp;
                    smtpClient.EnableSsl = remetente.UtilizarSsl;
                    smtpClient.DeliveryMethod = SmtpDeliveryMethod.Network;
                    smtpClient.UseDefaultCredentials = false;
                    smtpClient.Credentials = new NetworkCredential(remetente.EnderecoEmail, remetente.Senha);

                    await smtpClient.SendMailAsync(message);
                }
            }
            catch
            {
                //gravar em log se necessário
            }
        }
    }

    public class Email
    {
        public string EnderecoEmail { get; set; }
        public string Senha { get; set; }
        public string ServidorSmtp { get; set; }
        public int PortaSmtp { get; set; }
        public bool UtilizarSsl { get; set; }

        public Email()
        {
            this.EnderecoEmail = string.Empty;
            this.ServidorSmtp = string.Empty;
            this.PortaSmtp = 0;
            this.Senha = string.Empty;
            this.UtilizarSsl = false;
        }
    }
}
