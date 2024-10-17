using Library.Common;
using Library.Models;
using Library.Request;
using Library.Response;
using Microsoft.EntityFrameworkCore;
using System.Text.RegularExpressions;
using WebApi.IRepository;

namespace WebApi.Repository
{
    public class UserDetailRepository : IUserDetailRepository
    {
        private readonly QuizManagementContext dbContext;

        public UserDetailRepository(QuizManagementContext dbContext)
        {
            this.dbContext = dbContext;
        }

        // Phương thức kiểm tra người dùng tồn tại và lấy chi tiết vai trò
        private async Task<User> CheckExistingUserAsync(int userId)
        {
            return await dbContext.Users.Include(u => u.Role)
                                        .FirstOrDefaultAsync(u => u.UserId == userId);
        }

        // Phương thức kiểm tra chi tiết người dùng tồn tại
        private async Task<UserDetail> CheckExistingUserDetailAsync(int userId)
        {
            return await dbContext.UserDetails.FirstOrDefaultAsync(ud => ud.UserId == userId);
        }

        // Kiểm tra số điện thoại hợp lệ
        private bool ValidatePhoneNumber(string phoneNumber)
        {
            return string.IsNullOrEmpty(phoneNumber) || Regex.IsMatch(phoneNumber.Trim(), @"^\d+$");
        }
        // phương thức kiểm tra UserId có trùng lặp hay không
        private async Task<bool> IsUserIdDuplicateAsync(int userId, int? userDetailId = null)
        {
            if (userDetailId == null)
            {
                // Kiểm tra khi tạo mới, userId không được phép tồn tại trong bất kỳ bản ghi nào
                return await dbContext.UserDetails.AnyAsync(ud => ud.UserId == userId);
            }
            else
            {
                // Kiểm tra khi cập nhật, userId không được phép tồn tại trong bản ghi khác (khác userDetailId hiện tại)
                return await dbContext.UserDetails.AnyAsync(ud => ud.UserId == userId && ud.UserDetailId != userDetailId);
            }
        }
        // Tạo phương thức kiểm tra và cập nhật UserDetail
        private async Task<RequestResponse> CreateOrUpdateUserDetail(UserDetailRequest request, UserDetail userDetail = null)
        {
            var existingUser = await CheckExistingUserAsync(request.UserId);
            if (existingUser == null)
            {
                return new RequestResponse { IsSuccessful = false, Message = "User does not exist!" };
            }

            if (userDetail == null && await CheckExistingUserDetailAsync(request.UserId) != null)
            {
                return new RequestResponse { IsSuccessful = false, Message = "User detail already exists!" };
            }

            // Kiểm tra vai trò để yêu cầu email Fe
            var userRole = existingUser.Role;
            if (userRole == null)
            {
                return new RequestResponse { IsSuccessful = false, Message = "User role is not defined!" };
            }

            if (userRole.RoleName.Equals("Head of Department") || userRole.RoleName.Equals("Lecturer"))
            {
                if (string.IsNullOrEmpty(request.EmailFe))
                {
                    return new RequestResponse
                    {
                        IsSuccessful = false,
                        Message = "EmailFe is required for Lecturer and Head of Department!"
                    };
                }
            }
            else
            {
                request.EmailFe = null;  // Không cần email Fe cho vai trò khác
            }

            if (!ValidatePhoneNumber(request.PhoneNumber))
            {
                return new RequestResponse { IsSuccessful = false, Message = "Phone number must only contain digits!" };
            }

            // Nếu là tạo mới, kiểm tra trùng lặp UserId
            if (userDetail == null)
            {
                if (await IsUserIdDuplicateAsync(request.UserId))
                {
                    return new RequestResponse { IsSuccessful = false, Message = "User ID already exists!" };
                }
                userDetail = new UserDetail { CreateDate = DateTime.Now };
                await dbContext.UserDetails.AddAsync(userDetail);
            }
            else
            {
                // Nếu là cập nhật, kiểm tra trùng lặp UserId ngoài bản ghi hiện tại
                if (await IsUserIdDuplicateAsync(request.UserId, userDetail.UserDetailId))
                {
                    return new RequestResponse { IsSuccessful = false, Message = "User ID already exists in another record!" };
                }
            }

            userDetail.UserId = request.UserId;
            userDetail.FullName = request.FullName;
            userDetail.PhoneNumber = request.PhoneNumber?.Trim();
            userDetail.EmailFe = request.EmailFe;
            userDetail.DateOfBirth = request.DateOfBirth;
            userDetail.Gender = request.Gender;
            userDetail.Address = request.Address;
            userDetail.ProfilePicture = request.ProfilePicture;
            userDetail.IsActive = request.IsActive ?? true;
            userDetail.UpdateDate = DateTime.Now;

            await dbContext.SaveChangesAsync();
            return new RequestResponse { IsSuccessful = true, Message = "User detail saved successfully!" };
        }

