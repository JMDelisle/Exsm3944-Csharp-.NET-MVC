﻿using Exsm3945_Assignment.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
//^[a-zA-Z]{1,15}$
//@"^[a-zA-Z.-]{1,15}$"
namespace Exsm3945_Assignment.Data
{
    public partial class ApplicationDbContext : IdentityDbContext
    {
        public ApplicationDbContext()
        {

        }
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }
        public virtual DbSet<Account> Accounts { get; set; } = null!;
        public virtual DbSet<AccountType> AccountTypes { get; set; } = null!;
        public virtual DbSet<Client> Clients { get; set; } = null!;

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseMySql("server=localhost;user=root;database=csharp2_assignment_jeanmarc", Microsoft.EntityFrameworkCore.ServerVersion.Parse("10.4.24-mariadb"));
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.UseCollation("utf8mb4_general_ci")
                .HasCharSet("utf8mb4");

            modelBuilder.Entity<AccountType>()
               .HasData(new AccountType { Id = 1, Name = "Chequing", InterestRate = 0.35m },
                       new AccountType { Id = 2, Name = "Savings", InterestRate = 0.86m },
                       new AccountType { Id = 3, Name = "Retirement", InterestRate = 1.50m }
                      );

            modelBuilder.Entity<Client>()
              .HasData(new Client
              {
                  Id = 1,
                  FirstName = "Johnny",
                  LastName = "English",
                  Dob = new DateOnly(1958, 03, 05),
                  Address = "151 Military Ave"
              },
                       new Client
                       {
                           Id = 2,
                           FirstName = "Kirk ",
                           LastName = "Grimes",
                           Dob = new DateOnly(1960, 05, 22),
                           Address = "915 Halifax Rd."
                       },
                       new Client
                       {
                           Id = 3,
                           FirstName = "Izzie ",
                           LastName = "Cash",
                           Dob = new DateOnly(1963, 06, 04),
                           Address = "919 Old Theatre Court"
                       },
                       new Client
                       {
                           Id = 4,
                           FirstName = "Lachlan ",
                           LastName = "Kent",
                           Dob = new DateOnly(1964, 10, 30),
                           Address = "7363 South Gulf St"
                       }
                      );

            modelBuilder.Entity<Account>()
              .HasData(new Account(62512.62m)
              {
                  Id = 1,
                  ClientId = 1,
                  AccountTypeId = 1,
                  InterestAppliedDate = new DateOnly(1998, 01, 09)
              },
                       new Account(79364.54m)
                       {
                           Id = 2,
                           ClientId = 1,
                           AccountTypeId = 2,
                           InterestAppliedDate = new DateOnly(1979, 5, 25)
                       },
                       new Account(169300.14m)
                       {
                           Id = 3,
                           ClientId = 2,
                           AccountTypeId = 3,
                           InterestAppliedDate = new DateOnly(2004, 04, 28)
                       },
                       new Account(55495.53m)
                       {
                           Id = 4,
                           ClientId = 3,
                           AccountTypeId = 2,
                           InterestAppliedDate = new DateOnly(2010, 08, 03)
                       },
                       new Account(392450.78m)
                       {
                           Id = 5,
                           ClientId = 3,
                           AccountTypeId = 1,
                           InterestAppliedDate = new DateOnly(2001, 05, 16)
                       },
                       new Account(223.96m)
                       {
                           Id = 6,
                           ClientId = 4,
                           AccountTypeId = 2,
                           InterestAppliedDate = new DateOnly(2008, 02, 14)
                       },
                       new Account(98000.00m)
                       {
                           Id = 7,
                           ClientId = 2,
                           AccountTypeId = 3,
                           InterestAppliedDate = new DateOnly(2019, 10, 05)
                       }
                      );

            modelBuilder.Entity<Account>(entity =>
            {
                entity.HasOne(d => d.AccountType)
                    .WithMany(p => p.Accounts)
                    .HasForeignKey(d => d.AccountTypeId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("account_ibfk_2");

                entity.HasOne(d => d.Client)
                    .WithMany(p => p.Accounts)
                    .HasForeignKey(d => d.ClientId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("account_ibfk_1");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);

    }
}