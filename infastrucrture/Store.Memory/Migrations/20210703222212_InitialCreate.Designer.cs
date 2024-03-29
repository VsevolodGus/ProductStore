﻿// <auto-generated />
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Store.Memory;

namespace Store.Memory.Migrations
{
    [DbContext(typeof(StoreDbContext))]
    [Migration("20210703222212_InitialCreate")]
    partial class InitialCreate
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("ProductVersion", "5.0.7")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("Store.Data.MakerDto", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Addres")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Description")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Email")
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.Property<string>("NumberPhone")
                        .HasMaxLength(15)
                        .HasColumnType("nvarchar(15)");

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.HasKey("Id");

                    b.ToTable("Makers");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            Addres = "ООО 'Красная Цена' Самара",
                            Description = "Название Красная Цена выбрано не случайно!",
                            Email = "redPrice@gmail.com",
                            NumberPhone = "8937-216-76-11",
                            Title = "Красная цена "
                        },
                        new
                        {
                            Id = 2,
                            Addres = "ул.Комарова, д.41;",
                            Description = "У нас вы найдете экологически чистые продукты по приятным ценам",
                            Email = "alma@mail.ru",
                            NumberPhone = "8347-827-36-96",
                            Title = "АЛМА"
                        },
                        new
                        {
                            Id = 3,
                            Addres = "Транспортный проезд д.7 г. Одинцово Московская область",
                            Description = "Компания «Мясницкий ряд» основана в 2004 году на базе Первого Одинцовского мясокомбината. Наша компания активно растёт и развивается, регулярно расширяя ассортимент и повышая качество выпускаемой продукции.",
                            Email = "zakupki@kolbasa.ru",
                            NumberPhone = "+7495-411-33-41",
                            Title = "Мясницкий ряд"
                        },
                        new
                        {
                            Id = 4,
                            Addres = "Москва 125047 Лесная улица 5Б, бизнес - центр «Белая площадь», 12 - й этаж",
                            Description = "Мясное производство для нас не просто бизнес. Делать лучшие в стране продукты питания — наша страсть и призвание.",
                            Email = "sk@cherkizovo.com",
                            NumberPhone = "+7495-660-24-40",
                            Title = "Черкизово"
                        });
                });

            modelBuilder.Entity("Store.Data.OrderDto", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("CellPhone")
                        .HasMaxLength(20)
                        .HasColumnType("nvarchar(20)");

                    b.Property<string>("DeliveryDescription")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("DeliveryParameters")
                        .HasColumnType("nvarchar(max)");

                    b.Property<decimal>("DeliveryPrice")
                        .HasColumnType("money");

                    b.Property<string>("DeliveryUniqueCode")
                        .HasMaxLength(30)
                        .HasColumnType("nvarchar(30)");

                    b.Property<string>("Email")
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<string>("PaymentDescription")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("PaymentParametrs")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("PaymentUniqueCode")
                        .HasMaxLength(30)
                        .HasColumnType("nvarchar(30)");

                    b.HasKey("Id");

                    b.ToTable("Orders");
                });

            modelBuilder.Entity("Store.Data.OrderItemDto", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("Count")
                        .HasColumnType("int");

                    b.Property<int>("OrderId")
                        .HasColumnType("int");

                    b.Property<decimal>("Price")
                        .HasColumnType("money");

                    b.Property<int>("ProductId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("OrderId");

                    b.ToTable("OrderItems");
                });

            modelBuilder.Entity("Store.Data.ProductDto", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Category")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<string>("Description")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("IdMaker")
                        .HasColumnType("int");

                    b.Property<decimal>("Price")
                        .HasColumnType("money");

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.HasKey("Id");

                    b.ToTable("Products");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            Category = "яйца",
                            Description = "куринные яйца, категории C0",
                            IdMaker = 1,
                            Price = 30m,
                            Title = "яйца"
                        },
                        new
                        {
                            Id = 2,
                            Category = "выпечка",
                            Description = "хлебо-булочные изделия",
                            IdMaker = 2,
                            Price = 20m,
                            Title = "хлеб"
                        },
                        new
                        {
                            Id = 3,
                            Category = "мясо",
                            Description = "мясо из говядины и телятины",
                            IdMaker = 3,
                            Price = 30m,
                            Title = "говядина"
                        },
                        new
                        {
                            Id = 4,
                            Category = "мясо",
                            Description = "мясо из свинины",
                            IdMaker = 4,
                            Price = 40m,
                            Title = "свинина"
                        });
                });

            modelBuilder.Entity("Store.Data.OrderItemDto", b =>
                {
                    b.HasOne("Store.Data.OrderDto", "Order")
                        .WithMany("Items")
                        .HasForeignKey("OrderId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Order");
                });

            modelBuilder.Entity("Store.Data.OrderDto", b =>
                {
                    b.Navigation("Items");
                });
#pragma warning restore 612, 618
        }
    }
}
