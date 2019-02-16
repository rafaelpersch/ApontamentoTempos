using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Mail;
using System.Threading.Tasks;

namespace ApontamentoTempos.API.Tools
{
    public static class EmailHelper
    {
        public static async Task EnvioEmail(List<string> destinatarios, string assunto, string mensagem)
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
                message.From = new MailAddress("noreply@softbyte.com.br");

                using (var smtpClient = new SmtpClient())
                {
                    smtpClient.Host = "smtp.gmail.com";
                    smtpClient.Port = 587;
                    smtpClient.EnableSsl = true;
                    smtpClient.DeliveryMethod = SmtpDeliveryMethod.Network;
                    smtpClient.UseDefaultCredentials = false;
                    smtpClient.Credentials = new NetworkCredential("noreply@softbyte.com.br", "");

                    await smtpClient.SendMailAsync(message);
                }
            }
            catch(Exception ex)
            {
                //gravar em log se necessário
            }
        }
    }
}
