using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Migrations.Operations;

namespace littleviewservice.Models
{
    public class LittleContext : DbContext
    {
        public LittleContext(DbContextOptions<LittleContext> options) : base(options) 
        {

        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Student>()
                .HasKey(s => s.ID); 
        }
        public DbSet<Little> Littles { get; set; }
        public DbSet<Account> tbl_account { get; set; }
        public DbSet<Student> vw_students { get; set; }
        public DbSet<StudentProfile> tbl_student { get; set; }
        public DbSet<StudentImage> vw_profile_image { get; set; }
        public DbSet<Notification> tbl_notification { get; set; }
        public DbSet<Activity> tbl_activity { get; set; }
        public DbSet<Lesson> tbl_weekly_program { get; set; }
        public DbSet<Food> tbl_food_list { get; set; }
        public DbSet<StudentAttendance> view_attendance { get; set; }
        public DbSet<Attendance> tbl_attendance { get; set; }
    }
}
