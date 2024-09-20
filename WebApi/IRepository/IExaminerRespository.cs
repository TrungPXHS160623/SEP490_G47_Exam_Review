using Library.Enums;
using Library.Models;
using Library.Models.Dtos;

namespace WebApi.IRepository
{
    public interface IExaminerRepository
    {
        Task<List<ExamDto>> GetExamsByCampusAsync(int examinerId, string subjectName = null);
        Task<Exam> UpdateExamStatusAsync(int examId, ExamStatusEnum status);
    }

}