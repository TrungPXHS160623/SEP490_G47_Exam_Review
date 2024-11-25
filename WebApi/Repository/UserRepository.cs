using ExcelDataReader;
using Library.Common;
using Library.Models;
using Library.Request;
using Library.Response;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json;
using System.Globalization;
using System.IdentityModel.Tokens.Jwt;
using System.Net.Http.Headers;
using System.Security.Claims;
using System.Text;
using WebApi.IRepository;

namespace WebApi.Repository
{
    public class UserRepository : IUserRepository
    {
        private readonly QuizManagementContext dbContext;
        private readonly ILogHistoryRepository logRepository;
        private readonly IConfiguration config;

        public UserRepository(QuizManagementContext dbContext, ILogHistoryRepository logRepository, IConfiguration config)
        {
            this.dbContext = dbContext;
            this.logRepository = logRepository;
            this.config = config;
        }
        public async Task<RequestResponse> CreateAsync(UserRequest user)
        {
            try
            {
                var data = await this.dbContext.Users.FirstOrDefaultAsync(x => x.Mail.Equals(user.Email));

                if (data != null)
                {
                    return new RequestResponse
                    {
                        IsSuccessful = false,
                        Message = "Mail already exist!"
                    };
                }
                else
                {
                    var newUser = new User
                    {
                        Mail = user.Email + "@fpt.edu.vn",
                        FullName = user.UserName,
                        PhoneNumber = user.Phone,
                        RoleId = user.RoleId,
                        CampusId = user.CampusId,
                        CreateDate = DateTime.Now,
                        UpdateDate = DateTime.Now,
                        IsActive = user.IsActive.Value,
                    };
                    await dbContext.Users.AddAsync(newUser);
                    await dbContext.SaveChangesAsync();
                }

                await logRepository.LogAsync($"Create user {user.Email}");

                return new RequestResponse
                {
                    IsSuccessful = true,
                    Message = "Create account successfully!"
                };
            }
            catch (Exception ex)
            {
                return new RequestResponse
                {
                    IsSuccessful = false,
                    Message = ex.Message,
                };
            }
        }

        public async Task<ResultResponse<UserResponse>> GetAllAsync()
        {
            try
            {
                var data = (from u in this.dbContext.Users
                            join c in this.dbContext.Campuses on u.CampusId equals c.CampusId into campusJoin
                            from c in campusJoin.DefaultIfEmpty() // Left join for Campuses
                            join r in this.dbContext.UserRoles on u.RoleId equals r.RoleId into roleJoin
                            from r in roleJoin.DefaultIfEmpty() // Left join for UserRoles
                            select new UserResponse
                            {
                                Email = u.Mail,
                                CampusName = c != null ? c.CampusName : null, // Handle possible null from left join
                                IsActive = u.IsActive,
                                RoleName = r != null ? r.RoleName : null,     // Handle possible null from left join
                                UserId = u.UserId,
                                UpdateDt = u.UpdateDate,
                            }).ToList();

                return new ResultResponse<UserResponse>
                {
                    IsSuccessful = true,
                    Items = data.OrderByDescending(x => x.UpdateDt).ToList(),
                };
            }
            catch (Exception ex)
            {
                return new ResultResponse<UserResponse>
                {
                    IsSuccessful = false,
                    Message = ex.Message,
                };
            }
        }

        public async Task<ResultResponse<UserResponse>> GetUserForAdmin(string filterQuery)
        {
            var data = (from u in this.dbContext.Users
                        join c in this.dbContext.Campuses on u.CampusId equals c.CampusId into campusJoin
                        from c in campusJoin.DefaultIfEmpty() // Left join for Campuses
                        join r in this.dbContext.UserRoles on u.RoleId equals r.RoleId into roleJoin
                        from r in roleJoin.DefaultIfEmpty() // Left join for UserRoles
                        where (string.IsNullOrEmpty(filterQuery) || u.Mail.ToLower().Contains(filterQuery.ToLower()))
                        && (u.RoleId == 1 || u.RoleId == 2 || u.RoleId == null)
                        select new UserResponse
                        {
                            Email = u.Mail,
                            Tel = u.PhoneNumber,
                            UserName = u.FullName,
                            CampusName = c != null ? c.CampusName : null, // Handle possible null from left join
                            IsActive = u.IsActive,
                            RoleName = r != null ? r.RoleName : null,     // Handle possible null from left join
                            UserId = u.UserId,
                            UpdateDt = u.UpdateDate,
                        }).ToList();

            return new ResultResponse<UserResponse>
            {
                IsSuccessful = true,
                Items = data.OrderByDescending(x => x.UpdateDt).ToList(),
            };
        }

        public async Task<ResultResponse<UserResponse>> GetUserForExaminer(int userId, string filterQuery)
        {
            var campusId = (await this.dbContext.Users.FirstOrDefaultAsync(x => x.UserId == userId))?.CampusId;

            var data = (from u in this.dbContext.Users
                        join c in this.dbContext.Campuses on u.CampusId equals c.CampusId into campusJoin
                        from c in campusJoin.DefaultIfEmpty() // Left join for Campuses
                        join r in this.dbContext.UserRoles on u.RoleId equals r.RoleId into roleJoin
                        from r in roleJoin.DefaultIfEmpty() // Left join for UserRoles
                        where (string.IsNullOrEmpty(filterQuery) || u.Mail.ToLower().Contains(filterQuery.ToLower()))
                        && (u.RoleId == 4 || u.RoleId == null)
                        && u.CampusId == campusId
                        select new UserResponse
                        {
                            Email = u.Mail,
                            CampusName = c != null ? c.CampusName : null, // Handle possible null from left join
                            IsActive = u.IsActive,
                            RoleName = r != null ? r.RoleName : null,     // Handle possible null from left join
                            UserId = u.UserId,
                            UpdateDt = u.UpdateDate,
                        }).ToList();

            return new ResultResponse<UserResponse>
            {
                IsSuccessful = true,
                Items = data.OrderByDescending(x => x.UpdateDt).ToList(),
            };
        }

        public async Task<List<User>> GetAllWithFilterAsync(string? filterOn = null, string? filterQuery = null, string? sortBy = null, bool isAscending = true)
        {
            var users = dbContext.Users.Include(u => u.Campus).Include(u => u.Role).AsQueryable();

            // Kiểm tra filter
            if (!string.IsNullOrWhiteSpace(filterOn) && !string.IsNullOrWhiteSpace(filterQuery))
            {
                if (filterOn.Equals("Mail", StringComparison.OrdinalIgnoreCase))
                {
                    users = users.Where(x => x.Mail.Contains(filterQuery));
                }
            }

            // Kiểm tra sortBy
            if (!string.IsNullOrWhiteSpace(sortBy))
            {
                if (sortBy.Equals("Mail", StringComparison.OrdinalIgnoreCase))
                {
                    users = isAscending ? users.OrderBy(x => x.Mail) : users.OrderByDescending(x => x.Mail);
                }
            }
            return await users.ToListAsync();
        }

