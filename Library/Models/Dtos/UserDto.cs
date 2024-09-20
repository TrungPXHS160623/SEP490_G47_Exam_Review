using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.Models.Dtos
{
    public class UserDto
    {
        public int UserId { get; set; }  
        public string Mail { get; set; }
        public int CampusId { get; set; }
        public int RoleId { get; set; }

        public bool IsActive { get; set; }

        public string CampusName { get; set; }
        public string RoleName { get; set; }
    }
}
