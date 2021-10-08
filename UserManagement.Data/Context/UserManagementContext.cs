using System.Data.Entity;
using UserManagement.Data.Models;

namespace UserManagement.Data.Context
{
    public class UserManagementContext : DbContext
    {
        public UserManagementContext() : base("UserManagementDBConnectionString")
        {

        }
        public DbSet<User> Users { get; set; }
    }
}
