using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.Models
{
    public class ExamStatus
    {
        [Key]
        public int ExamStatusID { get; set; }

        [Required]
        [StringLength(255)]
        public string StatusContent { get; set; }
    }
}
