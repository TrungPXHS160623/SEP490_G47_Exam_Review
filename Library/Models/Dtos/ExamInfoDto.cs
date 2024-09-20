using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.Models.Dtos
{
	public class ExamInfoDto
	{
		public string DepartmentName { get; set; }
		public string SubjectName { get; set; }
		public string ExamCode { get; set; }
		public string Status { get; set; }
		public string InstructorName { get; set; }
	}
}
