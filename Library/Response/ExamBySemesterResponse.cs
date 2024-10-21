using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.Response
{
	public class ExamBySemesterResponse
	{
		public string ExamCode { get; set; }
		public string SubjectName { get; set; }
		public string FullName { get; set; }
		public string SemesterName { get; set; }
	}
}
