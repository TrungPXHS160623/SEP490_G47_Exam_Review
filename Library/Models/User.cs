using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Library.Models
{
    public class User
    {
        [Key]
        public int UserId { get; set; }

        [Required]
        [StringLength(250)]
        public string Mail { get; set; }

        public int? CampusId { get; set; }
        public int? RoleId { get; set; }
        public bool IsActive { get; set; }

        public DateTime? CreateDate { get; set; }
        public DateTime? UpdateDate { get; set; }

        [ForeignKey("CampusId")]
        public virtual Campus Campus { get; set; }

        [ForeignKey("RoleId")]
        public virtual UserRole UserRole { get; set; } 

        public virtual ICollection<Report> Reports { get; set; }
    }
}
