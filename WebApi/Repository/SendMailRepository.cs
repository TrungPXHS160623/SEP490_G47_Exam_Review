
using Library.Common;
using Library.Models;
using Library.Response;
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

        public async Task<RequestResponse> SendMailRemind(List<ExamRemindResponse> exam, int option)
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

            var groupedExams = exam.GroupBy(e => e.Mail).ToList();

            using (var client = new SmtpClient(emailSettings.Host, emailSettings.Port))
            {
                client.EnableSsl = true;
                client.Credentials = new NetworkCredential(emailSettings.Username, emailSettings.Password);

                foreach (var group in groupedExams)
                {
                    var mailTo = group.Key; 

                    var examCodes = group.Select(e => e.ExamCode).ToList();

                    string subject;
                    string body;

                    if (option == 1)
                    {
                        subject = "Notification: Remind Exam Codes with Errors Not Resolved";
                        body = $@"
                        <p><b>Dear {mailTo},</b></p>
                        <p>We would like to inform you that the following exam codes have issues that have not been resolved after 3 days:</p>
                        <table border='1' style='border-collapse: collapse; width: 100%;'>
                            <thead>
                                <tr>
                                    <th style='padding: 8px; text-align: left; background-color: #f2f2f2;'>Exam Code</th>
                                </tr>
                            </thead>
                            <tbody>";

                        foreach (var examCode in examCodes)
                        {
                            body += $@"
                                 <tr>
                                     <td style='padding: 8px;'>{examCode}</td>
                                 </tr>";
                        }

                        body += @"
                            </tbody>
                        </table>
                        <p>For more details, please visit the website at <a href='https://www.examreviewfpt.somee.com'>Exam Review System</a>.</p>
                        <p><b>From:</b> Remind System</p><br>
                        <p>Do not reply this mail</p><br>
                        <p><b>Exam Review System</b></p><br>";
                    }
                    else if (option == 2)
                    {
                        subject = "Notification: Remind Exam Codes Assigned for Review but No Review Date Set";
                        body = $@"
                            <p><b>Dear {mailTo},</b></p>
                            <p>We would like to inform you that the following exam codes have been assigned to you for review, but no review date has been chosen after 3 days:</p>
                            <table border='1' style='border-collapse: collapse; width: 100%;'>
                                <thead>
                                    <tr>
                                        <th style='padding: 8px; text-align: left; background-color: #f2f2f2;'>Exam Code</th>
                                    </tr>
                                </thead>
                                <tbody>";

                        foreach (var examCode in examCodes)
                        {
                            body += $@"
                                <tr>
                                    <td style='padding: 8px;'>{examCode}</td>
                                </tr>";
                        }

                        body += @"
                            </tbody>
                        </table>
                        <p>For more details, please visit the website at <a href='https://www.examreviewfpt.somee.com'>Exam Review System</a>.</p>
                        <p>Thank you for your support!</p>
                        <p><b>From:</b> Remind System</p><br>
                        <p>Do not reply this mail</p><br>
                        <p><b>Exam Review System</b></p><br>";
                    }
                    else if (option == 3)
                    {
                        subject = $"Notification: Remind the Review Day for Exam";
                        body = $@"
                        <p><b>Dear Lecturer/Department Head,</b></p>
                        <p>We would like to remind you that today is the review day for the following exam code(s):</p>
                        <table border='1' style='border-collapse: collapse; width: 100%;'>
                            <thead>
                                <tr>
                                    <th style='padding: 8px; text-align: left; background-color: #f2f2f2;'>Exam Code</th>
                                </tr>
                            </thead>
                            <tbody>";

                        foreach (var examCode in examCodes)
                        {
                            body += $@"
                                <tr>
                                    <td style='padding: 8px;'>{examCode}</td>
                                </tr>";
                        }

                        body += @"
                            </tbody>
                        </table>
                        <p>For more details, please visit the website at <a href='https://www.examreviewfpt.somee.com'>Exam Review System</a>.</p>
                        <p>Thank you for your attention!</p>
                        <p><b>From:</b> {emailSettings.Username}</p><br>
                        <p><b>Exam Review System</b></p><br>";
                    }
                    else
                    {
                        return new RequestResponse
                        {
                            IsSuccessful = false,
                            Message = "Invalid option"
                        };
                    }

                    using (var message = new MailMessage(emailSettings.Username, mailTo, subject, body))
                    {
                        message.IsBodyHtml = true;
                        message.BodyEncoding = Encoding.UTF8;
                        message.SubjectEncoding = Encoding.UTF8;
                        message.ReplyToList.Add(new MailAddress(emailSettings.Username));
                        message.Sender = new MailAddress(emailSettings.Username);
                        message.IsBodyHtml = true;

                        try
                        {
                            await client.SendMailAsync(message).ConfigureAwait(false);
                        }
                        catch (Exception ex)
                        {
                            return new RequestResponse
                            {
                                IsSuccessful = false,
                                Message = $"Error sending email to {mailTo}: {ex.Message}"
                            };
                        }
                    }
                }
            }

            return new RequestResponse
            {
                IsSuccessful = true,
                Message = "Send Mail Successfully"
            };
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
