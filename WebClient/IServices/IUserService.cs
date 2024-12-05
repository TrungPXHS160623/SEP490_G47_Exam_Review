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

        Task<ResultResponse<UserRequest>> GetByIdAsync(int id);

        Task<ResultResponse<UserSubjectRequest>> GetUserSubjectByIdAsync(int id);
        Task<ResultResponse<UserSubjectRequest>> GetUserFacutyByIdAsync(int id);

        Task<RequestResponse> UpdateAsync(UserRequest user);

        Task<RequestResponse> ExaminerUpdateUserAsync(UserSubjectRequest user);

        Task<RequestResponse> DeleteAsync(int id);
        Task<RequestResponse> CreateHeadAsync(UserSubjectRequest user);
        Task<RequestResponse> CreateAsync(UserRequest user);
        Task<RequestResponse> ImportUserFromExcel(IBrowserFile files);

        Task<ResultResponse<UserResponse>> GetUserBySubject(int subjectId, int campusId);

        Task<ResultResponse<AddLecturerSubjectRequest>> GetUserByMail(string mail, int headId);

        Task<RequestResponse> AddUserToSubject(AddLecturerSubjectRequest req);

        Task<RequestResponse> EditLecturer(AddLecturerSubjectRequest req);
        Task<RequestResponse> RemoveLecture(int userId, int subjectId);
    }
}
