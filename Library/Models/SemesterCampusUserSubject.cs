using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.Models
{
    public class SemesterCampusUserSubject
    {
        public int SemesterId { get; set; }
        public int CampusUserSubjectId { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime UpdatedDate { get; set; }

        public virtual CampusUserSubject CampusUserSubject { get; set; } = null!;
        public virtual Semester Semester { get; set; } = null!;
    }
}
