using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Newtonsoft.Json;
using Store.Data;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Store.Memory
{
    public class StoreDbContext : DbContext
    {

        public DbSet<ProductDto> Products { get; set; }

        public DbSet<MakerDto> Makers { get; set; }

        public DbSet<OrderDto> Orders { get; set; }

        public DbSet<OrderItemDto> OrderItems { get; set; }

        public StoreDbContext(DbContextOptions<StoreDbContext> options) : base(options)
        { }

        private static readonly ValueComparer DictionaryComparer =
           new ValueComparer<Dictionary<string, string>>(
               (dictionary1, dictionary2) => dictionary1.SequenceEqual(dictionary2),
               dictionary => dictionary.Aggregate(
                   0,
                   (a, p) => HashCode.Combine(HashCode.Combine(a, p.Key.GetHashCode()), p.Value.GetHashCode())
               )
           );

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            BuildModelProducts(modelBuilder);
            BuildModelMakers(modelBuilder);
            BuildModelOrders(modelBuilder);
            BuildModelOrderItems(modelBuilder);
        }

        private static void BuildModelProducts(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<ProductDto>(action =>
            {
                action.Property(dto => dto.Title)
                        .HasMaxLength(50)
                        .IsRequired();

                action.Property(dto => dto.Category)
                        .HasMaxLength(50)
                        .IsRequired();

                action.Property(dto => dto.IdMaker)
                        .IsRequired();

                action.Property(dto => dto.Price)
                        .HasColumnType("money");

                action.HasData(
                    new ProductDto
                    {
                        Id = 1,
                        IdMaker = 1,
                        Title = "яйца",
                        Category = "яйца",
                        Description = "куринные яйца, категории C0",
                        Price = 30m
                    },
                    //db.Products.Add(new Product("яйца", manufactures.GetById(1), "яйца", 10m, "куринные яйца, категории C0"));
                    new ProductDto
                    {
                        Id = 2,
                        IdMaker = 2,
                        Title = "хлеб",
                        Category = "выпечка",
                        Description = "хлебо-булочные изделия",
                        Price = 20m
                    },
                    //db.Products.Add(new Product("хлеб", manufactures.GetById(2), "выпечка", 20m, "хлебо-булочные изделия"));
                    new ProductDto
                    {
                        Id = 3,
                        IdMaker = 3,
                        Title = "говядина",
                        Category = "мясо",
                        Description = "мясо из говядины и телятины",
                        Price = 30m
                    },
                    //db.Products.Add(new Product("говядина", manufactures.GetById(3), "мясо", 30m, "мясо из говядины и телятины"));
                    new ProductDto
                    {
                        Id = 4,
                        IdMaker = 4,
                        Title = "свинина",
                        Category = "мясо",
                        Description = "мясо из свинины",
                        Price = 40m
                    }) ;
                    //db.Products.Add(new Product("свинина", manufactures.GetById(4), "мясо", 40m, "мясо из свинины"));
            });
        }
         
        private static void BuildModelMakers(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<MakerDto>(action =>
            {
                action.Property(dto => dto.Title)
                        .IsRequired()
                        .HasMaxLength(50);

                action.Property(dto => dto.Email)
                        .HasMaxLength(100);

                action.Property(dto => dto.NumberPhone)
                        .HasMaxLength(15);

                action.HasData(
                    new MakerDto
                    {
                        Id = 1,
                        Title="Красная цена ",
                        Email = "redPrice@gmail.com",
                        NumberPhone = "8937-216-76-11",
                        Addres = "ООО 'Красная Цена' Самара",
                        Description = "Название Красная Цена выбрано не случайно!"
                    },
                    //db.Makers.Add(new Maker("Красная Цена", "8937-216-76-11", "",
                    //    "ООО 'Красная Цена' Самара", "Название Красная Цена выбрано не случайно!"));
                    new MakerDto
                    {
                        Id = 2,
                        Title = "АЛМА",
                        Email = "alma@mail.ru",
                        NumberPhone = "8347-827-36-96",
                        Addres = "ул.Комарова, д.41;",
                        Description = "У нас вы найдете экологически чистые продукты по приятным ценам"
                    },
                    //db.Makers.Add(new Maker("АЛМА", "8347-827-36-96", "",
                    //    "ул.Комарова, д.41;", "У нас вы найдете экологически чистые продукты по приятным ценам"));
                    new MakerDto
                    {
                        Id = 3,
                        Title = "Мясницкий ряд",
                        Email = "zakupki@kolbasa.ru",
                        NumberPhone = "+7495-411-33-41",
                        Addres = "Транспортный проезд д.7 г. Одинцово Московская область",
                        Description = "Компания «Мясницкий ряд» основана в 2004 году на базе Первого Одинцовского мясокомбината. " +
                                      "Наша компания активно растёт и развивается, " +
                                      "регулярно расширяя ассортимент и повышая качество выпускаемой продукции."
                    },
                    //db.Makers.Add(new Maker("Мясницкий ряд", " +7495-411-33-41", " zakupki@kolbasa.ru",
                    //    "Транспортный проезд д.7 г. Одинцово Московская область", ""));
                    new MakerDto
                    {
                        Id = 4,
                        Title = "Черкизово",
                        Email = "sk@cherkizovo.com",
                        NumberPhone = "+7495-660-24-40",
                        Addres = "Москва 125047 Лесная улица 5Б, бизнес - центр «Белая площадь», 12 - й этаж",
                        Description = "Мясное производство для нас не просто бизнес. " +
                                        "Делать лучшие в стране продукты питания — наша страсть и призвание."

                    });
                    //db.Makers.Add(new Maker("Черкизово", "+7495-660-24-40", " sk@cherkizovo.com",
                    //    "Москва 125047 Лесная улица 5Б, бизнес - центр «Белая площадь», 12 - й этаж", ""));
            });

            
        }

        private static void BuildModelOrders(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<OrderDto>(action =>
            {
                action.Property(dto => dto.CellPhone)
                                .HasMaxLength(20);

                action.Property(dto => dto.Email)
                                .HasMaxLength(50);

                action.Property(dto => dto.DeliveryUniqueCode)
                                .HasMaxLength(30);
                
                action.Property(dto => dto.DeliveryPrice)
                                .HasColumnType("money");

                action.Property(dto => dto.PaymentUniqueCode)
                                .HasMaxLength(30);

                action.Property(dto => dto.PaymentParametrs)
                                .HasConversion(
                                    value => JsonConvert.SerializeObject(value),
                                    value => JsonConvert.DeserializeObject<Dictionary<string, string>>(value))
                                    .Metadata.SetValueComparer(DictionaryComparer);

                action.Property(dto => dto.DeliveryParameters)
                               .HasConversion(
                                   value => JsonConvert.SerializeObject(value),
                                   value => JsonConvert.DeserializeObject<Dictionary<string, string>>(value))
                                   .Metadata.SetValueComparer(DictionaryComparer);
            });
        }

        private static void BuildModelOrderItems(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<OrderItemDto>(action =>
            {
                action.Property(dto => dto.Price)
                            .HasColumnType("money")
                            .IsRequired();

                action.HasOne(dto => dto.Order)
                             .WithMany(dto=> dto.Items)
                             .IsRequired();                             
            });
        }
    }
}
