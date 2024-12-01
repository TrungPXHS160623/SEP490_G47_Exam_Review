using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.Request
{
    public class ExamImportRequest
    {
        public string ExamCode { get; set; } //mã đề thi
        public string TermDuration { get; set; } //đợt thi
        public string ExamType { get; set; } //hình thức thi
        public string CampusName { get; set; } // tên cơ sở
        public string SubjectCode { get; set; } //tên môn
        public string ExamDuration { get; set; }  //thời lượng thi
        public DateTime? StartDate { get; set; } //thời gian bát đầu thực hiện test đề 
        public DateTime? EndDate { get; set; } //thời gian kết thúc thực hiện test đề 
        public string SemesterName { get; set; } //tên kì
    }
}
