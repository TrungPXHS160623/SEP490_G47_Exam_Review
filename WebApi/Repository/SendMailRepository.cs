
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

        public async Task<RequestResponse> SendMail(MailModel mail)
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

                using (var client = new SmtpClient(emailSettings.Host, emailSettings.Port))
                {
                    client.EnableSsl = true;
                    client.Credentials = new NetworkCredential(emailSettings.Username, emailSettings.Password);

                    foreach (var item in mail.MailList)
                    {
                        using (var message = new MailMessage(emailSettings.Username, item.MailTo, mail.Subject, string.Format(mail.Body, item.MailTo)))
                        {
                            message.IsBodyHtml = true;
                            message.BodyEncoding = Encoding.UTF8;
                            message.SubjectEncoding = Encoding.UTF8;
                            message.ReplyToList.Add(new MailAddress(emailSettings.Username));
                            message.Sender = new MailAddress(emailSettings.Username);
                            message.IsBodyHtml = true;
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

        public async Task<RequestResponse> TestSendMail()
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

            using (var client = new SmtpClient(emailSettings.Host, emailSettings.Port))
            {
                client.EnableSsl = true;
                client.Credentials = new NetworkCredential(emailSettings.Username, emailSettings.Password);

                using (var message = new MailMessage(emailSettings.Username, "hunglthe160235@fpt.edu.vn", "Test", "Test"))
                {
                    message.IsBodyHtml = true;
                    message.BodyEncoding = Encoding.UTF8;
                    message.SubjectEncoding = Encoding.UTF8;
                    message.ReplyToList.Add(new MailAddress(emailSettings.Username));
                    message.Sender = new MailAddress(emailSettings.Username);
                    message.IsBodyHtml = true;

                    await client.SendMailAsync(message).ConfigureAwait(false);
                }
            }

            return new RequestResponse
            {
                IsSuccessful = true,
                Message = "Send Mail Successfully"
            };
        }
    }
}
