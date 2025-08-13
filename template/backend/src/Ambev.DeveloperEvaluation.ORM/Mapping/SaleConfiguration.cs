using Ambev.DeveloperEvaluation.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Ambev.DeveloperEvaluation.ORM.Mapping;

public class SaleConfiguration : IEntityTypeConfiguration<Sale>
{
    public void Configure(EntityTypeBuilder<Sale> builder)
    {
        builder.ToTable("Sales");

        builder.HasKey(s => s.Id);

        builder.Property(s => s.SaleNumber)
            .IsRequired()
            .HasMaxLength(50);

        builder.Property(s => s.SaleDate)
            .IsRequired();

        builder.Property(s => s.CustomerId)
            .IsRequired();

        builder.Property(s => s.CustomerName)
            .IsRequired()
            .HasMaxLength(200);

        builder.Property(s => s.BranchId)
            .IsRequired();

        builder.Property(s => s.BranchName)
            .IsRequired()
            .HasMaxLength(200);

        builder.Property(s => s.TotalAmount)
            .HasColumnType("decimal(18,2)")
            .IsRequired();

        builder.Property(s => s.IsCancelled)
            .IsRequired();

        // Configure SaleItem as owned collection
        builder.OwnsMany(s => s.Items, item =>
        {
            item.ToTable("SaleItems");
            item.WithOwner().HasForeignKey("SaleId");

            item.HasKey("SaleId", "ProductId");

            item.Property(i => i.ProductId)
                .IsRequired();

            item.Property(i => i.ProductName)
                .IsRequired()
                .HasMaxLength(200);

            item.Property(i => i.Quantity)
                .IsRequired();

            item.Property(i => i.UnitPrice)
                .HasColumnType("decimal(18,2)")
                .IsRequired();

            item.Property(i => i.Discount)
                .HasColumnType("decimal(18,2)")
                .IsRequired();

            item.Property(i => i.TotalAmount)
                .HasColumnType("decimal(18,2)")
                .IsRequired();
        });

        // Add mapping for UserId
        builder.Property(s => s.UserId)
            .IsRequired();

        // Optionally, if you want to set up a relationship:
        // builder.HasOne<User>()
        //     .WithMany()
        //     .HasForeignKey(s => s.UserId);
    }
}