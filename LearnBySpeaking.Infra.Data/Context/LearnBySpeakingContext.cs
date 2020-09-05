using LearnBySpeaking.Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace LearnBySpeaking.Infra.Data.Context
{
    public partial class LearnBySpeakingContext : DbContext
    {
        public LearnBySpeakingContext()
        {
        }

        public LearnBySpeakingContext(DbContextOptions<LearnBySpeakingContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Topic> Topic { get; set; }
        public virtual DbSet<Question> Question { get; set; }
        public virtual DbSet<Answer> Answers { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}