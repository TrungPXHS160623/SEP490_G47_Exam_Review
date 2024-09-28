﻿using Library.Common;
using Library.Response;
namespace WebApi.IRepository
{
    public interface IExaminerRepository
    {
        Task<ResultResponse<ExamByCampusResponse>> GetExamsByCampusAsync(int examinerId);
        Task<ResultResponse<ExamDetailResponse>> GetExamsDetail(int examID);
        //Task<List<ExamDto>> GetExamsByCampusAsync(int examinerId, string subjectName = null);
        //Task<Exam> UpdateExamStatusAsync(int examId);
        //Task<ExamAssignment> AssignInstructor(ExamAssignment instructorAssignment);
        //Task<IEnumerable<ExamAssignment>> GetExamAssignments(int instructorId);
        //Task<IEnumerable<ExamAssignment>> GetAllExamAssignmentsAsync();
        //Task<ExamAssignment?> GetExamAssignmentByIdAsync(int id);
    }

}