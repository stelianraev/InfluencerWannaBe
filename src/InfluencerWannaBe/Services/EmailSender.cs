namespace InfluencerWannaBe.Services
{
    using System.Net.Mail;

    public static class EmailSender
    {
        public static void SendEmail(string recepientEmail, string subject, string body)
        {
            MailMessage mail = new();
            SmtpClient SmtpServer = new("smtp-mail.outlook.com");
            mail.From = new MailAddress("reportWorker@outlook.com");
           
            mail.To.Add(recepientEmail);
            mail.Subject = subject;
            mail.Body = body;

            SmtpServer.Port = 587;
            SmtpServer.Credentials = new System.Net.NetworkCredential("reportWorker@outlook.com", "Parola1234");
            SmtpServer.EnableSsl = true;

            SmtpServer.Send(mail);

        }
    }
}