        public async Task<RequestResponse> CreateUserDetailAsync(UserDetailRequest request)
        {
            return await CreateOrUpdateUserDetail(request);
        }

        public async Task<RequestResponse> UpdateUserDetailAsync(int userDetailId, UserDetailRequest request)
        {
            var userDetail = await dbContext.UserDetails.FirstOrDefaultAsync(ud => ud.UserDetailId == userDetailId);
            if (userDetail == null)
            {
                return new RequestResponse { IsSuccessful = false, Message = "User detail does not exist!" };
            }
            return await CreateOrUpdateUserDetail(request, userDetail);
        }

        public async Task<ResultResponse<UserDetailResponse>> GetUserDetailByUserIdAsync(int userId)
        {
            try
            {
                var data = await dbContext.UserDetails.FirstOrDefaultAsync(x => x.UserId == userId);
                if (data == null)
                {
                    return new ResultResponse<UserDetailResponse>
                    {
                        IsSuccessful = false,
                        Message = "User has not had detail info yet."
                    };
                }

                return new ResultResponse<UserDetailResponse>
                {
                    IsSuccessful = true,
                    Item = MapToUserDetailResponse(data)
                };
            }
            catch (Exception ex)
            {
                return new ResultResponse<UserDetailResponse>
                {
                    IsSuccessful = false,
                    Message = ex.Message
                };
            }
        }

        public async Task<ResultResponse<UserDetailResponse>> GetUserDetailsAsync()
        {
            try
            {
                var data = await dbContext.UserDetails.ToListAsync();
                return new ResultResponse<UserDetailResponse>
                {
                    IsSuccessful = true,
                    Items = data.Select(MapToUserDetailResponse).ToList()
                };
            }
            catch (Exception ex)
            {
                return new ResultResponse<UserDetailResponse>
                {
                    IsSuccessful = false,
                    Message = ex.Message
                };
            }
        }

        public async Task<RequestResponse> ToggleUserDetailStatusAsync(int userDetailId)
        {
            var userDetail = await dbContext.UserDetails.FindAsync(userDetailId);
            if (userDetail == null)
            {
                return new RequestResponse
                {
                    IsSuccessful = false,
                    Message = "User detail not found."
                };
            }

            userDetail.IsActive = !userDetail.IsActive;
            userDetail.UpdateDate = DateTime.Now;

            await dbContext.SaveChangesAsync();

            return new RequestResponse
            {
                IsSuccessful = true,
                Message = userDetail.IsActive != null && userDetail.IsActive == true
                ? "User detail activated."
                : "User detail deactivated."
            };
        }

        public async Task<bool> DeleteUserDetailAsync(int userDetailId)
        {
            var userDetail = await dbContext.UserDetails.FindAsync(userDetailId);
            if (userDetail == null)
            {
                return false;
            }

            dbContext.UserDetails.Remove(userDetail);
            await dbContext.SaveChangesAsync();
            return true;
        }

        // Tạo phương thức để map dữ liệu từ UserDetail sang UserDetailResponse
        private UserDetailResponse MapToUserDetailResponse(UserDetail data)
        {
            return new UserDetailResponse
            {
                UserDetailId = data.UserDetailId,
                UserId = data.UserId,
                FullName = data.FullName,
                PhoneNumber = data.PhoneNumber,
                EmailFe = data.EmailFe,
                DateOfBirth = data.DateOfBirth,
                Gender = data.Gender,
                Address = data.Address,
                ProfilePicture = data.ProfilePicture,
                IsActive = data.IsActive,
                CreateDate = data.CreateDate,
                UpdateDate = data.UpdateDate
            };
        }
    }
}
