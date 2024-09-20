using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.Models
{
    public class Subject
    {
        [Key]
        public int SubjectId { get; set; }

        [Required]
        [StringLength(255)]
        public string SubjectName { get; set; }

        [Required]
        public int DepartmentId { get; set; }

        // Navigation property
        public Department Department { get; set; }
    }
}
