using System.Linq;
using FluentValidation.Results;
using Microsoft.EntityFrameworkCore;
using NSE.Cart.API.Model;

namespace NSE.Cart.API.Data
{
    public class CartContext : DbContext
    {
        public CartContext(DbContextOptions<CartContext> options) : base(options)
        {
            ChangeTracker.QueryTrackingBehavior = QueryTrackingBehavior.NoTracking;
            ChangeTracker.AutoDetectChangesEnabled = false;
        }

        public DbSet<CartItem> CartItems { get; set; }
        public DbSet<CartClient> CartClient { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Ignore<ValidationResult>();

            foreach (var property in modelBuilder.Model.GetEntityTypes().SelectMany(
                e => e.GetProperties().Where(p => p.ClrType == typeof(string))))
                property.SetColumnType("varchar(100)");

            modelBuilder.Entity<CartClient>()
                .HasIndex(c => c.ClientId)
                .HasName("IDX_Client");

            modelBuilder.Entity<CartClient>()
                .Ignore(c => c.Voucher)
                .OwnsOne(c => c.Voucher, v =>
                {
                    v.Property(vc => vc.Code)
                        .HasColumnName("VoucherCode")
                        .HasColumnType("varchar(50)");

                    v.Property(vc => vc.DiscountType)
                        .HasColumnName("DiscountType");

                    v.Property(vc => vc.Percent)
                        .HasColumnName("Percent");

                    v.Property(vc => vc.ValueDiscount)
                        .HasColumnName("ValueDiscount");
                });

            modelBuilder.Entity<CartClient>()
                .HasMany(c => c.Items)
                .WithOne(i => i.CartClient)
                .HasForeignKey(c => c.CartId);

            foreach (var relationship in modelBuilder.Model.GetEntityTypes()
                .SelectMany(e => e.GetForeignKeys())) relationship.DeleteBehavior = DeleteBehavior.ClientSetNull;
        }
    }
}
