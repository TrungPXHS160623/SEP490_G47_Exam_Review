using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.Models
{
    public class ExamAssignment
    {
        [Key]
        public int AssignmentId { get; set; }

        [Required]
        public int ExamId { get; set; }

        [Required]
        public int AssignedBy { get; set; }

        [Required]
        public int AssignedTo { get; set; }

        [Required]
        public DateTime AssignmentDate { get; set; }

        public string Status { get; set; }

        public DateTime? CreateDate { get; set; }
        public DateTime? UpdateDate { get; set; }

        [ForeignKey("ExamId")]
        public virtual Exam Exam { get; set; }

        [ForeignKey("AssignedBy")]
        public virtual User Assigner { get; set; }

        [ForeignKey("AssignedTo")]
        public virtual Department AssignedDepartment { get; set; }
    }
}
