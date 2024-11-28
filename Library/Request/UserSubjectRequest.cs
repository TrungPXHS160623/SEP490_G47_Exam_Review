namespace Library.Request
{
    public class UserSubjectRequest : UserRequest
    {
        public int? FacultyId { get; set; }
        public string FacultyName { get; set; }

    }
}
