namespace UserManagement.Data.Migrations
{
    using System;
    using System.Collections.Generic;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;
    using UserManagement.Data.Models;

    internal sealed class Configuration : DbMigrationsConfiguration<UserManagement.Data.Context.UserManagementContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(UserManagement.Data.Context.UserManagementContext context)
        {
            //  This method will be called after migrating to the latest version.

            IList<User> users = new List<User>();
            users.Add(new User() { Id = Guid.NewGuid(), FirstName = "Savan", LastName = "Sakariya", Address = "Rajkot", BirthDate = new DateTime(1994, 11, 12), Email = "savan@gmail.com" });
            users.Add(new User() { Id = Guid.NewGuid(), FirstName = "Akshay", LastName = "Rathod", Address = "Jamnagar", BirthDate = new DateTime(2000, 08, 28), Email = "a.kshay@gmail.com" });
            users.Add(new User() { Id = Guid.NewGuid(), FirstName = "Amit", LastName = "Patel", Address = "Junagadh", BirthDate = new DateTime(1992, 05, 14), Email = "apatel@gmail.com" });

            context.Users.AddRange(users);
            base.Seed(context);

            //  You can use the DbSet<T>.AddOrUpdate() helper extension method
            //  to avoid creating duplicate seed data.
        }
    }
}
