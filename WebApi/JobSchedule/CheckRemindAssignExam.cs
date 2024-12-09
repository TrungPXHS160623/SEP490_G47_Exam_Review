using Quartz;
using WebApi.IRepository;

namespace WebApi.JobSchedule
{
    public class CheckRemindAssignExam : IJob
    {
        private readonly ISendMailRepository _sendMailRepository;
        private readonly IExamRepository _examRepository;

        public CheckRemindAssignExam(ISendMailRepository sendMailRepository, IExamRepository examRepository)
        {
            _sendMailRepository = sendMailRepository;
            _examRepository = examRepository;
        }

        public async Task Execute(IJobExecutionContext context)
        {
            await SendReminderForUncorrectedExams();

            await SendReminderForExamsWithoutScheduledDate();

            await SendReminderForReviewDate();

        }

        private async Task SendReminderForUncorrectedExams()
        {
            var data = await this._examRepository.SendReminderForUncorrectedExams();

            if(data != null && data.Count >0)
            {
                var result = await _sendMailRepository.SendMailRemind(data,1);
            }
        }

        private async Task SendReminderForExamsWithoutScheduledDate()
        {
            var data = await this._examRepository.SendReminderForExamsWithoutScheduledDate();

            if (data != null && data.Count > 0)
            {
                var result = await _sendMailRepository.SendMailRemind(data, 2);
            }
        }

        private async Task SendReminderForReviewDate()
        {
            var data = await this._examRepository.SendReminderForReviewDate();

            if (data != null && data.Count > 0)
            {
                var result = await _sendMailRepository.SendMailRemind(data, 3);
            }
        }
    }
}
