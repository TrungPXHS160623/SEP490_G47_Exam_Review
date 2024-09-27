using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.Response
{
	public class ExamAssignResponse
	{
		public int ExamId { get; set; }
		public string? ExamCode { get; set; }
		public string? ExamDuration { get; set; }
		public string? ExamType { get; set; }
		public string? HeadOfDepartment { get; set; }
		public string? SubjectName { get; set; }
		public int? ExamStatusId { get; set; }
		public string? ExamStatus { get; set; }
		public DateTime? StartDate { get; set; }
		public DateTime? EndDate { get; set; }
		

	}

}
