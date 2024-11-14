using Library.Response;

namespace Library.Request
{
    public class UserSubjectRequest : UserRequest
    {
        public int? FacultyId { get; set; }
        public IEnumerable<FacutyResponse> FacutyResponse { get; set; } = new List<FacutyResponse>();
        public IEnumerable<SubjectResponse> SubjectResponses { get; set; } = new List<SubjectResponse>();
    }
}
