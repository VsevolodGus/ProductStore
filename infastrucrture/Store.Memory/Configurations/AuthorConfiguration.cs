using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Store.Entities;

namespace Store.Memory.Configurations;

internal sealed class AuthorConfiguration : IEntityTypeConfiguration<Author>
{
    public void Configure(EntityTypeBuilder<Author> builder)
    {
        builder.ToTable("Authors", Options.Scheme);

        builder.HasKey(c=> c.ID).HasName("PK_Authors");

        builder.Property(dto => dto.ID).HasColumnName("ID").ValueGeneratedOnAdd();
        builder.Property(dto => dto.FirstName).IsRequired().HasMaxLength(100).HasColumnName("FirstName");
        builder.Property(dto => dto.SecondName).IsRequired().HasMaxLength(100).HasColumnName("SecondName");
        builder.Property(dto => dto.LastName).IsRequired().HasMaxLength(100).HasColumnName("LastName");
        builder.Property(dto => dto.Description).HasMaxLength(1000).HasColumnName("Description");

        builder.HasData(
            new Author
            {
                ID = 1,
                FirstName = "Test",
                SecondName = "Test",
                LastName = "Test",
                Description = "куринные яйца, категории C0",
            });
            //db.Products.Add(new Product("яйца", manufactures.GetById(1), "яйца", 10m, "куринные яйца, категории C0"));
    }
}
