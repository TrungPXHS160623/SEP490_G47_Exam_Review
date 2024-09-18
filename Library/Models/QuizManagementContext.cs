using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace Library.Models;

public partial class QuizManagementContext : DbContext
{
    public QuizManagementContext()
    {
    }

    public QuizManagementContext(DbContextOptions<QuizManagementContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Account> Accounts { get; set; }

    public virtual DbSet<MainQuestion> MainQuestions { get; set; }

    public virtual DbSet<QuestionHistory> QuestionHistories { get; set; }

    public virtual DbSet<QuestionSubject> QuestionSubjects { get; set; }

    public virtual DbSet<Role> Roles { get; set; }

    public virtual DbSet<SubQuestion> SubQuestions { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Data Source=DESKTOP-97B2SVH;Initial Catalog=QuizManagement;User ID=sa;Password=123;TrustServerCertificate=True");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Account>(entity =>
        {
            entity.ToTable("Account");

            entity.Property(e => e.AccountId).HasColumnName("account_id");
            entity.Property(e => e.Email)
                .HasMaxLength(250)
                .HasColumnName("email");
            entity.Property(e => e.Password)
                .HasMaxLength(250)
                .HasColumnName("password");
            entity.Property(e => e.RoleId).HasColumnName("role_id");

            entity.HasOne(d => d.Role).WithMany(p => p.Accounts)
                .HasForeignKey(d => d.RoleId)
                .HasConstraintName("FK_Account_Role");
        });

        modelBuilder.Entity<MainQuestion>(entity =>
        {
            entity.HasKey(e => e.MainId);

            entity.ToTable("MainQuestion");

            entity.Property(e => e.MainId).HasColumnName("main_id");
            entity.Property(e => e.Images)
                .HasMaxLength(250)
                .HasColumnName("images");
            entity.Property(e => e.MainContent)
                .HasMaxLength(250)
                .HasColumnName("main_content");
            entity.Property(e => e.QuestionType).HasColumnName("question_type");
            entity.Property(e => e.SubjectId)
                .HasMaxLength(10)
                .IsUnicode(false)
                .HasColumnName("subject_id");

            entity.HasOne(d => d.Subject).WithMany(p => p.MainQuestions)
                .HasForeignKey(d => d.SubjectId)
                .HasConstraintName("FK_MainQuestion_QuestionSubject");
        });

        modelBuilder.Entity<QuestionHistory>(entity =>
        {
            entity.ToTable("QuestionHistory");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.AccountId).HasColumnName("account_id");
            entity.Property(e => e.MainId).HasColumnName("main_id");
            entity.Property(e => e.UpdateDt)
                .HasColumnType("datetime")
                .HasColumnName("update_dt");

            entity.HasOne(d => d.Account).WithMany(p => p.QuestionHistories)
                .HasForeignKey(d => d.AccountId)
                .HasConstraintName("FK_QuestionHistory_Account");

            entity.HasOne(d => d.Main).WithMany(p => p.QuestionHistories)
                .HasForeignKey(d => d.MainId)
                .HasConstraintName("FK_QuestionHistory_MainQuestion");
        });

        modelBuilder.Entity<QuestionSubject>(entity =>
        {
            entity.HasKey(e => e.SubjectId);

            entity.ToTable("QuestionSubject");

            entity.Property(e => e.SubjectId)
                .HasMaxLength(10)
                .IsUnicode(false)
                .HasColumnName("subject_id");
            entity.Property(e => e.SubjectName)
                .HasMaxLength(50)
                .HasColumnName("subject_name");
            entity.Property(e => e.Time)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("time");
        });

        modelBuilder.Entity<Role>(entity =>
        {
            entity.ToTable("Role");

            entity.Property(e => e.RoleId)
                .HasColumnName("role_id");
            entity.Property(e => e.RoleName)
                .HasMaxLength(250)
                .HasColumnName("role_name");
        });

        modelBuilder.Entity<SubQuestion>(entity =>
        {
            entity.HasKey(e => e.SubId);

            entity.ToTable("SubQuestion");

            entity.Property(e => e.SubId)
                .HasColumnName("sub_id");
            entity.Property(e => e.IsAnswer)
                .HasMaxLength(10)
                .IsFixedLength()
                .HasColumnName("is_answer");
            entity.Property(e => e.MainId).HasColumnName("main_id");
            entity.Property(e => e.SubContent)
                .HasMaxLength(250)
                .HasColumnName("sub_content");

            entity.HasOne(d => d.Main).WithMany(p => p.SubQuestions)
                .HasForeignKey(d => d.MainId)
                .HasConstraintName("FK_SubQuestion_MainQuestion");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
