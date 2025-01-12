﻿using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

#nullable disable

namespace CommunityLibrary.Models
{
    public partial class LibraryDbContext : DbContext
    {
        public LibraryDbContext()
        {
        }

        public LibraryDbContext(DbContextOptions<LibraryDbContext> options)
            : base(options)
        {
        }

        public virtual DbSet<AspNetRole> AspNetRoles { get; set; }
        public virtual DbSet<AspNetRoleClaim> AspNetRoleClaims { get; set; }
        public virtual DbSet<AspNetUser> AspNetUsers { get; set; }
        public virtual DbSet<AspNetUserClaim> AspNetUserClaims { get; set; }
        public virtual DbSet<AspNetUserLogin> AspNetUserLogins { get; set; }
        public virtual DbSet<AspNetUserRole> AspNetUserRoles { get; set; }
        public virtual DbSet<AspNetUserToken> AspNetUserTokens { get; set; }
        public virtual DbSet<Book> Books { get; set; }
        public virtual DbSet<BookReview> BookReviews { get; set; }
        public virtual DbSet<Loan> Loans { get; set; }
        public virtual DbSet<User> Users { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
                optionsBuilder.UseSqlServer("Server=tcp:bookcoop.database.windows.net,1433;Initial Catalog=LibraryDb;Persist Security Info=False;User ID=LibraryAdmin3;Password=TastyCupc@kes3;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasAnnotation("Relational:Collation", "SQL_Latin1_General_CP1_CI_AS");

            modelBuilder.Entity<AspNetRole>(entity =>
            {
                entity.HasIndex(e => e.NormalizedName, "RoleNameIndex")
                    .IsUnique()
                    .HasFilter("([NormalizedName] IS NOT NULL)");

                entity.Property(e => e.Name).HasMaxLength(256);

                entity.Property(e => e.NormalizedName).HasMaxLength(256);
            });

            modelBuilder.Entity<AspNetRoleClaim>(entity =>
            {
                entity.HasIndex(e => e.RoleId, "IX_AspNetRoleClaims_RoleId");

                entity.Property(e => e.RoleId).IsRequired();

                entity.HasOne(d => d.Role)
                    .WithMany(p => p.AspNetRoleClaims)
                    .HasForeignKey(d => d.RoleId);
            });

            modelBuilder.Entity<AspNetUser>(entity =>
            {
                entity.HasIndex(e => e.NormalizedEmail, "EmailIndex");

                entity.HasIndex(e => e.NormalizedUserName, "UserNameIndex")
                    .IsUnique()
                    .HasFilter("([NormalizedUserName] IS NOT NULL)");

                entity.Property(e => e.Email).HasMaxLength(256);

                entity.Property(e => e.NormalizedEmail).HasMaxLength(256);

                entity.Property(e => e.NormalizedUserName).HasMaxLength(256);

                entity.Property(e => e.UserName).HasMaxLength(256);
            });

            modelBuilder.Entity<AspNetUserClaim>(entity =>
            {
                entity.HasIndex(e => e.UserId, "IX_AspNetUserClaims_UserId");

                entity.Property(e => e.UserId).IsRequired();

                entity.HasOne(d => d.User)
                    .WithMany(p => p.AspNetUserClaims)
                    .HasForeignKey(d => d.UserId);
            });

            modelBuilder.Entity<AspNetUserLogin>(entity =>
            {
                entity.HasKey(e => new { e.LoginProvider, e.ProviderKey });

                entity.HasIndex(e => e.UserId, "IX_AspNetUserLogins_UserId");

                entity.Property(e => e.LoginProvider).HasMaxLength(128);

                entity.Property(e => e.ProviderKey).HasMaxLength(128);

                entity.Property(e => e.UserId).IsRequired();

                entity.HasOne(d => d.User)
                    .WithMany(p => p.AspNetUserLogins)
                    .HasForeignKey(d => d.UserId);
            });

            modelBuilder.Entity<AspNetUserRole>(entity =>
            {
                entity.HasKey(e => new { e.UserId, e.RoleId });

                entity.HasIndex(e => e.RoleId, "IX_AspNetUserRoles_RoleId");

                entity.HasOne(d => d.Role)
                    .WithMany(p => p.AspNetUserRoles)
                    .HasForeignKey(d => d.RoleId);

                entity.HasOne(d => d.User)
                    .WithMany(p => p.AspNetUserRoles)
                    .HasForeignKey(d => d.UserId);
            });

            modelBuilder.Entity<AspNetUserToken>(entity =>
            {
                entity.HasKey(e => new { e.UserId, e.LoginProvider, e.Name });

                entity.Property(e => e.LoginProvider).HasMaxLength(128);

                entity.Property(e => e.Name).HasMaxLength(128);

                entity.HasOne(d => d.User)
                    .WithMany(p => p.AspNetUserTokens)
                    .HasForeignKey(d => d.UserId);
            });

            modelBuilder.Entity<Book>(entity =>
            {
                entity.Property(e => e.IsActive)
                    .IsRequired()
                    .HasDefaultValueSql("((1))");

                entity.Property(e => e.TitleIdApi)
                    .HasMaxLength(40)
                    .IsUnicode(false)
                    .HasColumnName("TitleIdAPI");

                entity.HasOne(d => d.BookOwnerNavigation)
                    .WithMany(p => p.BookBookOwnerNavigations)
                    .HasForeignKey(d => d.BookOwner)
                    .HasConstraintName("FK__Books__BookOwner__7C4F7684");

                entity.HasOne(d => d.CurrentHolderNavigation)
                    .WithMany(p => p.BookCurrentHolderNavigations)
                    .HasForeignKey(d => d.CurrentHolder)
                    .HasConstraintName("FK__Books__CurrentHo__7B5B524B");
            });

            modelBuilder.Entity<BookReview>(entity =>
            {
                entity.Property(e => e.Review).HasMaxLength(250);

                entity.Property(e => e.TitleIdApi)
                    .HasMaxLength(40)
                    .IsUnicode(false)
                    .HasColumnName("TitleIdAPI");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.BookReviews)
                    .HasForeignKey(d => d.UserId)
                    .HasConstraintName("FK__BookRevie__UserI__7A672E12");
            });

            modelBuilder.Entity<Loan>(entity =>
            {
                entity.Property(e => e.DueDate).HasColumnType("datetime");

                entity.Property(e => e.LoanerNote).HasMaxLength(140);

                entity.Property(e => e.OwnerNote).HasMaxLength(140);

                entity.HasOne(d => d.Book)
                    .WithMany(p => p.Loans)
                    .HasForeignKey(d => d.BookId)
                    .HasConstraintName("FK__Loans__BookId__7D439ABD");

                entity.HasOne(d => d.BookLoanerNavigation)
                    .WithMany(p => p.LoanBookLoanerNavigations)
                    .HasForeignKey(d => d.BookLoaner)
                    .HasConstraintName("FK__Loans__BookLoane__7E37BEF6");

                entity.HasOne(d => d.BookOwnerNavigation)
                    .WithMany(p => p.LoanBookOwnerNavigations)
                    .HasForeignKey(d => d.BookOwner)
                    .HasConstraintName("FK__Loans__BookOwner__7F2BE32F");
            });

            modelBuilder.Entity<User>(entity =>
            {
                entity.Property(e => e.CumulatvieRating).HasDefaultValueSql("((5))");

                entity.Property(e => e.Latitude)
                    .HasMaxLength(25)
                    .IsUnicode(false);

                entity.Property(e => e.Longitude)
                    .HasMaxLength(25)
                    .IsUnicode(false);

                entity.Property(e => e.ProfileImage)
                    .HasMaxLength(255)
                    .IsUnicode(false);

                entity.Property(e => e.UserId).HasMaxLength(450);

                entity.Property(e => e.UserLocation).HasMaxLength(255);

                entity.Property(e => e.UserName)
                    .HasMaxLength(30)
                    .HasDefaultValueSql("('Friend')");

                entity.HasOne(d => d.UserNavigation)
                    .WithMany(p => p.Users)
                    .HasForeignKey(d => d.UserId)
                    .HasConstraintName("FK__Users__UserId__00200768");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
