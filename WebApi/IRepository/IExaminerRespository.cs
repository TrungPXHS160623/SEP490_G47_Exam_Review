using Library.Models;
using Library.Models.Dtos;

namespace WebApi.IRepository
{
    public interface IExaminerRepository
    {
        Task<List<ExamDto>> GetExamsByCampusAsync(int examinerId, string subjectName = null);
        Task<Exam> UpdateExamStatusAsync(int examId);
        Task<InstructorAssignment> AssignInstructor(InstructorAssignment instructorAssignment);
        Task<IEnumerable<InstructorAssignment>> GetInstructorAssignments(int instructorId);
        Task<IEnumerable<InstructorAssignment>> GetAllInstructorAssignmentsAsync();
        Task<InstructorAssignment?> GetInstructorAssignmentByIdAsync(int id);
    }

}