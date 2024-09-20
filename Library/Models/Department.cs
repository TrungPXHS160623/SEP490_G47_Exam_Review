using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.Models
{
    public class Department
    {
        [Key]
        public int DepartmentId { get; set; }

        [Required]
        [StringLength(255)]
        public string DepartmentName { get; set; }

        [Required]
        public int HeadOfDepartmentId { get; set; }

        // Navigation property
        public User HeadOfDepartment { get; set; }
    }
}
