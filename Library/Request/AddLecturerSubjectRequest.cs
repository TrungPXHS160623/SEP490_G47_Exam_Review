namespace Library.Request
{
    public class AddLecturerSubjectRequest
    {
        public int? UserId { get; set; }
        public int? HeadId { get; set; }
        public string? Mail { get; set; }
        public string? FullName { get; set; }
        public string? MailFe { get; set; }
        public string? PhoneNumber { get; set; }
        public int? SubjectId { get; set; }

        public bool IsExist { get; set; } = false;
    }
}
