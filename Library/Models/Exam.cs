﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.Models
{
    public class Exam
    {
        [Key]
        public int ExamId { get; set; }

        [Required]
        [StringLength(50)]
        public string ExamCode { get; set; }

        [Required]
        [StringLength(10)]
        public string ExamDuration { get; set; }

        [Required]
        [StringLength(10)]
        public string ExamType { get; set; }

        [Required]
        public int SubjectId { get; set; }

        [Required]
        public int UserId { get; set; }

        public bool IsChecked { get; set; } = false;

        [Required]
        public int ExamStatusID { get; set; }

        [Required]
        public DateTime EstimatedTimeTest { get; set; }

        [Required]
        public DateTime StartDate { get; set; }

        [Required]
        public DateTime EndDate { get; set; }

        [StringLength(50)]
        public string ExamFormat { get; set; }

        // Navigation properties
        public Subject Subject { get; set; }
        public User User { get; set; }
        public ExamStatus ExamStatus { get; set; }
    }
}
