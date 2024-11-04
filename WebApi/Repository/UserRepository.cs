using ExcelDataReader;
using Library.Common;
using Library.Models;
using Library.Request;
using Library.Response;
using Microsoft.EntityFrameworkCore;
using System.Globalization;
using System.Security.Claims;
using WebApi.IRepository;

namespace WebApi.Repository
{
    public class UserRepository : IUserRepository
    {
        private readonly QuizManagementContext dbContext;
        private readonly ILogHistoryRepository logRepository;

        public UserRepository(QuizManagementContext dbContext, ILogHistoryRepository logRepository)
        {
            this.dbContext = dbContext;
            this.logRepository = logRepository;
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
                        && (u.RoleId != 1 && u.RoleId != 2 && u.RoleId != 5 || u.RoleId == null)
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
                            SubjectResponses = (from s in this.dbContext.Subjects
                                                join cus in this.dbContext.CampusUserSubjects on s.SubjectId equals cus.SubjectId
                                                where cus.UserId == u.UserId && cus.CampusId == u.CampusId
                                                select new SubjectResponse
                                                {
                                                    SubjectId = s.SubjectId,
                                                    SubjectCode = s.SubjectCode,
                                                    SubjectName = s.SubjectName,
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

                var currentSubjectIds = this.dbContext.CampusUserSubjects
                .Where(cus => cus.UserId == user.UserId && cus.CampusId == user.CampusId)
                 .Select(cus => cus.SubjectId)
                .ToList();

                // Lọc các SubjectId mới, loại bỏ null
                var validNewSubjectIds = user.SubjectResponses.Select(id => id.SubjectId).ToList();

                // Tìm các SubjectId cần thêm
                List<int> subjectsToAdd = new List<int>();
                foreach (var subjectId in validNewSubjectIds)
                {
                    if (!currentSubjectIds.Contains(subjectId))
                    {
                        subjectsToAdd.Add(subjectId); // Thêm vào danh sách cần thêm nếu không có trong currentSubjectIds
                    }
                }

                // Tìm các SubjectId cần xóa
                List<int> subjectsToRemove = new List<int>();
                foreach (var subjectId in currentSubjectIds)
                {
                    if (!validNewSubjectIds.Contains(subjectId.Value))
                    {
                        subjectsToRemove.Add(subjectId.Value); // Thêm vào danh sách cần xóa nếu không có trong newSubjectIds
                    }
                }

                // Xóa các môn học đã bị loại bỏ khỏi CampusUserSubjects
                var campusUserSubjectsToRemove = this.dbContext.CampusUserSubjects
                    .Where(cus => cus.UserId == user.UserId && cus.CampusId == user.CampusId && subjectsToRemove.Contains(cus.SubjectId.Value))
                    .ToList();

                if (campusUserSubjectsToRemove.Any())
                {
                    this.dbContext.CampusUserSubjects.RemoveRange(campusUserSubjectsToRemove);
                    this.dbContext.SaveChanges(); // Lưu thay đổi
                }

                // Thêm các môn học mới vào CampusUserSubjects
                var newCampusUserSubjects = subjectsToAdd.Select(subjectId => new CampusUserSubject
                {
                    UserId = user.UserId,
                    SubjectId = subjectId,
                    CampusId = user.CampusId,
                    IsLecturer = user.RoleId == 3 ? false : true // Đặt giá trị true nếu là giảng viên, false nếu là chủ nhiệm
                }).ToList();

                if (newCampusUserSubjects.Any())
                {
                    this.dbContext.CampusUserSubjects.AddRange(newCampusUserSubjects);
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
                              where cus.CampusId == campusId
                              && cus.SubjectId == campusId
                              && cus.IsLecturer == true
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
        private bool IsValidEmail(string email)
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

                // Lấy thông tin người dùng hiện tại từ Claims
                var userId = int.TryParse(currentUser.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier)?.Value, out var id) ? id : 0;


                // Có được id của người dùng từ hệ thống thì liên kết tới database
                var myUser = await dbContext.Users.FirstOrDefaultAsync(u => u.UserId == userId);
                if (myUser == null)
                {
                    return new RequestResponse
                    {
                        IsSuccessful = false,
                        Message = "User not found.",
                    };
                }


                // Lấy RoleId và CampusId từ đối tượng người dùng
                var roleId = myUser.RoleId;
                var currentUserCampusId = myUser.CampusId;

                // Lấy RoleName dựa trên RoleId
                var currentUserRole = await dbContext.UserRoles
                    .Where(r => r.RoleId == roleId)
                    .Select(r => r.RoleName)
                    .FirstOrDefaultAsync();

                if (string.IsNullOrEmpty(currentUserRole))
                {
                    return new RequestResponse
                    {
                        IsSuccessful = false,
                        Message = "User role not found.",
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
                                if (!isHeaderSkipped)
                                {
                                    isHeaderSkipped = true;
                                    continue;
                                }

                                var userImportRequest = new UserImportRequest
                                {
                                    Mail = reader.GetValue(0)?.ToString(),
                                    FullName = reader.GetValue(1)?.ToString(),
                                    PhoneNumber = reader.GetValue(2)?.ToString(),
                                    EmailFe = reader.GetValue(3)?.ToString(),
                                    DateOfBirth = reader.GetValue(4)?.ToString(),
                                    Gender = reader.GetValue(5)?.ToString().ToLower(),
                                    Address = reader.GetValue(6)?.ToString()
                                };


                                var errorMessages = new List<string>();



                                //kiểm tra định dang của mail

                                if (userImportRequest.Mail.Length > 255)
                                {
                                    errorMessages.Add("Email must not exceed 255 characters.");
                                }

                                if (!IsValidEmail(userImportRequest.Mail))
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


                                // Kiểm tra vai trò được phép import
                                string targetRoleName = null;
                                if (currentUserRole == "Admin")
                                {
                                    targetRoleName = "Examiner";
                                }
                                else if (currentUserRole == "Examiner")
                                {
                                    targetRoleName = "Head of Department";
                                }
                                else if (currentUserRole == "Head of Department")
                                {
                                    targetRoleName = "Lecturer";
                                }
                                var targetRoleId = await dbContext.UserRoles.Where(r => r.RoleName == targetRoleName).Select(r => r.RoleId).FirstOrDefaultAsync();

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
                                if ((currentUserRole == "Lecturer" || currentUserRole == "Head of Department"))
                                {
                                    // Vai trò "Lecturer" và "Head of Department" thì được nhập EmailFe
                                    if (!string.IsNullOrEmpty(userImportRequest.EmailFe) && !IsValidEmail(userImportRequest.EmailFe))
                                    {
                                        errorMessages.Add($"This EmailFe '{userImportRequest.EmailFe}' is not valid.");
                                    }
                                }
                                else
                                {
                                    // Các vai trò khác không được nhập EmailFe
                                    if (!string.IsNullOrEmpty(userImportRequest.EmailFe))
                                    {
                                        errorMessages.Add($"Role '{currentUserRole}' is not allowed to have EmailFe.");
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
                                var existingUser = await dbContext.Users
                                    .FirstOrDefaultAsync(u => u.Mail == userImportRequest.Mail
                                                             && u.FullName == userImportRequest.FullName
                                                             && u.PhoneNumber == userImportRequest.PhoneNumber);

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
                            }
                        } while (reader.NextResult());
                    }
                }

                // Lưu các user hợp lệ
                if (usersToAdd.Any())
                {
                    await dbContext.Users.AddRangeAsync(usersToAdd);
                    await dbContext.SaveChangesAsync();
                }
                else
                {
                    // Thêm phản hồi nếu không có dữ liệu hợp lệ
                    response.IsSuccessful = false;
                    response.Message = "No valid data to import.";
                }

                if (errors.Any())
                {
                    response.IsSuccessful = false;
                    response.Message = $"There are the following errors: {string.Join("; ", errors)}";
                }
                else
                {
                    response.IsSuccessful = true;
                    response.Message = $"{usersToAdd.Count} users added successfully. {errors.Count} errors found.";
                    response.Message = "Import users from Excel successfully.";
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
                            join ia in dbContext.InstructorAssignments on e.ExamId equals ia.ExamId
                            join u in dbContext.Users on ia.AssignedUserId equals u.UserId
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
            try
            {
                var data = (from u in dbContext.Users
                            where u.UserId == userId
                            select new UserResponse
                            {
                                UserId = u.UserId,
                                UserName = u.FullName,
                                Email = u.Mail
                            }).ToList();

                //   var result = (from cus_truong in dbContext.CampusUserSubjects
                //                 join cus_giangvien in dbContext.CampusUserSubjects
                //                 on cus_truong.SubjectId equals cus_giangvien.SubjectId
                //                 join u in dbContext.Users
                //                 on cus_giangvien.UserId equals u.UserId
                //                 where cus_truong.UserId == userId
                //                       && cus_truong.IsLecturer == false
                //                       && cus_giangvien.IsLecturer == true
                //                 select new
                //                 {
                //                     u.UserId,
                //                     u.FullName,
                //                     u.Mail,
                //                     u.PhoneNumber
                //                 })
                //.Distinct()
                //.ToList();

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
    }
}
