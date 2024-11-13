using Library.Common;
using Library.Request;
using Library.Response;
using Microsoft.AspNetCore.Components.Forms;

namespace WebClient.IServices
{
    public interface IUserService
    {
        Task<AuthenticationResponse> LoginUserAsync(UserRequest request);

        Task<RequestResponse> RegisterUserAsync(UserRegisterRequest request);

        Task<ResultResponse<UserResponse>> GetLectureListBySubject(int subjectId, int campusId);
        Task<ResultResponse<UserResponse>> GetLectureListByHead(int userId);

        Task<ResultResponse<UserResponse>> GetHeadOfDepartment(int subjectId, int campusId);

        Task<ResultResponse<UserResponse>> GetUserForAdmin(string filterQuery);

        Task<ResultResponse<UserResponse>> GetUserForExaminer(int userId, string filterQuery);

        Task<AuthenticationResponse> GetJWT();

        Task<RequestResponse> ClearJWT();

        Task<ResultResponse<UserRequest>> GetByIdAsync(int id);

        Task<ResultResponse<UserSubjectRequest>> GetUserSubjectByIdAsync(int id);
        Task<ResultResponse<UserSubjectRequest>> GetUserFacutyByIdAsync(int id);

        Task<RequestResponse> UpdateAsync(UserRequest user);

        Task<RequestResponse> ExaminerUpdateUserAsync(UserSubjectRequest user);

        Task<RequestResponse> DeleteAsync(int id);

        Task<RequestResponse> CreateAsync(UserRequest user);
        Task<RequestResponse> ImportUserFromExcel(IBrowserFile files);

        Task<ResultResponse<UserResponse>> GetAssignedUserByExam(int examId);

    }
}
