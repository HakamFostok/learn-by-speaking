using LearnBySpeaking.Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace LearnBySpeaking.Infra.Data.Context
{
    public partial class BTContext : DbContext
    {
        public BTContext()
        {
        }

        public BTContext(DbContextOptions<BTContext> options)
            : base(options)
        {
        }

        public virtual DbSet<AppParameter> AppParameter { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<AppParameter>(entity =>
            {
                entity.HasKey(x => x.Id);

                entity.Property(e => e.CreateDate).HasColumnType("datetime");

                entity.Property(e => e.CreateUser).IsRequired();

                entity.Property(e => e.Description).IsRequired();

                entity.Property(e => e.Name).IsRequired();

                entity.Property(e => e.UpdateDate).HasColumnType("datetime");

                entity.Property(e => e.Value).IsRequired();
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}