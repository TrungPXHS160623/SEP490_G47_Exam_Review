﻿namespace Library.Request
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
        public string? Deparment { get; set; }
        // Cột mới để nhập thông tin bộ môn hoặc môn học
        public string? FacultyOrSubjectInCharge { get; set; }
    }
}
