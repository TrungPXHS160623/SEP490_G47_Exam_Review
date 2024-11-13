namespace Library.Request
{
    public class FacutyRequest
    {
        public int FacultyId { get; set; }
        public string FacultyName { get; set; } = null!;
        public string Description { get; set; } = null!;
        public DateTime CreateDate { get; set; }
        public DateTime UpdateDate { get; set; }

    }
}
