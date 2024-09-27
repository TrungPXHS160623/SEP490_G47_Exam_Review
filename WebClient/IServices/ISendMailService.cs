using Library.Common;

namespace WebClient.IServices
{
    public interface ISendMailService
    {
        /// <summary>
        /// Send mail
        /// </summary>
        /// <returns>A <see cref="Task"/> representing the result of the asynchronous operation.</returns>
        Task<RequestResponse> SendMail(List<MailUtil> mail);
    }
}
