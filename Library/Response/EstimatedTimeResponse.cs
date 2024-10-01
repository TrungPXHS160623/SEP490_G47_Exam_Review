using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.Response
{
	public class EstimatedTimeResponse
	{
		public string? ExamCode { get; set; }

		public string? SubjectName { get; set; }

		public DateTime? EstimatedTimeTest { get; set; }
	}
}