        public async Task<RequestResponse> DeleteAsync(int id)
        {
            var existingUser = await dbContext.Users.FirstOrDefaultAsync(x => x.UserId == id);
            if (existingUser == null)
            {
                return new RequestResponse
                {
                    IsSuccessful = false,
                    Message = "Account no exist"
                };
            }
            dbContext.Users.RemoveRange(existingUser);
            await dbContext.SaveChangesAsync();
            return new RequestResponse
            {
                IsSuccessful = true,
                Message = " Delete success "
            };
        }

        public async Task<ResultResponse<UserRequest>> GetByIdAsync(int id)
        {
            var data = (from u in this.dbContext.Users
                        join c in this.dbContext.Campuses on u.CampusId equals c.CampusId into campusJoin
                        from c in campusJoin.DefaultIfEmpty() // Left join for Campuses
                        join r in this.dbContext.UserRoles on u.RoleId equals r.RoleId into roleJoin
                        from r in roleJoin.DefaultIfEmpty() // Left join for UserRoles
                        where u.UserId == id
                        select new UserRequest
                        {
                            Email = u.Mail.Replace("@fpt.edu.vn", string.Empty),
                            MailFe = u.EmailFe.Replace("@fe.edu.vn", string.Empty),
                            Phone = u.PhoneNumber,
                            UserName = u.FullName,
                            CampusId = u.CampusId,                        // Keep the CampusId from the Users table
                            CampusName = c != null ? c.CampusName : null, // Handle possible null from left join
                            IsActive = u.IsActive,
                            RoleId = u.RoleId,                            // Keep the RoleId from the Users table
                            RoleName = r != null ? r.RoleName : null,     // Handle possible null from left join
                            UserId = u.UserId,
                        }).FirstOrDefault();
            return new ResultResponse<UserRequest>
            {
                IsSuccessful = true,
                Item = data,
            };
        }

        public async Task<ResultResponse<UserSubjectRequest>> GetUserSubjectByIdAsync(int id)
        {
            var data = (from u in this.dbContext.Users
                        join c in this.dbContext.Campuses on u.CampusId equals c.CampusId into campusJoin
                        from c in campusJoin.DefaultIfEmpty() // Left join for Campuses
                        join r in this.dbContext.UserRoles on u.RoleId equals r.RoleId into roleJoin
                        from r in roleJoin.DefaultIfEmpty() // Left join for UserRoles
                        where u.UserId == id
                        select new UserSubjectRequest
                        {
                            Email = u.Mail.Replace("@fpt.edu.vn", string.Empty),
                            CampusId = u.CampusId,
                            CampusName = c != null ? c.CampusName : null,
                            IsActive = u.IsActive,
                            RoleId = u.RoleId,
                            RoleName = r != null ? r.RoleName : null,
                            UserId = u.UserId,
                            Phone = u.PhoneNumber,
                            UserName = u.FullName,
                            FacutyResponse = (from s in this.dbContext.Faculties
                                              join cus in this.dbContext.CampusUserFaculties on s.FacultyId equals cus.FacultyId
                                              where cus.UserId == u.UserId && cus.CampusId == u.CampusId
                                              select new FacutyResponse
                                              {
                                                  FacultyId = s.FacultyId,
                                                  FacultyName = s.FacultyName,
                                                  Description = s.Description,
                                              }).ToList()
                        }).FirstOrDefault();

            return new ResultResponse<UserSubjectRequest>
            {
                IsSuccessful = true,
                Item = data,
            };
        }
        public async Task<ResultResponse<UserSubjectRequest>> GetUserFacutyByIdAsync(int id)
        {
            var data = (from u in this.dbContext.Users
                        join c in this.dbContext.Campuses on u.CampusId equals c.CampusId into campusJoin
                        from c in campusJoin.DefaultIfEmpty() // Left join for Campuses
                        join r in this.dbContext.UserRoles on u.RoleId equals r.RoleId into roleJoin
                        from r in roleJoin.DefaultIfEmpty() // Left join for UserRoles
                        where u.UserId == id
                        select new UserSubjectRequest
                        {
                            Email = u.Mail.Replace("@fpt.edu.vn", string.Empty),
                            CampusId = u.CampusId,
                            CampusName = c != null ? c.CampusName : null,
                            IsActive = u.IsActive,
                            RoleId = u.RoleId,
                            RoleName = r != null ? r.RoleName : null,
                            UserId = u.UserId,
                            Phone = u.PhoneNumber,
                            UserName = u.FullName,
                            FacutyResponse = (from s in this.dbContext.Faculties
                                              join cus in this.dbContext.CampusUserFaculties on s.FacultyId equals cus.FacultyId
                                              where cus.UserId == u.UserId && cus.CampusId == u.CampusId
                                              select new FacutyResponse
                                              {
                                                  FacultyId = s.FacultyId,
                                                  FacultyName = s.FacultyName,
                                                  Description = s.Description,
                                              }).ToList()
                        }).FirstOrDefault();

            return new ResultResponse<UserSubjectRequest>
            {
                IsSuccessful = true,
                Item = data,
            };
        }


        public async Task<RequestResponse> UpdateAsync(UserRequest user)
        {
            try
            {
                RequestResponse response = new RequestResponse();

                var existingUser = await dbContext.Users.FirstOrDefaultAsync(x => x.UserId == user.UserId);
                if (existingUser == null)
                {
                    return new RequestResponse
                    {
                        IsSuccessful = false,
                        Message = "User not exist"
                    };
                }
                existingUser.Mail = user.Email + "@fpt.edu.vn";
                existingUser.FullName = user.UserName;
                existingUser.PhoneNumber = user.Phone;
                existingUser.RoleId = user.RoleId;
                existingUser.CampusId = user.CampusId;
                existingUser.IsActive = user.IsActive.Value;

                await dbContext.SaveChangesAsync();
                response.IsSuccessful = true;

                response.Message = "Update account successfuly";
                await logRepository.LogAsync($"Update user {user.Email}");
                return response;
            }
            catch (Exception ex)
            {
                return new RequestResponse
                {
                    Message = ex.Message
                };
            }
        }

