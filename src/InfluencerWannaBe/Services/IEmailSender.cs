namespace InfluencerWannaBe.Services
{
    public interface IEmailSender
    {   
        void SendEmail(string recepientEmail, string subject, string body);
    }
}
