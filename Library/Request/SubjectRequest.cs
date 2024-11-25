namespace Library.Request
{
    public class SubjectRequest
    {
        public int SubjectId { get; set; }

        public int? FacultyId { get; set; }

        public string? SubjectCode { get; set; }

        public string? SubjectName { get; set; }

        public bool? IsDeleted { get; set; }

        public DateTime? CreateDate { get; set; }

        public DateTime? UpdateDate { get; set; }


    }
}
