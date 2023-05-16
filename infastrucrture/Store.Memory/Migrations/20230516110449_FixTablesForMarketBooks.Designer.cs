﻿// <auto-generated />
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Store.Memory;

#nullable disable

namespace Store.Memory.Migrations
{
    [DbContext(typeof(StoreDbContext))]
    [Migration("20230516110449_FixTablesForMarketBooks")]
    partial class FixTablesForMarketBooks
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.5")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("Store.Data.OrderEntity", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasColumnName("ID");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("ID"));

                    b.Property<string>("CellPhone")
                        .HasMaxLength(20)
                        .HasColumnType("nvarchar(20)")
                        .HasColumnName("CellPhone");

                    b.Property<string>("DeliveryDescription")
                        .HasMaxLength(1000)
                        .HasColumnType("nvarchar(1000)")
                        .HasColumnName("DeliveryDescription");

                    b.Property<string>("DeliveryParameters")
                        .HasColumnType("nvarchar(max)")
                        .HasColumnName("DeliveryParameters");

                    b.Property<decimal>("DeliveryPrice")
                        .HasColumnType("money")
                        .HasColumnName("DeliveryPrice");

                    b.Property<string>("DeliveryUniqueCode")
                        .HasMaxLength(30)
                        .HasColumnType("nvarchar(30)")
                        .HasColumnName("DeliveryUniqueCode");

