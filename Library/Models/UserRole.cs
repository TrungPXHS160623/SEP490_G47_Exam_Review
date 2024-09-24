﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.Models
{
    public class UserRole
    {
        [Key]
        public int RoleId { get; set; }

        [MaxLength(100)]
        public string? RoleName { get; set; }
    }
}
