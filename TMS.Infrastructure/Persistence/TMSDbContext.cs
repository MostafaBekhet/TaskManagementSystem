using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using TMS.Domain.Entities;

namespace TMS.Infrastructure.Persistence
{
    public class TMSDbContext(DbContextOptions<TMSDbContext> options) : IdentityDbContext<User>(options)
    {
        internal DbSet<User> Users { get; set; }

        internal DbSet<Team> Teams { get; set; }

        internal DbSet<UserTeam> UserTeams { get; set; }

        internal DbSet<TaskItem> Tasks { get; set; }

        internal DbSet<TaskAttachment> TaskAttachments { get; set; }

        internal DbSet<TaskComment> TaskComments { get; set; }

        internal DbSet<TaskReminder> TaskReminders { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.HasDefaultSchema("TMS");

            SetUsersTeamsRelation(modelBuilder);

            SetUserTeamTaskRelation(modelBuilder);

            SetUserTaskCommentRelation(modelBuilder);

            SetTaskAttachmentRelation(modelBuilder);

            SetUserTaskReminderRelation(modelBuilder);
        }

        private void SetUsersTeamsRelation(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<UserTeam>()
                        .HasKey(t => new { t.UserId, t.TeamId });

            modelBuilder.Entity<UserTeam>()
                        .HasOne(t => t.User)
                        .WithMany(t => t.UserTeams)
                        .HasForeignKey(t => t.UserId);

            modelBuilder.Entity<UserTeam>()
                        .HasOne(t => t.Team)
                        .WithMany(t => t.UserTeams)
                        .HasForeignKey(t => t.TeamId);
        }

        private void SetUserTeamTaskRelation(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<TaskItem>()
                        .HasOne(t => t.CreatedByUser)
                        .WithMany(t => t.CreatedTasks)
                        .HasForeignKey(t => t.CreatedByUserId)
                        .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<TaskItem>()
                        .HasOne(t => t.AssignedToUser)
                        .WithMany(t => t.AssignedTasks)
                        .HasForeignKey(t => t.AssignedToUserId)
                        .OnDelete(DeleteBehavior.SetNull);

            modelBuilder.Entity<TaskItem>()
                        .HasOne(t => t.AssigendToTeam)
                        .WithMany(t => t.AssignedTasks)
                        .HasForeignKey(t => t.AssignedToTeamId)
                        .OnDelete(DeleteBehavior.SetNull);
        }

        private void SetUserTaskCommentRelation(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<TaskComment>()
                        .HasOne(t => t.TaskItem)
                        .WithMany(t => t.TaskComments)
                        .HasForeignKey(t => t.TaskId)
                        .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<TaskComment>()
                        .HasOne(t => t.User)
                        .WithMany(t => t.TaskComments)
                        .HasForeignKey(t => t.CommentedBy)
                        .OnDelete(DeleteBehavior.Cascade);
        }

        private void SetTaskAttachmentRelation(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<TaskAttachment>()
                        .HasOne (t => t.TaskItem)
                        .WithMany(t => t.Attachments)
                        .HasForeignKey (t => t.AttachmentId)
                        .OnDelete (DeleteBehavior.Cascade);
        }

        private void SetUserTaskReminderRelation(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<TaskReminder>()
                        .HasOne(t => t.User)
                        .WithMany(t => t.TaskReminders)
                        .HasForeignKey(t => t.UserId)
                        .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<TaskReminder>()
                        .HasOne(t => t.TaskItem)
                        .WithMany(t => t.TaskReminders)
                        .HasForeignKey(t => t.TaskId)
                        .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
