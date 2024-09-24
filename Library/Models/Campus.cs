using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.Models
{
    public class Campus
    {
        [Key]
        public int CampusId { get; set; }

        [MaxLength(100)]
        public string? CampusName { get; set; }
    }
}
