using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Store.Data;

namespace Store.Memory.Configurations;
internal sealed class ProductConfiguration : IEntityTypeConfiguration<ProductEntity>
{
    public void Configure(EntityTypeBuilder<ProductEntity> builder)
    {
        builder.ToTable("Products", Options.Scheme);

        builder.HasKey(c=> c.ID).HasName("PK_Products");

        builder.Property(dto => dto.ID).HasColumnName("ID").ValueGeneratedOnAdd();
        builder.Property(dto => dto.Price).HasColumnName("Price");
        builder.Property(dto => dto.MakerID).HasColumnName("MakerID");

        builder.Property(dto => dto.Title)
                .HasMaxLength(50)
                .IsRequired()
                .HasColumnName("Title");

        builder.Property(dto => dto.Category)
                .HasMaxLength(50)
                .IsRequired()
                .HasColumnName("Category");

        builder.Property(dto => dto.Description)
            .HasMaxLength(1000)
            .HasColumnName("Description");

        builder.Property(dto => dto.MakerID).HasColumnName("MakerID");
        builder.Property(dto => dto.Price).HasColumnName("Price");

        builder.HasOne(c => c.Maker)
            .WithMany()
            .HasForeignKey(c => c.MakerID)
            .OnDelete(DeleteBehavior.Cascade)
            .HasConstraintName("FK_Maker");

        builder.HasData(
            new ProductEntity
            {
                ID = 1,
                MakerID = 1,
                Title = "яйца",
                Category = "яйца",
                Description = "куринные яйца, категории C0",
                Price = 30m
            },
            //db.Products.Add(new Product("яйца", manufactures.GetById(1), "яйца", 10m, "куринные яйца, категории C0"));
            new ProductEntity
            {
                ID = 2,
                MakerID = 2,
                Title = "хлеб",
                Category = "выпечка",
                Description = "хлебо-булочные изделия",
                Price = 20m
            },
            //db.Products.Add(new Product("хлеб", manufactures.GetById(2), "выпечка", 20m, "хлебо-булочные изделия"));
            new ProductEntity
            {
                ID = 3,
                MakerID = 3,
                Title = "говядина",
                Category = "мясо",
                Description = "мясо из говядины и телятины",
                Price = 30m
            },
            //db.Products.Add(new Product("говядина", manufactures.GetById(3), "мясо", 30m, "мясо из говядины и телятины"));
            new ProductEntity
            {
                ID = 4,
                MakerID = 4,
                Title = "свинина",
                Category = "мясо",
                Description = "мясо из свинины",
                Price = 40m
            });
            //db.Products.Add(new Product("свинина", manufactures.GetById(4), "мясо", 40m, "мясо из свинины"));
    }
}
