using Microsoft.EntityFrameworkCore;
using MVCTaskManagmentApp.Models;
using System.Collections.Generic;

namespace MVCTaskManagmentApp.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        public DbSet<TaskItem> Tasks { get; set; }
        public DbSet<Users> Users { get; set; }
    }
}