        public async Task<RequestResponse> ExaminerUpdateUserAsync(UserSubjectRequest user)
        {
            try
            {
                RequestResponse response = new RequestResponse();

                var existingUser = await dbContext.Users.FirstOrDefaultAsync(x => x.UserId == user.UserId);


                if (existingUser == null)
                {
                    return new RequestResponse
                    {
                        IsSuccessful = false,
                        Message = "User not exist"
                    };
                }
                existingUser.Mail = user.Email + "@fpt.edu.vn";
                existingUser.RoleId = user.RoleId;
                existingUser.CampusId = user.CampusId;
                existingUser.IsActive = user.IsActive.Value;
                existingUser.FullName = user.UserName;
                existingUser.PhoneNumber = user.Phone;

                var currentFacuti = this.dbContext.CampusUserFaculties
                .Where(cus => cus.UserId == user.UserId && cus.CampusId == user.CampusId)
                 .Select(cus => cus.FacultyId)
                .ToList();

                // Lọc các SubjectId mới, loại bỏ null
                var validNewFacutyIds = user.FacutyResponse.Select(id => id.FacultyId).ToList();

                // Tìm các SubjectId cần thêm
                List<int> facutiesToAdd = new List<int>();
                foreach (var FacultyId in validNewFacutyIds)
                {
                    if (!currentFacuti.Contains(FacultyId))
                    {
                        facutiesToAdd.Add(FacultyId); // Thêm vào danh sách cần thêm nếu không có trong currentSubjectIds
                    }
                }

                // Tìm các SubjectId cần xóa
                List<int> facutiesToRemove = new List<int>();
                foreach (var FacultyId in currentFacuti)
                {
                    if (!validNewFacutyIds.Contains(FacultyId.Value))
                    {
                        facutiesToRemove.Add(FacultyId.Value); // Thêm vào danh sách cần xóa nếu không có trong newSubjectIds
                    }
                }

                // Xóa các môn học đã bị loại bỏ khỏi CampusUserSubjects
                var campusUserFacutiesToRemove = this.dbContext.CampusUserFaculties
                    .Where(cus => cus.UserId == user.UserId && cus.CampusId == user.CampusId && facutiesToRemove.Contains(cus.FacultyId.Value))
                    .ToList();

                if (campusUserFacutiesToRemove.Any())
                {
                    this.dbContext.CampusUserFaculties.RemoveRange(campusUserFacutiesToRemove);
                    this.dbContext.SaveChanges(); // Lưu thay đổi
                }

                // Thêm các môn học mới vào CampusUserSubjects
                var newCampusUserFacutiess = facutiesToAdd.Select(facutyID => new CampusUserFaculty
                {
                    UserId = user.UserId,
                    FacultyId = facutyID,
                    CampusId = user.CampusId,
                    //IsLecturer = user.RoleId == 3 ? false : true // Đặt giá trị true nếu là giảng viên, false nếu là chủ nhiệm
                }).ToList();

                if (newCampusUserFacutiess.Any())
                {
                    this.dbContext.CampusUserFaculties.AddRange(newCampusUserFacutiess);
                    this.dbContext.SaveChanges(); // Lưu thay đổi
                }

                await dbContext.SaveChangesAsync();
                response.IsSuccessful = true;

                response.Message = "Update account successfuly";
                return response;
            }
            catch (Exception ex)
            {
                return new RequestResponse
                {
                    Message = ex.Message
                };
            }
        }

        public async Task<ResultResponse<UserResponse>> GetHeadOfDepartment(int subjectId, int campusId)
        {
            try
            {
                var user = await (from u in dbContext.Users
                                  join cus in dbContext.CampusUserSubjects on u.UserId equals cus.UserId
                                  join s in dbContext.Subjects on cus.SubjectId equals s.SubjectId
                                  join c in dbContext.Campuses on cus.CampusId equals c.CampusId
                                  where s.SubjectId == subjectId && c.CampusId == campusId
                                  select new UserResponse
                                  {
                                      Email = u.Mail,
                                      UserId = u.UserId,
                                  }).FirstOrDefaultAsync();

                if (user == null)
                {
                    return new ResultResponse<UserResponse>
                    {
                        IsSuccessful = true,
                        Message = "Cannot find Head of Department.",
                    };
                }

                return new ResultResponse<UserResponse>
                {
                    IsSuccessful = true,
                    Item = user
                };
            }
            catch (Exception ex)
            {
                return new ResultResponse<UserResponse>
                {
                    IsSuccessful = false,
                    Message = ex.Message
                };
            }
        }

        public async Task<ResultResponse<UserResponse>> GetLectureBySubject(int subjectId, int campusId)
        {
            var data = await (from u in this.dbContext.Users
                              join cus in this.dbContext.CampusUserSubjects on u.UserId equals cus.UserId
                              where u.CampusId == campusId
                              && cus.SubjectId == subjectId
                              select new UserResponse
                              {
                                  Email = u.Mail,
                                  UserId = u.UserId,
                              }).ToListAsync();

            return new ResultResponse<UserResponse>
            {
                IsSuccessful = true,
                Items = data,
            };
        }
        private bool IsValidFptEmail(string email)
        {
            try
            {
                // Kiểm tra định dạng email
                var addr = new System.Net.Mail.MailAddress(email);

                // Kiểm tra xem email có kết thúc bằng .fpt.edu.vn không
                if (addr.Address == email && email.EndsWith("@fpt.edu.vn"))
                {
                    return true;
                }
                return false;
            }
            catch
            {
                return false;
            }
        }

