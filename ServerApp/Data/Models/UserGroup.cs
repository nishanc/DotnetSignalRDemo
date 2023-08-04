namespace ServerApp.Data.Models;

public class UserGroup
{
    public int Id { get; set; }
    public string GroupName { get; set; }

    public List<User> Users { get; set; } // Navigation property to users in this group
}