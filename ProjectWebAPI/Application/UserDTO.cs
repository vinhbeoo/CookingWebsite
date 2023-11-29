namespace ProjectWebAPI.Application
{
    public class UserDTO
    {
        public int UserId { get; set; }

        public string UserName { get; set; } = null!;

        public string Password { get; set; } = null!;

        public string Email { get; set; } = null!;

        public int? RoleId { get; set; }

        public string? Status { get; set; }
    }
}
