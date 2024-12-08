namespace Library.Response
{
    public class ExaminerExamResponse
    {
        public int ExamId { get; set; }

        public string? ExamCode { get; set; }

        public int? ExamDuration { get; set; }
        public string? TermDuration { get; set; }
        public string? ExamType { get; set; }

        public int? SubjectId { get; set; }
        public int? FacutyID { get; set; }
        public string? SubjectCode { get; set; }

        public string? SubjectName { get; set; }

        public int? CreaterId { get; set; }

        public string? CreaterName { get; set; }

        public int? HeadDepartmentId { get; set; }

        public string? HeadDepartmentName { get; set; }

        public int? LectureId { get; set; }

        public string? LectureName { get; set; }

        public int? CampusId { get; set; }
        public int? SemesterId { get; set; }
        public string? SemseterName { get; set; }
        public string? CampusName { get; set; }

        public int? ExamStatusId { get; set; }

        public string? ExamStatusContent { get; set; }

        public int? AssignmentId { get; set; }

        public DateTime? EstimatedTimeTest { get; set; }
        public DateTime? ExamDate { get; set; }

        public DateTime? StartDate { get; set; }

        public DateTime? EndDate { get; set; }

        public DateTime? CreateDate { get; set; }

        public DateTime? UpdateDate { get; set; }
    }
}
