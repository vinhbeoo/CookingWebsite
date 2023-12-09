namespace ProjectWebAPI.Application
{
    public class UserDTO
    {
        public int UserId { get; set; }

        public string UserName { get; set; } = null!;

        public string Password { get; set; } = null!;

        public string Email { get; set; } = null!;

        public bool EmailConfirmed { get; set; }

        public string EmailConfirmationToken { get; set; }

        public int? RoleId { get; set; }

        public string Status { get; set; }

        public int UserType { get; set; }
    }
}
