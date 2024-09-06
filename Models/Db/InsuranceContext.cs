using System;
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
            entity.HasKey(e => e.BeneficiaryId).HasName("PK__Benefici__3FBA95D58C42652C");

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
                .HasConstraintName("FK__Beneficia__UserN__30C33EC3");
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
                .HasConstraintName("FK__Cart__PolicyId__1DB06A4F");

            entity.HasOne(d => d.User).WithMany()
                .HasForeignKey(d => d.UserId)
                .HasConstraintName("FK__Cart__UserID__1CBC4616");
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
                .HasConstraintName("FK__Claims__Purchase__2DE6D218");
        });

        modelBuilder.Entity<Policy>(entity =>
        {
            entity.HasKey(e => e.PolicyId).HasName("PK__Policies__2E1339441C9DAC14");

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
            entity.HasKey(e => e.PurchaseId).HasName("PK__PolicySo__6B0A6BBECFDD0E0E");

            entity.ToTable("PolicySold");

            entity.Property(e => e.Amount).HasColumnType("money");
            entity.Property(e => e.Duration).HasColumnName("duration");
            entity.Property(e => e.PolicyId).HasColumnName("PolicyID");
            entity.Property(e => e.SoldDate).HasColumnType("datetime");
            entity.Property(e => e.UserId).HasColumnName("UserID");

            entity.HasOne(d => d.Policy).WithMany(p => p.PolicySolds)
                .HasForeignKey(d => d.PolicyId)
                .HasConstraintName("FK__PolicySol__Polic__2BFE89A6");

            entity.HasOne(d => d.User).WithMany(p => p.PolicySolds)
                .HasForeignKey(d => d.UserId)
                .HasConstraintName("FK__PolicySol__UserI__2B0A656D");
        });

        modelBuilder.Entity<User>(entity =>
        {
            entity.HasKey(e => e.UserId).HasName("PK__Users__1788CCAC8D1CDE3C");

            entity.HasIndex(e => e.ContactNo, "UQ__Users__5C667C05798171E3").IsUnique();

            entity.HasIndex(e => e.Email, "UQ__Users__A9D105342FB18C33").IsUnique();

            entity.HasIndex(e => e.UserName, "UQ__Users__C9F28456CD05A6C0").IsUnique();

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
