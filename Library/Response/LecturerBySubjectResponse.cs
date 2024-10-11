using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.Response
{
	public class LecturerBySubjectResponse
	{
		public string Lecturer { get; set; }
		public string SubjectName { get; set; }
		public string Campus { get; set; }
		public List<string> ExamCodes { get; set; }
	}
}
