using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.Request
{
	public class StatusRequest
	{
		public int ExamId { get; set; }
		public string? ExamCode { get; set; }
		public string? ExamDuration { get; set; }
		public string? ExamType { get; set; }
		public string? SubjectName { get; set; }
		public string? ExamStatus { get; set; }
		public DateTime? UpdateDate { get; set; }
		public DateTime? StartDate { get; set; }
		public DateTime? EndDate { get; set; }
	}
}
