using LearnBySpeaking.Domain.Models.MongoModel;
using Microsoft.EntityFrameworkCore;

namespace LearnBySpeaking.Infra.Data.Context
{
    public class EventStoreContext : DbContext
    {
        public DbSet<MongoEventModel> MongoEventModel { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<MongoEventModel>(entity =>
            {
                entity.HasKey(x => x.Id);

                entity.Property(e => e.Data).IsRequired();

                entity.Property(e => e.ActionType).IsRequired();

                entity.Property(e => e.ActionDate).IsRequired();
            });

            base.OnModelCreating(modelBuilder);
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);
        }
    }
}