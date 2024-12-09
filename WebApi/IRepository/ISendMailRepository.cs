using Library.Common;
using Library.Response;

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

        Task<RequestResponse> TestSendMail();
        Task<RequestResponse> SendMailRemind(List<ExamRemindResponse> exam,int option);
    }
}
