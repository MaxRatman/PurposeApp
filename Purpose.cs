using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace PurposeApp
{
    public class Purpose
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public List<Subtask> subtasks { get; set; }
        public int UserId { get; set; }
        public User? UserP { get; set; }
    }
    public class Subtask
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Text { get; set; }
        public bool Compete { get; set; }
        public int PurposeId { get; set; }
    }
    public class User
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Password { get; set; }
        public string Email { get; set; }
        public string UserName { get; set; }
        public List<Purpose> Purposes { get; set; }
    }
        public class AppContext : DbContext
        {
            public DbSet<Purpose> Purposes { get; set; }
            public DbSet<Subtask> Subtasks { get; set; }
            public DbSet<User> Users { get; set; }
            public AppContext(DbContextOptions<AppContext> dbContextOptions):base(dbContextOptions) { }
            public AppContext(DbContextOptionsBuilder options) { }
            protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
            {

            }
            protected override void OnModelCreating(ModelBuilder builder)
            {
            }
        }
        public class ContextFactory : IDesignTimeDbContextFactory<AppContext>
        {
            public AppContext CreateDbContext(string[] args)
            {
                var optionsBuilder = new DbContextOptionsBuilder<AppContext>();
                ConfigurationBuilder builder = new ConfigurationBuilder();
                builder.SetBasePath(Directory.GetCurrentDirectory());
                builder.AddJsonFile("config.json");
                IConfigurationRoot config = builder.Build();

                string connectionString = config.GetConnectionString("DefaultConnection");
                optionsBuilder.UseSqlServer(connectionString, opts => opts.CommandTimeout((int)TimeSpan.FromMinutes(10).TotalSeconds));
                return new AppContext(optionsBuilder.Options);
            }
        }
    }

