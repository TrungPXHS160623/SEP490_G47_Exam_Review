namespace Library.Response
{
    public class InstructorAssign
    {
        public int AssignmentId { get; set; }

        public int ExamId { get; set; }

        public int AssignedUserId { get; set; }

        public DateTime? AssignmentDate { get; set; }

        public int? AssignStatusId { get; set; }
        public string? leture { get; set; }
        public int? ReportID { get; set; }

    }
}
