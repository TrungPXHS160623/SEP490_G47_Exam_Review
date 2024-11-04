namespace Library.Request
{
    public class ExamSearchRequest
    {
        public string? ExamCode { get; set; }

        public int? StatusId { get; set; }
        public int? SemesterId { get; set; }
        public int? UserId { get; set; }

    }
}
