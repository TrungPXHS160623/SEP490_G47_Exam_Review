using Library.Common;

namespace WebApi.IRepository
{
    public interface ISendMailRepository
    {
        /// <summary>
        /// Send mail
        /// </summary>
        /// <param name="mail">Content of the mail.</param>
        /// <returns>The request response.</returns>
        Task<RequestResponse> SendMail(MailModel mail);
    }
}
