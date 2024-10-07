using Library.Common;
using Microsoft.AspNetCore.Mvc;
using WebApi.IRepository;
  
namespace WebApi.Controllers
{
    public class SendMailController : ApiBaseController
    {
        private readonly ISendMailRepository _sendMailRepository;

        public SendMailController(ISendMailRepository sendMailRepository)
            : base()
        {
            _sendMailRepository = sendMailRepository;
        }

        [HttpPost("SendMail")]
        public async Task<IActionResult> SendMail([FromBody] MailModel mail)
        {
            var result = await this._sendMailRepository.SendMail(mail);

            return this.Ok(result);
        }
    }
}