        private bool IsValidFeEmail(string email)
        {
            try
            {
                // Kiểm tra định dạng email
                var addr = new System.Net.Mail.MailAddress(email);

                // Kiểm tra xem email có kết thúc bằng .fpt.edu.vn không
                if (addr.Address == email && email.EndsWith("@fe.edu.vn"))
                {
                    return true;
                }
                return false;
            }
            catch
            {
                return false;
            }
        }
        // Phương thức nhập cho Examiner
        private async Task<bool> ImportForExaminer(List<string> facultyOrSubjectList, User user, int? currentUserCampusId)
        {
            var facultiesToAdd = new List<CampusUserFaculty>(); // Danh sách các đối tượng cần thêm vào DB
            var errors = new List<string>(); // Danh sách lỗi (nếu có)

            foreach (var facultyName in facultyOrSubjectList)
            {
                var faculty = await dbContext.Faculties
                    .FirstOrDefaultAsync(f => f.FacultyName.ToLower() == facultyName);

                if (faculty == null)
                {
                    // Thêm lỗi nếu bộ môn không tồn tại
                    errors.Add($"Bộ môn '{facultyName}' không tồn tại.");
                    continue;
                }


                // Kiểm tra nếu bộ môn đã có người quản lý tại campus cụ thể này
                var existingCampusUserFacultyRecord = await dbContext.CampusUserFaculties
                    .FirstOrDefaultAsync(c => c.FacultyId == faculty.FacultyId && c.CampusId == currentUserCampusId && c.UserId != null);

                if (existingCampusUserFacultyRecord != null)
                {
                    // Thêm lỗi nếu bộ môn này tại campus đã có người quản lý (UserId khác null)
                    errors.Add($"Bộ môn '{facultyName}' tại campus này đã có người quản lý.");
                    continue;
                }

                var existingCampusUserFaculty = await dbContext.CampusUserFaculties
                    .FirstOrDefaultAsync(c => c.UserId == user.UserId && c.FacultyId == faculty.FacultyId && c.CampusId == currentUserCampusId);

                if (existingCampusUserFaculty != null)
                {
                    // Thêm lỗi nếu bản ghi đã tồn tại
                    errors.Add($"Bản ghi của User '{user.FullName}' với Bộ môn '{facultyName}' đã tồn tại.");
                    continue;
                }

                facultiesToAdd.Add(new CampusUserFaculty
                {
                    CampusId = currentUserCampusId,
                    UserId = user.UserId,
                    FacultyId = faculty.FacultyId
                });
            }

            if (facultiesToAdd.Any())
            {
                try
                {
                    await dbContext.CampusUserFaculties.AddRangeAsync(facultiesToAdd);
                    await dbContext.SaveChangesAsync();
                }
                catch (Exception ex)
                {
                    // Xử lý lỗi khi lưu vào DB và trả về false nếu có lỗi
                    Console.WriteLine($"Lỗi khi lưu vào cơ sở dữ liệu: {ex.Message}");
                    return false;
                }
            }

            // Nếu có lỗi, trả về false
            if (errors.Any())
            {
                foreach (var error in errors)
                {
                    Console.WriteLine(error);
                }
                return false;
            }

            // Nếu không có lỗi, trả về true
            return true;
        }

        // Phương thức nhập cho Head of Department
        private async Task<bool> ImportForHeadOfDepartment(List<string> facultyOrSubjectList, User user, int? currentUserCampusId)
        {
            var subjectsToAdd = new List<CampusUserSubject>(); // Danh sách các đối tượng cần thêm vào DB
            var errors = new List<string>(); // Danh sách lỗi (nếu có)

            foreach (var subjectCode in facultyOrSubjectList)
            {
                var subject = await dbContext.Subjects
                    .FirstOrDefaultAsync(s => s.SubjectCode.ToLower() == subjectCode);

                if (subject == null)
                {
                    // Thêm lỗi nếu môn học không tồn tại
                    errors.Add($"Môn học '{subjectCode}' không tồn tại.");
                    continue;
                }

                // Kiểm tra bản ghi đã tồn tại trong bảng CampusUserSubject
                var existingCampusUserSubject = await dbContext.CampusUserSubjects
                    .FirstOrDefaultAsync(c => c.UserId == user.UserId && c.SubjectId == subject.SubjectId && c.CampusId == currentUserCampusId);

                if (existingCampusUserSubject != null)
                {
                    // Thêm lỗi nếu bản ghi đã tồn tại
                    errors.Add($"Bản ghi của User '{user.FullName}' với Môn học '{subjectCode}' đã tồn tại.");
                    continue;
                }

                // Nếu không tồn tại, thêm bản ghi mới
                subjectsToAdd.Add(new CampusUserSubject
                {
                    CampusId = currentUserCampusId,
                    UserId = user.UserId,
                    SubjectId = subject.SubjectId
                });
            }

            // Nếu có bản ghi cần thêm, lưu vào DB
            if (subjectsToAdd.Any())
            {
                try
                {
                    await dbContext.CampusUserSubjects.AddRangeAsync(subjectsToAdd);
                    await dbContext.SaveChangesAsync();
                }
                catch (Exception ex)
                {
                    // Xử lý lỗi khi lưu vào DB
                    Console.WriteLine($"Lỗi khi lưu vào cơ sở dữ liệu: {ex.Message}");
                    return false;
                }
            }

            // Nếu có lỗi, trả về false
            if (errors.Any())
            {
                foreach (var error in errors)
                {
                    Console.WriteLine(error);
                }
                return false;
            }

            // Nếu không có lỗi, trả về true
            return true;
        }

        public async Task<ResultResponse<AccountClaims>> GetCurrentUserInfoAsync(ClaimsPrincipal currentUser)
        {
            // Lấy thông tin người dùng hiện tại từ Claims
            var userId = int.TryParse(currentUser.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier)?.Value, out var id) ? id : 0;

            // Có được id của người dùng từ hệ thống thì liên kết tới database
            var myUser = await dbContext.Users.FirstOrDefaultAsync(u => u.UserId == userId);
            if (myUser == null)
            {
                return new ResultResponse<AccountClaims>
                {
                    IsSuccessful = false,
                    Message = "User not found."
                };
            }

            // Lấy RoleId và CampusId từ đối tượng người dùng
            var currentUserRoleId = myUser.RoleId;
            var currentUserCampusId = myUser.CampusId;

            // Tạo đối tượng AccountClaims
            var accountClaims = new AccountClaims
            {
                Id = userId,
                RoleId = currentUserRoleId,
                Email = myUser.Mail,
                FirstName = myUser.FullName,
                CampusId = currentUserCampusId
            };

