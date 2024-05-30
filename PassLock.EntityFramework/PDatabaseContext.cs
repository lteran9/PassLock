using System;
using Microsoft.EntityFrameworkCore;
using PassLock.Core;
using PassLock.EntityFramework.Config;

namespace PassLock.EntityFramework
{
   internal class PDatabaseContext : DbContext
   {
      public DbSet<Domain> Domains { get; set; }
      public DbSet<Account> Accounts { get; set; }
      public DbSet<Password> Passwords { get; set; }
      public DbSet<AccountPasswordForDomain> AccountDomainPasswords { get; set; }

#pragma warning disable CS8618
      public PDatabaseContext()
      {
         // This will set the SqlitePath used by OnConfiguring
         DatabaseSettings.Init();
      }
#pragma warning restore CS8618

      protected override void OnModelCreating(ModelBuilder builder)
      {
         base.OnModelCreating(builder);
         builder.Entity<AccountPasswordForDomain>()
            .HasKey(x => new { x.AccountId, x.PasswordId, x.DomainId });

      }

      // The following configures EF to create a Sqlite database file in the
      // special "local" folder for your platform.
      protected override void OnConfiguring(DbContextOptionsBuilder options)
          => options.UseSqlite($"Data Source={DatabaseSettings.SqlitePath}", options =>
          {

          });
   }
}