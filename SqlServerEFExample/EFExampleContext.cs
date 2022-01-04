using System;
using Microsoft.EntityFrameworkCore;

namespace SqlServerEFExample {

  public class EFExampleContext : DbContext {
    string _connectionString;
    public EFExampleContext(string connectionString) {
      this._connectionString = connectionString;
    }
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder) {
      optionsBuilder.UseSqlServer(this._connectionString);
    }

    public DbSet<User> Users { get; set; }
    public DbSet<Task> Tasks { get; set; }
  }
}