            return new ResultResponse<AccountClaims>
            {
                IsSuccessful = true,
                Item = accountClaims
            };
        }
        // Hàm để kiểm tra tính hợp lệ của số điện thoại
        private bool IsValidPhoneNumber(string phoneNumber)
        {
            // Xóa khoảng trắng và ký tự đặc biệt
            var cleanedNumber = new string(phoneNumber.Where(char.IsDigit).ToArray());

            // Kiểm tra xem số điện thoại có chứa tất cả các ký tự là số hay không
            if (cleanedNumber.Length == 0)
            {
                return false;
            }

            // Kiểm tra độ dài số điện thoại
            if (cleanedNumber.Length < 9 || cleanedNumber.Length > 11)
            {
                return false;
            }

            // Kiểm tra xem có bắt đầu bằng mã vùng hợp lệ
            // Ví dụ: mã vùng di động tại Việt Nam có thể là 09xx, 01xx, 03xx, 07xx, 08xx
            // Nếu bạn có các mã vùng khác, hãy thêm vào danh sách này
            var validPrefixes = new[] { "9", "1", "3", "7", "8" };

            // Kiểm tra xem số điện thoại có bắt đầu bằng các mã vùng hợp lệ
            if (!validPrefixes.Any(prefix => cleanedNumber.StartsWith(prefix)))
            {
                return false;
            }

            return true;
        }
        public async Task<RequestResponse> ImportUsersFromExcel(IFormFile file, ClaimsPrincipal currentUser)
        {
            //khai báo biến
            var response = new RequestResponse();
            var errors = new List<string>();
            var usersToAdd = new List<User>();
            var campusUserFacultiesToAdd = new List<CampusUserFaculty>();
            var campusUserSubjectsToAdd = new List<CampusUserSubject>();
            // Tạo một HashSet để theo dõi các bản ghi đã được thêm
            var existingUserSet = new HashSet<string>();

            try
            {
                // Đăng ký mã hóa hỗ trợ cho Excel
                System.Text.Encoding.RegisterProvider(System.Text.CodePagesEncodingProvider.Instance);

                // Kiểm tra nếu file không tồn tại hoặc không có dữ liệu
                if (file == null || file.Length == 0)
                {
                    return new RequestResponse
                    {
                        IsSuccessful = false,
                        Message = "No file uploaded!",
                    };
                }

                var userInfoResponse = await GetCurrentUserInfoAsync(currentUser);
                if (!userInfoResponse.IsSuccessful)
                {
                    return new ResultResponse<AccountClaims>
                    {
                        IsSuccessful = false,
                        Message = "Failed to retrieve user information. Please try again later."
                    };
                }

                var currentUserRoleId = userInfoResponse.Item.RoleId;
                var currentUserCampusId = userInfoResponse.Item.CampusId;

                // Lấy RoleName dựa trên RoleId
                var currentUserRoleName = await dbContext.UserRoles
                    .Where(r => r.RoleId == currentUserRoleId)
                    .Select(r => r.RoleName)
                    .FirstOrDefaultAsync();

                if (string.IsNullOrEmpty(currentUserRoleName))
                {
                    return new ResultResponse<AccountClaims>
                    {
                        IsSuccessful = false,
                        Message = "RoleName not found for the given RoleId."
                    };
                }

                // Thiết lập thư mục lưu file upload
                var uploadsFolder = $"{Directory.GetCurrentDirectory()}\\Uploads\\Users";
                if (!Directory.Exists(uploadsFolder))
                {
                    Directory.CreateDirectory(uploadsFolder);
                }

                // Đường dẫn file
                var filePath = Path.Combine(uploadsFolder, Path.GetFileName(file.FileName));

                // Ghi file lên server
                using (var stream = new FileStream(filePath, FileMode.Create))
                {
                    await file.CopyToAsync(stream);
                }

                using (var stream = System.IO.File.Open(filePath, FileMode.Open, FileAccess.Read))
                {
                    using (var reader = ExcelReaderFactory.CreateReader(stream))
                    {
                        bool isHeaderSkipped = false;

                        do
                        {
                            while (reader.Read())
                            {
                                // Bỏ qua hàng tiêu đề (header) đầu tiên
                                if (!isHeaderSkipped)
                                {
                                    isHeaderSkipped = true;
                                    continue;  // Bỏ qua hàng tiêu đề và tiếp tục với các hàng sau
                                }

                                // Kiểm tra dòng có rỗng không (ví dụ: cột Email và FullName phải có dữ liệu)
                                if (reader.IsDBNull(1) || string.IsNullOrWhiteSpace(reader.GetValue(1)?.ToString()) ||
                                    reader.IsDBNull(2) || string.IsNullOrWhiteSpace(reader.GetValue(2)?.ToString()))
                                {
                                    // Nếu dòng rỗng, in thông báo để dễ kiểm tra, bỏ qua
                                    Console.WriteLine("Dòng rỗng hoặc thiếu dữ liệu, bỏ qua.");
                                    continue;
                                }

                                // Đọc dữ liệu từ các cột sau hàng tiêu đề
                                var userImportRequest = new UserImportRequest
                                {
                                    Mail = reader.GetValue(1)?.ToString(),  // Cột Email
                                    FullName = reader.GetValue(2)?.ToString(),  // Cột FullName
                                    PhoneNumber = reader.GetValue(3)?.ToString(),  // Cột PhoneNumber
                                    EmailFe = reader.GetValue(4)?.ToString(),  // Cột EmailFe (có thể để trống cho một số roles)
                                    DateOfBirth = reader.GetValue(5)?.ToString(),  // Cột DateOfBirth
                                    Gender = reader.GetValue(6)?.ToString()?.ToLower(),  // Cột Gender
                                    Address = reader.GetValue(7)?.ToString(),  // Cột Address
                                    FacultyOrSubjectInCharge = (currentUserRoleName == "Examiner" || currentUserRoleName == "Head of Department") // Kiểm tra quyền Examiner hoặc Head of Department
                                    ? (string.IsNullOrEmpty(reader.GetValue(8)?.ToString()?.Trim()) ? null : reader.GetValue(8)?.ToString()?.Trim()) // Kiểm tra cột 8
                                    : null, // Nếu không phải Examiner hoặc Head of Department, gán null cho FacultyOrSubjectInCharge
                                };


                                var errorMessages = new List<string>();

                                //kiểm tra định dang của mail

                                if (userImportRequest.Mail.Length > 255)
                                {
                                    errorMessages.Add("Email must not exceed 255 characters.");
                                }

                                if (!IsValidFptEmail(userImportRequest.Mail))
                                {
                                    errorMessages.Add($"This email '{userImportRequest.Mail}' is not valid.");
                                }

                                // kiểm tra định dạng của fullName
                                if (userImportRequest.FullName.Length > 100)
                                {
                                    errorMessages.Add("Full Name must not exceed 100 characters.");
                                }


                                // kiểm tra định dạng của phoneNumber
                                if (!IsValidPhoneNumber(userImportRequest.PhoneNumber))
                                {
                                    errorMessages.Add($"This PhoneNumber '{userImportRequest.PhoneNumber}' is not valid. Please ensure it follows Vietnamese standards.");
                                }



                                // Kiểm tra vai trò của người dùng để quyết định vai trò nào được phép import
                                string? targetRoleName = currentUserRoleName switch
                                {
                                    "Admin" => "Examiner",  // Admin có thể import Examiner
                                    "Examiner" => "Head of Department",  // Examiner có thể import Head of Department
                                    "Head of Department" => "Lecturer",  // Head of Department có thể import Lecturer
                                    _ => null  // Các vai trò khác không được phép import
                                };

                                // Kiểm tra xem vai trò mục tiêu có hợp lệ không
                                if (targetRoleName == null)
                                {
                                    return new RequestResponse
                                    {
                                        IsSuccessful = false,
                                        Message = "Your role does not allow you to import users with specific roles."
                                    };
                                }

                                // Lấy RoleId của vai trò mục tiêu
                                var targetRoleId = await dbContext.UserRoles
                                    .Where(r => r.RoleName == targetRoleName)
                                    .Select(r => r.RoleId)
                                    .FirstOrDefaultAsync();

                                // Xử lý DateOfBirth


                                DateTime? dobValue = null;
                                if (!string.IsNullOrEmpty(userImportRequest.DateOfBirth))
                                {
                                    // Thử phân tích cú pháp với định dạng cụ thể
                                    var formats = new[] { "dd-MM-yyyy", "d/M/yyyy", "d/MM/yyyy", "dd/MM/yyyy", "MM-dd-yyyy" }; // Thêm các định dạng khác nếu cần
                                    if (DateTime.TryParseExact(userImportRequest.DateOfBirth, formats, CultureInfo.InvariantCulture, DateTimeStyles.None, out DateTime parsedDob))
                                    {
                                        dobValue = parsedDob;
                                    }
                                    else
                                    {
                                        errorMessages.Add($"This DateOfBirth '{userImportRequest.DateOfBirth}' is not valid.");
                                    }
                                }

                                if (dobValue.HasValue)
                                {
                                    if (dobValue.Value > DateTime.Now)
                                    {
                                        errorMessages.Add("Date of Birth cannot be in the future.");
                                    }
                                    if ((DateTime.Now.Year - dobValue.Value.Year) > 120)
                                    {
                                        errorMessages.Add("Date of Birth is not valid. User cannot be older than 120 years.");
                                    }
                                }

                                // Xử lý chuyển đổi Gender
                                bool? gender = null;
                                if (!string.IsNullOrEmpty(userImportRequest.Gender))
                                {
                                    if (userImportRequest.Gender.Equals("male", StringComparison.OrdinalIgnoreCase))
                                    {
                                        gender = true;
                                    }
                                    else if (userImportRequest.Gender.Equals("female", StringComparison.OrdinalIgnoreCase))
                                    {
                                        gender = false;
                                    }
                                    else
                                    {
                                        errorMessages.Add($"This gender '{userImportRequest.Gender}' is not valid. Please input 'Male' or 'Female'.");
                                    }
                                }

                                // Kiểm tra vai trò của người dùng để validate EmailFe
                                if ((targetRoleName == "Lecturer" || targetRoleName == "Head of Department" || targetRoleName == "Examiner"))
                                {
                                    // Vai trò "Lecturer" và "Head of Department" thì được nhập EmailFe
                                    if (!string.IsNullOrEmpty(userImportRequest.EmailFe) && !IsValidFeEmail(userImportRequest.EmailFe))
                                    {
                                        errorMessages.Add($"This EmailFe '{userImportRequest.EmailFe}' is not valid.");
                                    }
                                }
                                else
                                {
                                    // Các vai trò khác không được nhập EmailFe
                                    if (!string.IsNullOrEmpty(userImportRequest.EmailFe))
                                    {
                                        errorMessages.Add($"Role '{targetRoleName}' is not allowed to have EmailFe.");
                                    }
                                }


                                // Tạo khóa duy nhất cho mỗi người dùng
                                string uniqueKey = $"{userImportRequest.Mail}_{userImportRequest.FullName}_{userImportRequest.PhoneNumber}";
                                // Kiểm tra xem người dùng đã tồn tại trong HashSet chưa
                                if (existingUserSet.Contains(uniqueKey))
                                {
                                    errors.Add($"Duplicate entry for Mail '{userImportRequest.Mail}', FullName '{userImportRequest.FullName}', and PhoneNumber '{userImportRequest.PhoneNumber}'.");
                                    continue; // Bỏ qua bản ghi trùng lặp
                                }

                                // Thêm vào HashSet nếu không trùng lặp
                                existingUserSet.Add(uniqueKey);
                                // Kiểm tra xem người dùng đã tồn tại hay chưa

                                var normalizedMail = userImportRequest.Mail?.Trim().ToLower();
                                var normalizedFullName = userImportRequest.FullName?.Trim().ToLower();
                                var normalizedPhoneNumber = userImportRequest.PhoneNumber?.Trim();

                                var existingUser = await dbContext.Users
                                    .FirstOrDefaultAsync(u => u.Mail.ToLower() == normalizedMail
                                                             && u.FullName.ToLower() == normalizedFullName
                                                             && u.PhoneNumber == normalizedPhoneNumber);

                                if (existingUser != null)
                                {
                                    errorMessages.Add($"User with Mail '{userImportRequest.Mail}' already exists.");
                                    continue;
                                }

                                if (errorMessages.Any())
                                {
                                    errors.Add($"Error with this Mail {userImportRequest.Mail} : {string.Join(", ", errorMessages)}");
                                    continue;
                                }



                                // Tạo User nếu không có lỗi
                                var user = new User
                                {
                                    Mail = userImportRequest.Mail,
                                    CampusId = currentUserCampusId,
                                    RoleId = targetRoleId,
                                    FullName = userImportRequest.FullName,
                                    PhoneNumber = userImportRequest.PhoneNumber,
                                    EmailFe = userImportRequest.EmailFe,
                                    DateOfBirth = dobValue,
                                    Gender = gender,
                                    Address = userImportRequest.Address,
                                    IsActive = true,
                                    CreateDate = DateTime.Now
                                };

                                usersToAdd.Add(user);

                                try
                                {
                                    // Lưu người dùng vào dbContext
                                    await dbContext.Users.AddAsync(user);
                                    await dbContext.SaveChangesAsync();


                                    // Kiểm tra quyền của người dùng hiện tại
                                    if (currentUserRoleName == "Admin")
                                    {
                                        // Với quyền Admin, chỉ lưu user vào hệ thống mà không cần xử lý FacultyOrSubjectInCharge
                                        await dbContext.SaveChangesAsync();

                                    }
                                    else
                                    {
                                        // Lấy danh sách các bộ môn hoặc môn học từ cột "FacultyOrSubjectInCharge" (cột 7)
                                        var facultyOrSubjectList = userImportRequest.FacultyOrSubjectInCharge
                                            .Split(new[] { ',', ';' }, StringSplitOptions.RemoveEmptyEntries)
                                            .Select(f => f.Trim().ToLower())
                                            .ToList();

                                        bool importSuccess = false;

                                        if (currentUserRoleName == "Examiner")
                                        {
                                            // Gọi phương thức ImportForExaminer để xử lý nhập dữ liệu cho Examiner
                                            importSuccess = await ImportForExaminer(facultyOrSubjectList, user, currentUserCampusId);
                                        }
                                        else if (currentUserRoleName == "Head of Department")
                                        {
                                            // Gọi phương thức ImportForHeadOfDepartment để xử lý nhập dữ liệu cho Head of Department
                                            importSuccess = await ImportForHeadOfDepartment(facultyOrSubjectList, user, currentUserCampusId);
                                        }

                                        if (importSuccess)
                                        {
                                            // Nếu nhập dữ liệu thành công, lưu thay đổi vào DB
                                            await dbContext.SaveChangesAsync();
                                        }
                                        else
                                        {
                                            // Nếu không thành công, có thể báo lỗi và không lưu thay đổi
                                            errors.Add("Nhập dữ liệu không thành công. Quá trình bị hủy.");
                                        }
                                    }

                                }
                                catch (Exception ex)
                                {
                                    // Xử lý lỗi
                                    errors.Add($"Đã xảy ra lỗi: {ex.Message}");
                                }
                            }
                        } while (reader.NextResult());
                    }
                }
                if (errors.Any())
                {
                    response.IsSuccessful = false;
                    response.Message = $"There are the following errors: {string.Join("; ", errors)}";
                }
                else
                {
                    response.IsSuccessful = true;
                    response.Message = $"Successfully added {usersToAdd.Count} users";
                }

            }
            catch (Exception ex)
            {
                response.IsSuccessful = false;
                response.Message = $"An error occurred: {ex.Message}";
            }

            return response;
        }
        public async Task<ResultResponse<UserResponse>> GetAssignedUserByExam(int examId)
        {
            try
            {
                var data = (from e in dbContext.Exams
                            join u in dbContext.Users on e.AssignedUserId equals u.UserId
                            where e.ExamId == examId
                            select new UserResponse
                            {
                                UserId = u.UserId,
                                UserName = u.FullName,
                                Email = u.Mail
                            }).ToList();

                return new ResultResponse<UserResponse>
                {
                    IsSuccessful = true,
                    Items = data,
                };
            }
            catch (Exception ex)
            {
                return new ResultResponse<UserResponse>
                {
                    IsSuccessful = false,
                    Message = ex.Message,
                };
            }
        }

