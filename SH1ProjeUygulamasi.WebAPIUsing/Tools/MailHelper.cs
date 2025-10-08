using System.Net;
using System.Net.Mail; // MailMessage, SmtpClient

namespace SH1ProjeUygulamasi.WebAPIUsing.Tools
{
    public class MailHelper
    {
        public static void SendMail(string to, string subject, string body)
        {
            MailMessage mail = new MailMessage();
            SmtpClient smtpClient = new SmtpClient("smtp.gmail.com");
            mail.From = new MailAddress("mail@gmail.com"); // Gönderen e-posta adresi
            mail.To.Add(to); // Alıcı e-posta adresi
            mail.Subject = subject; // E-posta konusu
            mail.Body = body; // E-posta içeriği
            mail.IsBodyHtml = true; // HTML formatında gönderim
            smtpClient.Port = 587; // SMTP portu
            smtpClient.Credentials = new NetworkCredential("mail@gmail.com", "mailşifresi"); // Gönderen e-posta ve şifre
            smtpClient.EnableSsl = true; // SSL kullanımı
            smtpClient.Send(mail); // E-postayı gönder
        }
    }
}
