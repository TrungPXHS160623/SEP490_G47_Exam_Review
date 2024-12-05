using Library.Response;

namespace Library.Request
{
    public class UserSubjectRequest : UserRequest
    {
        public int? FacultyId { get; set; }
        public string FacultyName { get; set; }
        public IEnumerable<SubjectResponse> SubjectResponses { get; set; } = new List<SubjectResponse>();
    }
}