        public async Task<ResultResponse<UserResponse>> GetLectureListByHead(int userId)
        {
            var campusId = (await this.dbContext.Users.FirstOrDefaultAsync(x => x.UserId == userId))?.CampusId;
            var facutiID = (await this.dbContext.CampusUserFaculties.FirstOrDefaultAsync(x => x.UserId == userId))?.FacultyId;
            try
            {
                var data = (from u in dbContext.Users
                            join cus in dbContext.CampusUserSubjects on u.UserId equals cus.UserId
                            join sj in dbContext.Subjects on cus.SubjectId equals sj.SubjectId
                            join cuf in dbContext.CampusUserFaculties on sj.FacultyId equals cuf.FacultyId
                            where cus.CampusId == cuf.CampusId
                            && cuf.UserId == userId
                            select new UserResponse
                            {
                                UserId = u.UserId,
                                UserName = u.FullName,
                                Email = u.Mail,
                                Tel = u.PhoneNumber,
                                IsActive = u.IsActive,
                            })
                            .Distinct()
                            .ToList();

                return new ResultResponse<UserResponse>
                {
                    IsSuccessful = true,
                    Items = data,
                };
            }
            catch (Exception ex)
            {
                return new ResultResponse<UserResponse>
                {
                    IsSuccessful = false,
                    Message = ex.Message,
                };
            }
        }

