using System;
using System.Collections.Generic;

namespace Library.Models
{
    public class CampusUserFaculty
    {
        public int Id { get; set; }
        public int CampusId { get; set; }
        public int UserId { get; set; }
        public int FacultyId { get; set; }

        public DateTime CreateDate { get; set; } = DateTime.Now;
        public DateTime? UpdateDate { get; set; }

        // Navigation properties
        public virtual Campus Campus { get; set; } = null!;
        public virtual User User { get; set; } = null!;
        public virtual Faculty Faculty { get; set; } = null!;
    }
}
