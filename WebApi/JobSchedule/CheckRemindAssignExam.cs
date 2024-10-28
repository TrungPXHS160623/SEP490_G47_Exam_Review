using Quartz;
using WebApi.IRepository;
using WebClient.IServices;

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

        public Task Execute(IJobExecutionContext context)
        {
            throw new NotImplementedException();
        }
    }
}
