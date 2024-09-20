using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace Library.Models.Dtos
{
    public class AddUserRequestDto
    {
        [Required(ErrorMessage = "Email is required")]
        [EmailAddress(ErrorMessage = "Invalid email format")]
        public string? Mail { get; set; }

        [Required(ErrorMessage = "Campus ID is required")]
        [Range(1, 5, ErrorMessage = "Campus ID must be <=5 ")]
        public int CampusId { get; set; }

        [Required(ErrorMessage = "Role ID is required")]
        [Range(1, 4, ErrorMessage = "Role ID must be a <=4 ")]
        public int RoleId { get; set; }

        public bool IsActive { get; set; }
    }
}
