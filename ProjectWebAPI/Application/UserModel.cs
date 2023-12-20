namespace ProjectWebAPI.Application
{
    public class UserModel
    {
        public int UserId { get; set; }

        public string FullName { get; set; }

        public bool Gender { get; set; }

        public string Phone { get; set; }

        public string Address { get; set; }

        public string Email { get; set; } = null!; 
        
        public int? RoleId { get; set; }

        public string Status { get; set; }

        public int UserType { get; set; }
    }
}
