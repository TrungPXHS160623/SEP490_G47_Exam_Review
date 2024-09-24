﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.Request
{
    public class UserRequest
    {
        public int UserId { get; set; }
        public string? Email { get; set; }
        public int? RoleId { get; set; }
        public string? RoleName { get; set; }
        public int? CampusId { get; set; }
        public string? CampusName { get; set; }
        public bool? IsActive { get; set; }

    }
}
