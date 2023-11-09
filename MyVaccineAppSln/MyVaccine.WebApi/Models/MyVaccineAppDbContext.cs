﻿using Microsoft.EntityFrameworkCore;

namespace MyVaccine.WebApi.Models;

public class MyVaccineAppDbContext : DbContext
{
    public MyVaccineAppDbContext(DbContextOptions<MyVaccineAppDbContext> options) : base(options)
    {

    }
    public DbSet<User> Users { get; set; }
    public DbSet<Dependent> Dependents { get; set; }
    public DbSet<VaccineCategory> VaccineCategories { get; set; }
    public DbSet<Vaccine> Vaccines { get; set; }
    public DbSet<VaccineRecord> VaccineRecords { get; set; }
    public DbSet<Allergy> Allergies { get; set; }
    public DbSet<FamilyGroup> FamilyGroups { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<User>(entity =>
        {
            entity.Property(u => u.UserName)
                .IsRequired()
                .HasMaxLength(255);

            entity.Property(u => u.Email)
                .IsRequired()
                .HasMaxLength(255);

            entity.Property(u => u.Password)
                .IsRequired()
                .HasMaxLength(255);
        });

        modelBuilder.Entity<Dependent>(entity =>
        {
            entity.Property(d => d.Name)
                .IsRequired()
                .HasMaxLength(255);

            entity.HasOne(d => d.User)
                .WithMany(u => u.Dependents)
                .HasForeignKey(d => d.UserId)
                .OnDelete(DeleteBehavior.Cascade);
        });

        modelBuilder.Entity<VaccineCategory>(entity =>
        {
            entity.Property(vc => vc.Name)
                .IsRequired()
                .HasMaxLength(100);
        });

        modelBuilder.Entity<Vaccine>(entity =>
        {
            entity.Property(v => v.Name)
                .IsRequired()
                .HasMaxLength(255);

            entity.HasMany(v => v.Categories)
                .WithMany(vc => vc.Vaccines)
                .UsingEntity(j => j.ToTable("VaccineCategoryVaccines"));
        });

        modelBuilder.Entity<VaccineRecord>(entity =>
        {
            entity.Property(vr => vr.AdministeredLocation)
                .HasMaxLength(255);

            entity.Property(vr => vr.AdministeredBy)
                .HasMaxLength(255);

            entity.HasOne(vr => vr.User)
                .WithMany(u => u.VaccineRecords)
                .HasForeignKey(vr => vr.UserId)
                .OnDelete(DeleteBehavior.NoAction);
            entity.HasOne(vr => vr.Dependent)
                .WithMany(d => d.VaccineRecords)
                .HasForeignKey(vr => vr.DependentId)
                .OnDelete(DeleteBehavior.Cascade);

            entity.HasOne(vr => vr.Vaccine)
                .WithMany()
                .HasForeignKey(vr => vr.VaccineId)
                .OnDelete(DeleteBehavior.Cascade);
        });

        modelBuilder.Entity<Allergy>(entity =>
        {
            entity.Property(a => a.Name)
                .IsRequired()
                .HasMaxLength(100);

            entity.HasOne(a => a.User)
                .WithMany(u => u.Allergies)
                .HasForeignKey(a => a.UserId)
                .OnDelete(DeleteBehavior.Cascade);
        });

        modelBuilder.Entity<FamilyGroup>(entity =>
        {
            entity.Property(fg => fg.Name)
                .IsRequired()
                .HasMaxLength(255);
        });
    }
}