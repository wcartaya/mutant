using Microsoft.EntityFrameworkCore;
using Mutants.Business.Mutant;
using System;

namespace Mutants.DataAccess
{
    public class MutantDBContext : DbContext
    {
        public MutantDBContext(DbContextOptions options)
            : base(options)
        {
        }

        public DbSet<Dna> Dnas { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Dna>()
            .Property(e => e.DnaString)
            .HasConversion(
                v => string.Join(',', v),
                v => v.Split(',', StringSplitOptions.RemoveEmptyEntries));
        }
    }
}
