using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Store.Entities;

namespace Store.Memory.Configurations;
internal sealed class ProductConfiguration : IEntityTypeConfiguration<ProductEntity>
{
    public void Configure(EntityTypeBuilder<ProductEntity> builder)
    {
        builder.ToTable("Books", Options.Scheme);

        builder.HasKey(c=> c.ID).HasName("PK_Products");

        builder.HasIndex(c => c.ISBN, "UI_ISBN_Books").IsUnique();

        builder.Property(dto => dto.ID).HasColumnName("ID").ValueGeneratedOnAdd();
        builder.Property(dto => dto.Price).HasColumnName("Price").HasColumnType("money");
        builder.Property(dto => dto.PublishHousingID).HasColumnName("PublishHousingID");
        builder.Property(dto => dto.AuthorID).HasColumnName("AuthorID");

        builder.Property(dto => dto.Title)
                .HasMaxLength(50)
                .IsRequired()
                .HasColumnName("Title");

        builder.Property(dto => dto.ISBN)
                .HasMaxLength(35)
                .HasColumnName("ISBN");

        builder.Property(dto => dto.Category)
                .HasMaxLength(50)
                .IsRequired()
                .HasColumnName("Category");

        builder.Property(dto => dto.Description)
            .HasMaxLength(1000)
            .HasColumnName("Description");

        builder.HasOne(c => c.PublishHousing)
            .WithMany()
            .HasForeignKey(c => c.PublishHousingID)
            .OnDelete(DeleteBehavior.Cascade)
            .HasConstraintName("FK_PublishingHouses");

        builder.HasOne(c => c.Author)
                .WithMany(c => c.Books)
                .HasForeignKey(c => c.AuthorID)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("FK_Authors");

        builder.HasData(
            new ProductEntity
            {
                ID = 1,
                PublishHousingID = 1,
                Title = "яйца",
                Category = "яйца",
                AuthorID = 1,
                Description = "куринные яйца, категории C0",
                Price = 30m
            },
            //db.Products.Add(new Product("яйца", manufactures.GetById(1), "яйца", 10m, "куринные яйца, категории C0"));
            new ProductEntity
            {
                ID = 2,
                PublishHousingID = 2,
                Title = "хлеб",
                Category = "выпечка",
                AuthorID = 1,
                Description = "хлебо-булочные изделия",
                Price = 20m
            },
            //db.Products.Add(new Product("хлеб", manufactures.GetById(2), "выпечка", 20m, "хлебо-булочные изделия"));
            new ProductEntity
            {
                ID = 3,
                PublishHousingID = 3,
                Title = "говядина",
                Category = "мясо",
                AuthorID = 1,
                Description = "мясо из говядины и телятины",
                Price = 30m
            },
            //db.Products.Add(new Product("говядина", manufactures.GetById(3), "мясо", 30m, "мясо из говядины и телятины"));
            new ProductEntity
            {
                ID = 4,
                PublishHousingID = 4,
                Title = "свинина",
                AuthorID = 1,
                Category = "мясо",
                Description = "мясо из свинины",
                Price = 40m
            });
            //db.Products.Add(new Product("свинина", manufactures.GetById(4), "мясо", 40m, "мясо из свинины"));
    }
}
