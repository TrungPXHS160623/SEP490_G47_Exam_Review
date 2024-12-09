namespace Library.Request
{
    public class UserImportRequest
    {
        public string Mail { get; set; } = null!;
        public string? FullName { get; set; }
        public string? PhoneNumber { get; set; }
        public string? EmailFe { get; set; } = "Unknown";
        public string? DateOfBirth { get; set; }
        public string? Gender { get; set; }
        public string? Address { get; set; }

        // Cột mới để nhập thông tin bộ môn 
        public string? FacultyInCharge { get; set; }
        // Cột mới để nhập thông tin môn học
        public string? SubjectInCharge { get; set; }

    }
}
