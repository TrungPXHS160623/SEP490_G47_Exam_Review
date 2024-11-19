namespace Library.Response
{
    public class UserResponse
    {
        public int UserId { get; set; }
        public string? Email { get; set; }
        public string? FeEmail { get; set; }
        public string? UserName { get; set; }
        public string? Tel { get; set; }
        public string? RoleName { get; set; }
        public bool IsActive { get; set; }
        public string? CampusName { get; set; }
        public DateTime? UpdateDt { get; set; }

        public override bool Equals(object obj)
        {
            return obj is UserResponse response &&
                   UserId == response.UserId;
        }

        public override int GetHashCode()
        {
            return UserId.GetHashCode();
        }

    }
}
