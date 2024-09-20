namespace Library.Models.Dtos
{
    public class ExamDto
    {
        public int ExamId { get; set; }
        public string ExamCode { get; set; }
        public string ExamType { get; set; }
        public string Status { get; set; }
        public string HeadDepartmentMail { get; set; }
        public DateTime EstimatedTimeTest { get; set; }
    }

}
