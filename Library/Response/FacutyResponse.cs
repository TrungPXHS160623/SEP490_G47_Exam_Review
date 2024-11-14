namespace Library.Response
{
    public class FacutyResponse
    {
        public int FacultyId { get; set; }
        public string FacultyName { get; set; } = null!;
        public string Description { get; set; } = null!;
        public DateTime CreateDate { get; set; }
        public DateTime UpdateDate { get; set; }
        public override bool Equals(object obj)
        {
            return obj is FacutyResponse response &&
                   FacultyId == response.FacultyId;
        }

        public override int GetHashCode()
        {
            return FacultyId.GetHashCode();
        }

    }
}
