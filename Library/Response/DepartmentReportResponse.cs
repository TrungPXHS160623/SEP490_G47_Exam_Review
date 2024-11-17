namespace Library.Response
{
    public class DepartmentReportResponse
    {
        public string? CampusName { get; set; }
        public string? DepartmentName { get; set; }
        public int ExamCodeCount { get; set; }
        public int ErrorCode { get; set; }
        public int OKCode { get; set; }
        public List<DepartmentReport> DepartmentDetail { get; set; } = new List<DepartmentReport>();
        public class DepartmentReport
        {
            public string? ExamCode { get; set; }
            public string? Status { get; set; }
            public List<string> issues { get; set; } = new List<string>();
        }
    }
}
