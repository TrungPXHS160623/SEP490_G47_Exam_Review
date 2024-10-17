using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.Models
{
    public class UserDetail
    {
        public int UserDetailId { get; set; }
        public int UserId { get; set; }
        public string FullName { get; set; } = null!;
        public string? PhoneNumber { get; set; }
        public string? EmailFe { get; set; }
        public DateTime? DateOfBirth { get; set; }
        public string? Gender { get; set; }
        public string? Address { get; set; }
        public string? ProfilePicture { get; set; }
        public bool? IsActive { get; set; }
        public DateTime? CreateDate { get; set; }
        public DateTime? UpdateDate { get; set; }

        public virtual User User { get; set; } = null!;
    }
}
