namespace Library.Response
{
    public class CampusReportResponse
    {
        public int ExamCodeCount { get; set; } // Tổng số mã đề
        public int ErrorCode { get; set; }
        public int OKCode { get; set; }
        public List<CampusReport> Campus { get; set; } = new List<CampusReport>();
        public class CampusReport
        {
            public string? CampusName { get; set; }
            public int? totalExams { get; set; }
            public int ErrorCode { get; set; }
            public int OKCode { get; set; }
        }
    }
}
