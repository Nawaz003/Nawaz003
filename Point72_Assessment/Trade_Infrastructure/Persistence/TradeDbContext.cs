using Microsoft.EntityFrameworkCore;
using Trade_Domain.Entities;


namespace Trade_Infrastructure.Persistence
{
    public class TradeDbContext : DbContext
    {
        public TradeDbContext(DbContextOptions<TradeDbContext> options) : base(options) { }
        public DbSet<Trade> Trades { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Trade>(entity =>
            {
                entity.HasKey(e => e.Id);

                entity.Property(e => e.Symbol)
                    .IsRequired()
                    .HasMaxLength(10);

                entity.Property(e => e.Quantity)
                    .HasPrecision(18, 4);

                entity.Property(e => e.Price)
                    .HasPrecision(18, 2);

                entity.Property(e => e.Type)
                    .IsRequired();

                entity.Property(e => e.TradeDate)
                    .IsRequired();

                entity.HasIndex(e => e.Symbol);
                entity.HasIndex(e => e.TradeDate);
            });
        }
    }
}
