using CodingAssessment.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;
using System.Reflection.Emit;

namespace CodingAssessment.Infrastructure
{
    public class DbContext : Microsoft.EntityFrameworkCore.DbContext
    {
        public DbContext(DbContextOptions<DbContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Meta> Meta { get; set; }
        public virtual DbSet<Result> Result { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Meta>()
                        .HasMany(c => c.results)
                        .WithOne(e => e.Meta);

            modelBuilder.Entity<Meta>()
                        .HasMany(o => o.results)
                        .WithOne(oi => oi.Meta)
                        .HasForeignKey(oi => oi.MetaId);
        }

    }
}