        public string GenerateToken(User acc)
        {
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(config["Jwt:Key"]!));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);
            var userClaims = new[]
            {
                new Claim(ClaimTypes.Email, acc.Mail!),
                new Claim(ClaimTypes.NameIdentifier, acc.UserId.ToString()),
                new Claim(ClaimTypes.Role,acc.RoleId.ToString()!),
                new Claim("CampusId",acc.CampusId.ToString()!),
            };

            var token = new JwtSecurityToken(
                issuer: config["Jwt:Issuer"],
                audience: config["Jwt:Audience"],
                claims: userClaims,
                expires: DateTime.Now.AddDays(2),
                signingCredentials: credentials
                );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }

        public async Task<AuthenticationResponse> GoogleLoginCallback(string code)
        {
            try
            {
                AuthenticationResponse response = new AuthenticationResponse();

                var tokenResponse = await GetGoogleTokenAsync(code);

                if (!string.IsNullOrEmpty(tokenResponse.AccessToken))
                {
                    var userInfo = await GetGoogleUserInfoAsync(tokenResponse.AccessToken);

                    if (userInfo != null)
                    {
                        var user = await FindUserAsync(userInfo.Email);

                        if (user != null)
                        {
                            var token = GenerateToken(user);
                            await logRepository.LogAsync("Login in into system");
                            return new AuthenticationResponse
                            {
                                IsSuccessful = true,
                                Token = token
                            };
                        }
                        else
                        {
                            return new AuthenticationResponse
                            {
                                IsSuccessful = false,
                                Message = "Your account does not exist in the system"
                            };
                        }
                    }
                } else {
                    return new AuthenticationResponse
                    {
                        IsSuccessful = false,
                        Message = "No Access token."
                    };
                }

                return new AuthenticationResponse
                {
                    IsSuccessful = false,
                    Message = "Login with Google failed."
                };
            }
            catch (Exception ex)
            {
                return new AuthenticationResponse
                {
                    IsSuccessful = false,
                    Message = ex.Message
                };
            }
        }

        private async Task<User?> FindUserAsync(string email)
        {
            try
            {
                var user = await dbContext.Users.FirstOrDefaultAsync(x => x.Mail.Equals(email) && x.IsActive);
                return user;
            }
            catch (Exception ex)
            {
                return null;
            }

        }

        private async Task<TokenResponse> GetGoogleTokenAsync(string code)
        {
            using var client = new HttpClient();
            var request = new HttpRequestMessage(HttpMethod.Post, "https://oauth2.googleapis.com/token");
            var content = new FormUrlEncodedContent(new Dictionary<string, string>
            {
                { "code", code },
                { "client_id", config["GoogleKeys:ClientId"] },
                { "client_secret", config["GoogleKeys:ClientSecret"] },
                { "redirect_uri", $"{config["BaseUri"]}api/user/googlelogincallback" },
                { "grant_type", "authorization_code" }
            });

            request.Content = content;
            var response = await client.SendAsync(request);
            var responseContent = await response.Content.ReadAsStringAsync();

            return JsonConvert.DeserializeObject<TokenResponse>(responseContent);
        }

        private async Task<GoogleUserInfo> GetGoogleUserInfoAsync(string accessToken)
        {
            using var client = new HttpClient();
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", accessToken);

            var response = await client.GetAsync("https://www.googleapis.com/oauth2/v2/userinfo");
            if (!response.IsSuccessStatusCode)
            {
                var error = await response.Content.ReadAsStringAsync();
                throw new Exception($"Cant not get user info");
            }

            var json = await response.Content.ReadAsStringAsync();

            return JsonConvert.DeserializeObject<GoogleUserInfo>(json);
        }

        public async Task<ResultResponse<UserResponse>> GetUserBySubject(int subjectid,int campusId)
        {
            try
            {
                var data = (from u in dbContext.Users
                            join cus in dbContext.CampusUserSubjects on u.UserId equals cus.UserId
                            join s in dbContext.Subjects on cus.SubjectId equals s.SubjectId
                            where s.SubjectId == subjectid
                            && u.CampusId == campusId
                            select new UserResponse
                            {
                                UserId = u.UserId,
                                UserName = u.FullName,
                                Tel = u.PhoneNumber,
                                Email = u.Mail,
                                FeEmail = u.EmailFe,
                            }).ToList();

                return new ResultResponse<UserResponse>
                {
                    IsSuccessful = true,
                    Items = data,
                };
            }
            catch (Exception ex)
            {
                return new ResultResponse<UserResponse>
                {
                    IsSuccessful = false,
                    Message = ex.Message,
                };
            }
        }

        public async Task<ResultResponse<AddLecturerSubjectRequest>> GetUserByMail(string mail, int headId)
        {
            try
            {
                var u = await this.dbContext.Users.FirstOrDefaultAsync(x => x.UserId == headId);

                var data = await this.dbContext.Users
                    .Where(x => x.Mail.ToLower() == mail.ToLower())
                    .Select(x => new AddLecturerSubjectRequest
                    {
                        UserId = x.UserId,
                        Mail = x.Mail.Replace("@fpt.edu.vn", string.Empty),
                        FullName = x.FullName,
                        MailFe = x.EmailFe.Replace("@fe.edu.vn", string.Empty),
                        PhoneNumber = x.PhoneNumber,
                        IsExist = true,
                    })
                    .FirstOrDefaultAsync();

                return new ResultResponse<AddLecturerSubjectRequest>
                {
                    IsSuccessful = data != null,
                    Item = data
                };
            }
            catch (Exception ex)
            {
                return new ResultResponse<AddLecturerSubjectRequest>
                {
                    IsSuccessful = false,
                    Message = ex.Message,
                };
            }
        }

        public async Task<RequestResponse> AddUserToSubject(AddLecturerSubjectRequest req)
        {
            try
            {
                RequestResponse response = new RequestResponse();
                var u = await this.dbContext.Users.FirstOrDefaultAsync(x => x.UserId == req.HeadId);


                if (req.IsExist)
                {
                    var existData = await this.dbContext.CampusUserSubjects.FirstOrDefaultAsync(x => x.UserId == req.UserId && x.SubjectId == req.SubjectId);

                    if (existData != null)
                    {
                        return new RequestResponse
                        {
                            IsSuccessful = false,
                            Message = "This lecturer already teaching this subject"
                        };
                    }

                    var newData = new CampusUserSubject
                    {
                        UserId = req.UserId,
                        SubjectId = req.SubjectId,
                        CampusId = u.CampusId,
                    };

                    await this.dbContext.CampusUserSubjects.AddAsync(newData);
                }
                else
                {
                    var newUser = new User
                    {
                        CampusId = u.CampusId,
                        PhoneNumber = req.PhoneNumber,
                        EmailFe = req.MailFe+"@fe.edu.vn",
                        RoleId = 3,
                        FullName = req.FullName,
                        Mail = req.Mail+"@fpt.edu.vn",
                        CreateDate = DateTime.Now,
                        UpdateDate = DateTime.Now,
                        IsActive = true,
                    };

                    await this.dbContext.Users.AddAsync(newUser);
                    await this.dbContext.SaveChangesAsync();

                    var newData = new CampusUserSubject
                    {
                        UserId = newUser.UserId,
                        SubjectId = req.SubjectId,
                        CampusId = newUser.CampusId,
                    };

                    await this.dbContext.CampusUserSubjects.AddAsync(newData);
                }

                await this.dbContext.SaveChangesAsync();

                response.IsSuccessful = true;
                response.Message = "Add lecturer successfully";

                return response;
            }
            catch (Exception ex)
            {
                return new RequestResponse
                {
                    IsSuccessful = false,
                    Message = ex.Message
                };
            }
        }

        public async Task<RequestResponse> EditLecturer(AddLecturerSubjectRequest req)
        {
            try
            {
                RequestResponse response = new RequestResponse();

                var user = await this.dbContext.Users.FirstOrDefaultAsync(x => x.UserId == req.UserId);

                if(user == null)
                {
                    return new RequestResponse
                    {
                        IsSuccessful = false,
                        Message = "Lecturer not found"
                    };
                }

                user.Mail = req.Mail;
                user.EmailFe = req.MailFe;
                user.PhoneNumber = req.PhoneNumber;
                user.FullName = req.FullName;

                await this.dbContext.SaveChangesAsync();

                response.IsSuccessful = true;
                response.Message = "Update lecturer successfully";

                return response;
            }
            catch (Exception ex)
            {
                return new RequestResponse
                {
                    IsSuccessful = false,
                    Message = ex.Message
                };
            }
        }

        public async Task<RequestResponse> RemoveLecture(int userId, int subjectId)
        {
            try
            {
                RequestResponse response = new RequestResponse();

                var data = await this.dbContext.CampusUserSubjects.FirstOrDefaultAsync(x => x.UserId == userId && x.SubjectId == subjectId);

                if(data == null)
                {
                    return new RequestResponse
                    {
                        IsSuccessful = false,
                        Message = "Something wrong!"
                    };
                }

                this.dbContext.CampusUserSubjects.Remove(data);

                await this.dbContext.SaveChangesAsync();

                response.IsSuccessful = true;
                response.Message = "Remove lecturer successfully";

                return response;
            }
            catch (Exception ex)
            {
                return new RequestResponse
                {
                    IsSuccessful = false,
                    Message = ex.Message
                };
            }
        }
    }
}
