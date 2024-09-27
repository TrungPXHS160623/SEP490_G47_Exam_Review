
using Library.Common;
using Library.Models;
using System.Net;
using System.Net.Mail;
using System.Text;
using WebApi.IRepository;

namespace WebApi.Repository
{
    public class SendMailRepository : ISendMailRepository
    {
        private readonly QuizManagementContext DBcontext;

        public SendMailRepository(IConfiguration configuration, QuizManagementContext DBcontext)
        {
            this.DBcontext = DBcontext;
            this.Configuration = configuration;
        }

        private IConfiguration Configuration { get; }

        public async Task<RequestResponse> SendMail(List<MailUtil> mail)
        {
            RequestResponse? response;

            try
            {
                var emailSettings = new EmailSettings
                {
                    Host = this.Configuration["BusinessEmail:Host"],
                    Port = int.Parse(this.Configuration["BusinessEmail:Port"]),
                    Subject = this.Configuration["BusinessEmail:Subject"],
                    Message = this.Configuration["BusinessEmail:Message"],
                    Username = this.Configuration["BusinessEmail:Username"],
                    Password = this.Configuration["BusinessEmail:Password"],
                };

                var subject = "Assign đề thi cho chủ nhiệm bộ môn";

                var body = "Đề thi đã được khảo thí chuyển cho các chủ nhiệm bộ môn. Chủ nhiệm bộ môn thực hiện việc assign cho các giảng viên để thực hiện test đề thi";

                using (var client = new SmtpClient(emailSettings.Host, emailSettings.Port))
                {
                    client.EnableSsl = true;
                    client.Credentials = new NetworkCredential(emailSettings.Username, emailSettings.Password);

                    foreach (var item in mail)
                    {
                        using (var message = new MailMessage(emailSettings.Username, item.MailTo, subject, body))
                        {
                            message.IsBodyHtml = true;
                            message.BodyEncoding = Encoding.UTF8;
                            message.SubjectEncoding = Encoding.UTF8;
                            message.ReplyToList.Add(new MailAddress(emailSettings.Username));
                            message.Sender = new MailAddress(emailSettings.Username);

                            if (!string.IsNullOrEmpty(item.BccTo))
                            {
                                message.Bcc.Add(new MailAddress(item.BccTo));
                            }

                            if (!string.IsNullOrEmpty(item.CcTo))
                            {
                                message.CC.Add(new MailAddress(item.CcTo));
                            }

                            await client.SendMailAsync(message).ConfigureAwait(false);
                        }
                    }
                }

                return new RequestResponse
                {
                    IsSuccessful = true,
                    Message = "Send Mail Successfully"
                };
            }
            catch (SmtpFailedRecipientException ex)
            {
                response = new RequestResponse
                {
                    IsSuccessful = false,
                    Message = ex.Message,
                };
                return response;
            }
            catch (Exception ex)
            {
                response = new RequestResponse
                {
                    IsSuccessful = false,
                    Message = ex.Message,

                };
                return response;
            }
        }
    }
}
