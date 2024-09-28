namespace Library.Response
{
    public class ExamDetailResponse
    {
        public int ExamId { get; set; }

        public string? ExamCode { get; set; }

        public string? ExamDuration { get; set; }

        public string? ExamType { get; set; }

        public string? SubjectName { get; set; }

        public string? ExamCreater { get; set; }

        public string? HeadOfDepartment { get; set; }

        public string? ExamStatus { get; set; }

        public DateTime? EstimatedTimeTest { get; set; }

        public DateTime? StartDate { get; set; }

        public DateTime? EndDate { get; set; }


    }
}