                    b.Property<string>("Email")
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)")
                        .HasColumnName("Email");

                    b.Property<string>("PaymentDescription")
                        .HasMaxLength(1000)
                        .HasColumnType("nvarchar(1000)")
                        .HasColumnName("PaymentDescription");

                    b.Property<string>("PaymentParameters")
                        .HasColumnType("nvarchar(max)")
                        .HasColumnName("PaymentParameters");

                    b.Property<string>("PaymentUniqueCode")
                        .HasMaxLength(30)
                        .HasColumnType("nvarchar(30)")
                        .HasColumnName("PaymentUniqueCode");

                    b.HasKey("ID");

                    b.ToTable("Orders", "dbo");
                });

            modelBuilder.Entity("Store.Data.OrderItemEntity", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasColumnName("ID");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("ID"));

                    b.Property<int>("Count")
                        .HasColumnType("int")
                        .HasColumnName("Count");

                    b.Property<int>("OrderID")
                        .HasColumnType("int")
                        .HasColumnName("OrderID");

                    b.Property<decimal>("Price")
                        .HasColumnType("money")
                        .HasColumnName("Price");

                    b.Property<int>("ProductID")
                        .HasColumnType("int")
                        .HasColumnName("ProductID");

                    b.HasKey("ID")
                        .HasName("PK_OrderItems");

                    b.HasIndex("OrderID");

                    b.HasIndex("ProductID");

                    b.ToTable("OrderItems", "dbo");
                });

            modelBuilder.Entity("Store.Data.ProductEntity", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasColumnName("ID");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("ID"));

                    b.Property<string>("Category")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)")
                        .HasColumnName("Category");

                    b.Property<string>("Description")
                        .HasMaxLength(1000)
                        .HasColumnType("nvarchar(1000)")
                        .HasColumnName("Description");

                    b.Property<string>("ISBN")
                        .HasMaxLength(35)
                        .HasColumnType("nvarchar(35)")
                        .HasColumnName("ISBN");

                    b.Property<int>("MakerID")
                        .HasColumnType("int")
                        .HasColumnName("MakerID");

                    b.Property<decimal>("Price")
                        .HasColumnType("money")
                        .HasColumnName("Price");

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)")
                        .HasColumnName("Title");

                    b.HasKey("ID")
                        .HasName("PK_Products");

                    b.HasIndex("MakerID");

                    b.HasIndex(new[] { "ISBN" }, "UI_ISBN_Books")
                        .IsUnique()
                        .HasFilter("[ISBN] IS NOT NULL");

                    b.ToTable("Books", "dbo");

                    b.HasData(
                        new
                        {
                            ID = 1,
                            Category = "яйца",
                            Description = "куринные яйца, категории C0",
                            MakerID = 1,
                            Price = 30m,
                            Title = "яйца"
                        },
                        new
                        {
                            ID = 2,
                            Category = "выпечка",
                            Description = "хлебо-булочные изделия",
                            MakerID = 2,
                            Price = 20m,
                            Title = "хлеб"
                        },
                        new
                        {
                            ID = 3,
                            Category = "мясо",
                            Description = "мясо из говядины и телятины",
                            MakerID = 3,
                            Price = 30m,
                            Title = "говядина"
                        },
                        new
                        {
                            ID = 4,
                            Category = "мясо",
                            Description = "мясо из свинины",
                            MakerID = 4,
                            Price = 40m,
                            Title = "свинина"
                        });
                });

            modelBuilder.Entity("Store.Data.PublishingHouseEntity", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasColumnName("ID");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("ID"));

                    b.Property<string>("Address")
                        .HasMaxLength(1000)
                        .HasColumnType("nvarchar(1000)")
                        .HasColumnName("Address");

                    b.Property<string>("Description")
                        .HasMaxLength(1000)
                        .HasColumnType("nvarchar(1000)")
                        .HasColumnName("Description");

                    b.Property<string>("Email")
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)")
                        .HasColumnName("Email");

                    b.Property<string>("NumberPhone")
                        .HasMaxLength(15)
                        .HasColumnType("nvarchar(15)")
                        .HasColumnName("NumberPhone");

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)")
                        .HasColumnName("Title");

                    b.HasKey("ID")
                        .HasName("PK_PublishingHouses");

                    b.ToTable("PublishingHouses", "dbo");

                    b.HasData(
                        new
                        {
                            ID = 1,
                            Address = "ООО 'Красная Цена' Самара",
                            Description = "Название Красная Цена выбрано не случайно!",
                            Email = "redPrice@gmail.com",
                            NumberPhone = "8937-216-76-11",
                            Title = "Красная цена "
                        },
                        new
                        {
                            ID = 2,
                            Address = "ул.Комарова, д.41;",
                            Description = "У нас вы найдете экологически чистые продукты по приятным ценам",
                            Email = "alma@mail.ru",
                            NumberPhone = "8347-827-36-96",
                            Title = "АЛМА"
                        },
                        new
                        {
                            ID = 3,
                            Address = "Транспортный проезд д.7 г. Одинцово Московская область",
                            Description = "Компания «Мясницкий ряд» основана в 2004 году на базе Первого Одинцовского мясокомбината. Наша компания активно растёт и развивается, регулярно расширяя ассортимент и повышая качество выпускаемой продукции.",
                            Email = "zakupki@kolbasa.ru",
                            NumberPhone = "+7495-411-33-41",
                            Title = "Мясницкий ряд"
                        },
                        new
                        {
                            ID = 4,
                            Address = "Москва 125047 Лесная улица 5Б, бизнес - центр «Белая площадь», 12 - й этаж",
                            Description = "Мясное производство для нас не просто бизнес. Делать лучшие в стране продукты питания — наша страсть и призвание.",
                            Email = "sk@cherkizovo.com",
                            NumberPhone = "+7495-660-24-40",
                            Title = "Черкизово"
                        });
                });

            modelBuilder.Entity("Store.Data.OrderItemEntity", b =>
                {
                    b.HasOne("Store.Data.OrderEntity", "Order")
                        .WithMany("Items")
                        .HasForeignKey("OrderID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired()
                        .HasConstraintName("FK_Order");

                    b.HasOne("Store.Data.ProductEntity", "Product")
                        .WithMany()
                        .HasForeignKey("ProductID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired()
                        .HasConstraintName("FK_Product");

                    b.Navigation("Order");

                    b.Navigation("Product");
                });

            modelBuilder.Entity("Store.Data.ProductEntity", b =>
                {
                    b.HasOne("Store.Data.PublishingHouseEntity", "Maker")
                        .WithMany()
                        .HasForeignKey("MakerID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired()
                        .HasConstraintName("FK_PublishingHouses");

                    b.Navigation("Maker");
                });

            modelBuilder.Entity("Store.Data.OrderEntity", b =>
                {
                    b.Navigation("Items");
                });
#pragma warning restore 612, 618
        }
    }
}
