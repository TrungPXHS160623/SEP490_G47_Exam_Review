﻿namespace Library.Response
{
    public class LeaderExamResponse
    {
        public int ExamId { get; set; }

        public string? ExamCode { get; set; }

        public long? ExamDuration { get; set; }

        public string? ExamType { get; set; }

        public int SubjectId { get; set; }

        public string? SubjectCode { get; set; }

        public string? SubjectName { get; set; }

        public int? CreaterId { get; set; }

        public string? CreaterName { get; set; }

        public int? HeadDepartmentId { get; set; }

        public string? HeadDepartmentName { get; set; }

        public int? AssignedLectureId { get; set; }

        public string? AssignedLectureName { get; set; }

        public int CampusId { get; set; }

        public string? CampusName { get; set; }
        public string? SemesterName { get; set; }

        public int? ExamStatusId { get; set; }

        public string? ExamStatusContent { get; set; }
        public DateTime? EstimatedTimeTest { get; set; }
        public DateTime? ExamDate { get; set; }
        public DateTime? StartDate { get; set; }

        public DateTime? EndDate { get; set; }

        public DateTime? CreateDate { get; set; }

        public DateTime? UpdateDate { get; set; }
    }
}
