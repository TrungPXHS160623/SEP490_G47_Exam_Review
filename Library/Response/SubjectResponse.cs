﻿namespace Library.Response
{
    public class SubjectResponse
    {
        public int SubjectId { get; set; }

        public string? SubjectCode { get; set; }

        public int? FacultyId { get; set; }
        public string? SubjectName { get; set; }

        public string? Faculty { get; set; }

        public DateTime? CreateDate { get; set; }
        public bool? IsDeleted { get; set; }

        public override bool Equals(object obj)
        {
            return obj is SubjectResponse response &&
                   SubjectId == response.SubjectId;
        }

        public override int GetHashCode()
        {
            return SubjectId.GetHashCode();
        }
    }
}
