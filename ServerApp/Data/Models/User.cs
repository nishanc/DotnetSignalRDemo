namespace ServerApp.Data.Models
{
    public class User
    {
        public int Id { get; set; }
        public string Username { get; set; }
        public byte[] PasswordHash { get; set; }
        public byte[] PasswordSalt { get; set; }
        public int UserGroupId { get; set; } // Foreign key to UserGroup
        public UserGroup UserGroup { get; set; } // Navigation property to UserGroup
    }
}
