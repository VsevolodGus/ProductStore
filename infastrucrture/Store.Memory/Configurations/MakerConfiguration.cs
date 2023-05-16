using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Store.Data;

namespace Store.Memory.Configurations;
internal sealed class MakerConfiguration : IEntityTypeConfiguration<MakerEntity>
{
    public void Configure(EntityTypeBuilder<MakerEntity> builder)
    {
        builder.ToTable("Makers", Options.Scheme);

        builder.HasKey(c => c.ID).HasName("PK_Makers");

        builder.Property(c => c.ID).HasColumnName("ID")
            .ValueGeneratedOnAdd();

        builder.Property(dto => dto.Title)
                .IsRequired()
                .HasMaxLength(50)
                .HasColumnName("Title");

        builder.Property(dto => dto.Email)
                .HasMaxLength(100)
                .HasColumnName("Email");
        
        builder.Property(dto => dto.Address)
                .HasMaxLength(1000)
                .HasColumnName("Address");
        
        builder.Property(dto => dto.Description)
                .HasMaxLength(1000)
                .HasColumnName("Description");

        builder.Property(dto => dto.NumberPhone)
                .HasMaxLength(15)
                .HasColumnName("NumberPhone");

        builder.HasData(
            new MakerEntity
            {
                ID = 1,
                Title = "Красная цена ",
                Email = "redPrice@gmail.com",
                NumberPhone = "8937-216-76-11",
                Address = "ООО 'Красная Цена' Самара",
                Description = "Название Красная Цена выбрано не случайно!"
            },
            //db.Makers.Add(new Maker("Красная Цена", "8937-216-76-11", "",
            //    "ООО 'Красная Цена' Самара", "Название Красная Цена выбрано не случайно!"));
            new MakerEntity
            {
                ID = 2,
                Title = "АЛМА",
                Email = "alma@mail.ru",
                NumberPhone = "8347-827-36-96",
                Address = "ул.Комарова, д.41;",
                Description = "У нас вы найдете экологически чистые продукты по приятным ценам"
            },
            //db.Makers.Add(new Maker("АЛМА", "8347-827-36-96", "",
            //    "ул.Комарова, д.41;", "У нас вы найдете экологически чистые продукты по приятным ценам"));
            new MakerEntity
            {
                ID = 3,
                Title = "Мясницкий ряд",
                Email = "zakupki@kolbasa.ru",
                NumberPhone = "+7495-411-33-41",
                Address = "Транспортный проезд д.7 г. Одинцово Московская область",
                Description = "Компания «Мясницкий ряд» основана в 2004 году на базе Первого Одинцовского мясокомбината. " +
                              "Наша компания активно растёт и развивается, " +
                              "регулярно расширяя ассортимент и повышая качество выпускаемой продукции."
            },
            //db.Makers.Add(new Maker("Мясницкий ряд", " +7495-411-33-41", " zakupki@kolbasa.ru",
            //    "Транспортный проезд д.7 г. Одинцово Московская область", ""));
            new MakerEntity
            {
                ID = 4,
                Title = "Черкизово",
                Email = "sk@cherkizovo.com",
                NumberPhone = "+7495-660-24-40",
                Address = "Москва 125047 Лесная улица 5Б, бизнес - центр «Белая площадь», 12 - й этаж",
                Description = "Мясное производство для нас не просто бизнес. " +
                                "Делать лучшие в стране продукты питания — наша страсть и призвание."

            });
        //db.Makers.Add(new Maker("Черкизово", "+7495-660-24-40", " sk@cherkizovo.com",
        //    "Москва 125047 Лесная улица 5Б, бизнес - центр «Белая площадь», 12 - й этаж", ""));
    }
}
