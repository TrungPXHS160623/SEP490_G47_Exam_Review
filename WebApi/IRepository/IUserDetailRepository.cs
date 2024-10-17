using Library.Common;
using Library.Request;
using Library.Response;

namespace WebApi.IRepository
{
    public interface IUserDetailRepository
    {


        // Lấy danh sách tất cả các chi tiết người dùng
        Task<ResultResponse<UserDetailResponse>> GetUserDetailsAsync();
        //lấy thông tin chi tiết của ng dùng dựa theo userId
        Task<ResultResponse<UserDetailResponse>> GetUserDetailByUserIdAsync(int userId);

        // Thêm mới chi tiết người dùng
        Task<RequestResponse> CreateUserDetailAsync(UserDetailRequest request);

        // Cập nhật thông tin chi tiết người dùng
        Task<RequestResponse> UpdateUserDetailAsync(int userDetailId, UserDetailRequest request);

        // Xóa chi tiết người dùng
        Task<bool> DeleteUserDetailAsync(int userDetailId);

        // Kích hoạt hoặc vô hiệu hóa chi tiết người dùng
        Task<RequestResponse> ToggleUserDetailStatusAsync(int userDetailId);
    }
}
