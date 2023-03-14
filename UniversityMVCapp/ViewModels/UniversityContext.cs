using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using UniversityMVCapp.Models;

namespace UniversityMVCapp.ViewModels;

public class UniversityContext : DbContext
{
	public DbSet<Course> Courses { get; set; } = null!;
	public DbSet<Group> Groups { get; set; } = null!;
	public DbSet<Student> Students { get; set; } = null!;

	public UniversityContext(DbContextOptions<UniversityContext> options)
		: base(options)
	{
	}

	protected override void OnModelCreating(ModelBuilder modelBuilder)
	{
		modelBuilder.Entity<Course>(entity =>
		{
			entity.HasKey(c => c.CourseId).HasName("PK_Courses_CourseId");
			entity.HasIndex(c => c.Name).HasName("UQ_Courses_Name").IsUnique();
			entity.Property(c => c.Name).HasMaxLength(50);
		});

		modelBuilder.Entity<Group>(entity =>
		{
			entity.HasKey(g => g.GroupId).HasName("PK_Groups_GroupId");
			entity.HasAlternateKey(g => g.Name).HasName("UQ_Groups_Name");
			entity.Property(g => g.Name).HasMaxLength(50);
			entity.HasOne(g => g.Course).WithMany(c => c.Groups)
				.HasForeignKey(g => g.CourseId)
				.OnDelete(DeleteBehavior.Restrict)
				.HasConstraintName("FK_Groups_Courses");
		});

		modelBuilder.Entity<Student>(entity =>
		{
			entity.HasKey(s => s.StudentId).HasName("PK_Students_StudentId");
			entity.Property(s => s.FirstName).HasMaxLength(50);
			entity.Property(s => s.LastName).HasMaxLength(50);
			entity.HasOne(s => s.Group).WithMany(g => g.Students)
				.HasForeignKey(s => s.GroupId)
				.OnDelete(DeleteBehavior.Restrict)
				.HasConstraintName("FK_Students_Groups");
		});
	}
}
