using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.Response
{
    public class AssignResponse
    {
        public string? ExamCode { get; set; }    // mã đề

        public string? ExamDuration { get; set; }  //10w,5w,half1,half2

        public string? ExemType { get; set; }  //trắc nghiệm , tự luận

        public string? SubjectName { get; set; }  //tên môn học

        public string? CampusName { get; set; }   //tên campus 

        public string? ExaminorMail { get; set; } //Mail khảo thí

        public string? HeadOfDepartmentMail { get; set; } //Mail của trưởng bộ môn gửi đề

        public string? LecturorMail { get; set; } //Mail của giảng viên nhận đề 

        public string? ExamStatus { get; set; } //trạng thái của phân công

        public DateTime? EstimatedTimeTest { get; set; } //thời gian dự kiến test
        
        public DateTime? AssignmentDate { get; set; } //thời gian bắt đầu tạo phân công






    }
}
