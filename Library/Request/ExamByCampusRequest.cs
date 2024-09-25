namespace Library.Request
{
    public class ExamByCampusRequest
    {
        public int ExamId { get; set; }

        public string? ExamCode { get; set; }
        public string? StatusContent { get; set; }
        public string? HeadOfDepartment { get; set; }
        public DateTime? EstimatedTimeTest { get; set; }

        public DateTime? StartDate { get; set; }

        public DateTime? EndDate { get; set; }
    }
}
