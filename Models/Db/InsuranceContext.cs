﻿using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace insurance.Models.Db;

public partial class InsuranceContext : DbContext
{
    public InsuranceContext()
    {
    }

    public InsuranceContext(DbContextOptions<InsuranceContext> options)
        : base(options)
    {
    }

    public virtual DbSet<BeneficiaryDetail> BeneficiaryDetails { get; set; }

    public virtual DbSet<Cart> Carts { get; set; }

    public virtual DbSet<Claim> Claims { get; set; }

    public virtual DbSet<Policy> Policies { get; set; }

    public virtual DbSet<PolicySold> PolicySolds { get; set; }

    public virtual DbSet<User> Users { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Server=(localdb)\\MSSQLLocalDB;database=insurance;Integrated Security=true;TrustServerCertificate=true");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<BeneficiaryDetail>(entity =>
        {
            entity.HasKey(e => e.BeneficiaryId).HasName("PK__Benefici__3FBA95D550C3704D");

            entity.Property(e => e.BeneficiaryId).HasColumnName("BeneficiaryID");
            entity.Property(e => e.ContactNumber)
                .HasMaxLength(15)
                .IsUnicode(false);
            entity.Property(e => e.Name)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.Relationship)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.UserName)
                .HasMaxLength(25)
                .IsUnicode(false);

            entity.HasOne(d => d.UserNameNavigation).WithMany(p => p.BeneficiaryDetails)
                .HasPrincipalKey(p => p.UserName)
                .HasForeignKey(d => d.UserName)
                .HasConstraintName("FK__Beneficia__UserN__5DCAEF64");
        });

        modelBuilder.Entity<Cart>(entity =>
        {
            entity
                .HasNoKey()
                .ToTable("Cart");

            entity.Property(e => e.CartId).ValueGeneratedOnAdd();
            entity.Property(e => e.UserId).HasColumnName("UserID");

            entity.HasOne(d => d.Policy).WithMany()
                .HasForeignKey(d => d.PolicyId)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("FK__Cart__PolicyId__30F848ED");

            entity.HasOne(d => d.User).WithMany()
                .HasForeignKey(d => d.UserId)
                .HasConstraintName("FK__Cart__UserID__300424B4");
        });

        modelBuilder.Entity<Claim>(entity =>
        {
            entity.HasNoKey();

            entity.Property(e => e.ClaimAmount).HasColumnType("money");
            entity.Property(e => e.ClaimDate).HasColumnType("datetime");
            entity.Property(e => e.ClaimId).ValueGeneratedOnAdd();
            entity.Property(e => e.RemainingAmount)
                .HasColumnType("money")
                .HasColumnName("remainingAmount");

            entity.HasOne(d => d.Purchase).WithMany()
                .HasForeignKey(d => d.PurchaseId)
                .HasConstraintName("FK__Claims__Purchase__4CA06362");
        });

        modelBuilder.Entity<Policy>(entity =>
        {
            entity.HasKey(e => e.PolicyId).HasName("PK__Policies__2E133944179CA897");

            entity.Property(e => e.PolicyId)
                .ValueGeneratedNever()
                .HasColumnName("PolicyID");
            entity.Property(e => e.Available)
                .HasMaxLength(1)
                .IsUnicode(false)
                .IsFixedLength();
            entity.Property(e => e.InsuranceAmount).HasColumnType("money");
            entity.Property(e => e.PolicyDescription)
                .HasMaxLength(200)
                .IsUnicode(false);
            entity.Property(e => e.PolicyName)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.PolicyType)
                .HasMaxLength(20)
                .IsUnicode(false);
        });

        modelBuilder.Entity<PolicySold>(entity =>
        {
            entity.HasKey(e => e.PurchaseId).HasName("PK__PolicySo__6B0A6BBE89D1E18A");

            entity.ToTable("PolicySold");

            entity.Property(e => e.Amount).HasColumnType("money");
            entity.Property(e => e.Duration).HasColumnName("duration");
            entity.Property(e => e.PolicyId).HasColumnName("PolicyID");
            entity.Property(e => e.SoldDate).HasColumnType("datetime");
            entity.Property(e => e.UserId).HasColumnName("UserID");

            entity.HasOne(d => d.Policy).WithMany(p => p.PolicySolds)
                .HasForeignKey(d => d.PolicyId)
                .HasConstraintName("FK__PolicySol__Polic__4AB81AF0");

            entity.HasOne(d => d.User).WithMany(p => p.PolicySolds)
                .HasForeignKey(d => d.UserId)
                .HasConstraintName("FK__PolicySol__UserI__49C3F6B7");
        });

        modelBuilder.Entity<User>(entity =>
        {
            entity.HasKey(e => e.UserId).HasName("PK__Users__1788CCAC6C5EB5F6");

            entity.HasIndex(e => e.ContactNo, "UQ__Users__5C667C05ECC74897").IsUnique();

            entity.HasIndex(e => e.Email, "UQ__Users__A9D105343B73404B").IsUnique();

            entity.HasIndex(e => e.UserName, "UQ__Users__C9F28456216141F6").IsUnique();

            entity.Property(e => e.UserId).HasColumnName("UserID");
            entity.Property(e => e.Address)
                .HasMaxLength(200)
                .IsUnicode(false);
            entity.Property(e => e.ContactNo)
                .HasMaxLength(20)
                .IsUnicode(false);
            entity.Property(e => e.Email)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.FirstName)
                .HasMaxLength(20)
                .IsUnicode(false);
            entity.Property(e => e.Gender)
                .HasMaxLength(1)
                .IsUnicode(false)
                .IsFixedLength();
            entity.Property(e => e.LastName)
                .HasMaxLength(20)
                .IsUnicode(false);
            entity.Property(e => e.Password)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.UserName)
                .HasMaxLength(25)
                .IsUnicode(false);
            entity.Property(e => e.UserType)
                .HasMaxLength(20)
                .IsUnicode(false);
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
