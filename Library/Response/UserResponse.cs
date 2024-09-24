namespace Library.Response
{
    public class UserResponse
    {
        public int UserId { get; set; }
        public string? Email { get; set; }
        public string? RoleName { get; set; }
        public bool IsActive { get; set; }
        public string? CampusName { get; set; }
    }
}
