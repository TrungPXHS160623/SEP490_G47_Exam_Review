using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.Response
{
    public class ReportDurationResponse
    {
        public int AssignmentId { get; set; }               // Mã phân công
        public double TotalDurationHours { get; set; }      // Tổng thời gian làm report (tính theo giờ)
        public List<ReportDurationDetail> ReportDurations { get; set; }  // Danh sách báo cáo cùng thời gian thực hiện
    }
    public class ReportDurationDetail
    {
        public int ReportId { get; set; }                   // Mã báo cáo
        public int QuestionNumber { get; set; }             // Số thứ tự câu hỏi
        public double DurationHours { get; set; }           // Thời gian làm báo cáo (tính theo giờ)
    }
}
