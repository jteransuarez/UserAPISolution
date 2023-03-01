using Microsoft.EntityFrameworkCore;
using System;
using UserAPI.Core.Domain.Model;

namespace UserAPI.Infrastructure.DataContext
{
    public class UserAPIDataContext : DbContext
    {
        // protected override void OnConfiguring
        //(DbContextOptionsBuilder optionsBuilder)
        // {
        //     optionsBuilder.UseInMemoryDatabase(databaseName: "UserTestDB");
        // }

        public UserAPIDataContext(DbContextOptions<UserAPIDataContext> options) : base(options)
        {

        }

        public DbSet<User> Users { get; set; }
    }
}