namespace Library.Request
{
    public class ExamCreateRequest
    {
        public string? ExamCode { get; set; }

        public long? ExamDuration { get; set; }
        public string? TermDuration { get; set; }
        public string? ExamType { get; set; }

        public int? SubjectId { get; set; }

        public int? CreaterId { get; set; }
        public int? SemesterId { get; set; }

        public int? CampusId { get; set; }
        public DateTime? ExamDate { get; set; }
        public DateTime? EstimatedTimeTest { get; set; }

        public DateTime? StartDate { get; set; }

        public DateTime? EndDate { get; set; }

        public DateTime? CreateDate { get; set; }

        public DateTime? UpdateDate { get; set; }
    }
}
