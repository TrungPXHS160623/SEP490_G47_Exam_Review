using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.Models
{
    public class User
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int UserId { get; set; }

        [Required]
        [MaxLength(250)]
        public string Mail { get; set; }

        [ForeignKey("Campus")]
        public int? CampusId { get; set; }

        [ForeignKey("UserRole")]
        public int? RoleId { get; set; }

        public bool IsActive { get; set; }

        public Campus Campus { get; set; }
        public UserRole UserRole { get; set; }
    }
}
