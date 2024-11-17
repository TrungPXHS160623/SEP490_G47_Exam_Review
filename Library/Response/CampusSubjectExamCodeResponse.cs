namespace Library.Response
{


    public class CampusSubjectExamResponse
    {
        public string? CampusName { get; set; }
        public int ExamCodeCount { get; set; } // Tổng số mã đề
        public int ErrorCode { get; set; }
        public int OKCode { get; set; }
        public List<CampusSubjectExamCodeResponse> Departments { get; set; } = new List<CampusSubjectExamCodeResponse>();
        public class CampusSubjectExamCodeResponse
        {
            public string? departmentName { get; set; }
            public int? totalExams { get; set; }
            public int ErrorCode { get; set; }
            public int OKCode { get; set; }
        }
    }



}
