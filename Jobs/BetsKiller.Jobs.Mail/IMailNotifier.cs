namespace BetsKiller.Jobs.Mail
{
    public interface IMailNotifier
    {
        void SendServiceJobStatus(string jobName, string status, string message);

        void SendMailConfirmation(string link, string mailAddressTo);

        void SendPasswordReset(string link, string mailAddressTo);
    }
}
