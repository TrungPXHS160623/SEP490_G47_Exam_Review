using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.Models
{
    public class Faculty
    {
        public int FacultyId { get; set; }
        public string FacultyName { get; set; }
        public string Description { get; set; }
        public int? DeanId { get; set; } 
        public DateTime CreateDate { get; set; } = DateTime.Now;
        public DateTime UpdateDate { get; set; } = DateTime.Now;

        // Navigation properties for relationships
        public virtual ICollection<Subject> Subjects { get; set; } = new List<Subject>();
        public virtual User Deans { get; set; }
    }
}
