namespace Library.Request
{
    public class UserRequest
    {
        public int UserId { get; set; }
        public string? Email { get; set; }
        public string? UserName { get; set; }
        public string? Phone { get; set; }
        public int? RoleId { get; set; }
        public string? RoleName { get; set; }
        public int? CampusId { get; set; }
        public string? CampusName { get; set; }
        public bool? IsActive { get; set; }

    }
}
