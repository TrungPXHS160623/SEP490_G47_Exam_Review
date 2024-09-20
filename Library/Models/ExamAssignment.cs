﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
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

        [StringLength(50)]
        public string Status { get; set; }

        // Navigation properties
        public Exam Exam { get; set; }
        public User AssignedByUser { get; set; }
        public Department AssignedToDepartment { get; set; }
    }
}
