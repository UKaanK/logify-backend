using logifly.domain.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace logifly.persistence.Contexts
{
    public class LogiflyDbContext:DbContext
    {
        public LogiflyDbContext(DbContextOptions<LogiflyDbContext> options):base(options)
        {
            
        }
        public DbSet<Ticket> Tickets => Set<Ticket>();
        public DbSet<TicketLog> TicketLogs => Set<TicketLog>();

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Ticket>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.Property(e => e.Title).IsRequired().HasMaxLength(200);
                entity.Property(e => e.Message).IsRequired();
                entity.Property(e=>e.Status).IsRequired();
                entity.Property(e=>e.CreatedAt).IsRequired();
            });

            modelBuilder.Entity<TicketLog>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.Property(e => e.LogType).IsRequired().HasMaxLength(200);
                entity.Property(e => e.Content).IsRequired();
                entity.Property(e => e.CreatedAt).IsRequired();

                entity.HasOne(e=>e.Ticket).WithMany(t=>t.Logs).HasForeignKey(e=>e.TicketId).OnDelete(DeleteBehavior.Cascade);
            });
        }
    }
}
