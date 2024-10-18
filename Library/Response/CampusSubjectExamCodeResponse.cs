using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.Response
{
    ////cách 2
    public class CampusSubjectExamCodeResponse
    {
        public string? ExamCode { get; set; }

        public string? SubjectName { get; set; }

        public string? CampusName { get; set; }

        public int ExamCodeCount { get; set; }
    }
    ////cách 1
    //// Lớp này sẽ chứa thông tin tổng hợp cho phản hồi
    //public class CampusSubjectExamResponse
    //{
    //    public List<CampusSubjectExamCodeResponse> ExamCodes { get; set; } // Danh sách mã đề
    //    public int ExamCodeCount { get; set; } // Tổng số mã đề
    //}

    ////Lớp này sẽ chứa thông tin cho từng mã đề
    //public class CampusSubjectExamCodeResponse
    //{
    //    public string? ExamCode { get; set; }
    //    public string? SubjectName { get; set; }
    //    public string? CampusName { get; set; }
    //}

}
