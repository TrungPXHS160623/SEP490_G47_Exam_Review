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

            var result = await _sendMailRepository.TestSendMail();
        }

        private async Task SendReminderForUncorrectedExams()
        {
            
        }

        private async Task SendReminderForExamsWithoutScheduledDate()
        {
            
        }
    }
}
