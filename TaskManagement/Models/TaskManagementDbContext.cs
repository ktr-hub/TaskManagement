using System;
using System.Collections.Generic;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;

namespace TaskManagement.Models;

public partial class TaskManagementDbContext : DbContext
{
    private TaskManagementDbContext _instance;

    public TaskManagementDbContext()
    {
    }

    public TaskManagementDbContext(DbContextOptions<TaskManagementDbContext> options)
        : base(options)
    {
    }

    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="procedureName"></param>
    /// <param name="parameters"></param>
    /// <returns></returns>
    public IEnumerable<T>? ExecuteStoredProcedure<T>(string procedureName, SqlParameter[] parameters)
    {
        if (parameters.Length == 0)
        {

            var result = this.Database.SqlQueryRaw<T>(procedureName);
            return result.ToList();
        }
        return null;
    }

    public static TaskManagementDbContext GetInstance()
    {
        var options = new DbContextOptionsBuilder<TaskManagementDbContext>()
            .UseSqlServer("Server=(localdb)\\mssqllocaldb;Database=TaskManagementDB;Trusted_Connection=True;")
            .Options;
        TaskManagementDbContext taskManagementDbContext = new TaskManagementDbContext(options);
        return taskManagementDbContext;
    }

    public virtual DbSet<Employee> Employees { get; set; }

    public virtual DbSet<Project> Projects { get; set; }

    public virtual DbSet<TaskModel> Tasks { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Server=(localdb)\\mssqllocaldb;Database=TaskManagementDB;Trusted_Connection=True;");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Employee>(entity =>
        {
            entity.ToTable("Employee");

            entity.Property(e => e.EmployeeId).HasColumnName("Employee_Id");
            entity.Property(e => e.EmployeeName)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("Employee_Name");
            entity.Property(e => e.Mid)
                .HasMaxLength(10)
                .IsUnicode(false)
                .HasColumnName("MID");
        });

        modelBuilder.Entity<Project>(entity =>
        {
            entity.ToTable("Project");

            entity.Property(e => e.ProjectId).HasColumnName("Project_Id");
            entity.Property(e => e.ProjectName)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("Project_Name");
        });

        modelBuilder.Entity<TaskModel>(entity =>
        {
            entity.ToTable("Task");

            entity.Property(e => e.TaskId).HasColumnName("Task_Id");
            entity.Property(e => e.DueDate)
                .HasColumnType("date")
                .HasColumnName("Due_Date");
            entity.Property(e => e.EmployeeId).HasColumnName("Employee_Id");
            entity.Property(e => e.ProjectId).HasColumnName("Project_Id");
            entity.Property(e => e.StartDate)
                .HasColumnType("date")
                .HasColumnName("Start_Date");
            entity.Property(e => e.TaskDescription)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("Task_Description");

            entity.HasOne(d => d.Employee).WithMany(p => p.Tasks)
                .HasForeignKey(d => d.EmployeeId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Task_Employee");

            entity.HasOne(d => d.Project).WithMany(p => p.Tasks)
                .HasForeignKey(d => d.ProjectId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Task_Project");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
