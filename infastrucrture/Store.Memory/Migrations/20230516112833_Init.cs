using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Store.Memory.Migrations
{
    /// <inheritdoc />
    public partial class Init : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "dbo");

            migrationBuilder.CreateTable(
                name: "Authors",
                schema: "dbo",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FirstName = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    SecondName = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    LastName = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Description = table.Column<string>(type: "nvarchar(1000)", maxLength: 1000, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Authors", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "Orders",
                schema: "dbo",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CellPhone = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: true),
                    Email = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    DeliveryUniqueCode = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: true),
                    DeliveryDescription = table.Column<string>(type: "nvarchar(1000)", maxLength: 1000, nullable: true),
                    DeliveryPrice = table.Column<decimal>(type: "money", nullable: false),
                    DeliveryParameters = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PaymentUniqueCode = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: true),
                    PaymentDescription = table.Column<string>(type: "nvarchar(1000)", maxLength: 1000, nullable: true),
                    PaymentParameters = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Orders", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "PublishingHouses",
                schema: "dbo",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Title = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    NumberPhone = table.Column<string>(type: "nvarchar(15)", maxLength: 15, nullable: true),
                    Email = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    Address = table.Column<string>(type: "nvarchar(1000)", maxLength: 1000, nullable: true),
                    Description = table.Column<string>(type: "nvarchar(1000)", maxLength: 1000, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PublishingHouses", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "Books",
                schema: "dbo",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Title = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    PublishHousingID = table.Column<int>(type: "int", nullable: false),
                    ISBN = table.Column<string>(type: "nvarchar(35)", maxLength: 35, nullable: true),
                    Category = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    AuthorID = table.Column<int>(type: "int", nullable: false),
                    Price = table.Column<decimal>(type: "money", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(1000)", maxLength: 1000, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Products", x => x.ID);
                    table.ForeignKey(
                        name: "FK_Authors",
                        column: x => x.AuthorID,
                        principalSchema: "dbo",
                        principalTable: "Authors",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_PublishingHouses",
                        column: x => x.PublishHousingID,
                        principalSchema: "dbo",
                        principalTable: "PublishingHouses",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "OrderItems",
                schema: "dbo",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ProductID = table.Column<int>(type: "int", nullable: false),
                    OrderID = table.Column<int>(type: "int", nullable: false),
                    Price = table.Column<decimal>(type: "money", nullable: false),
                    Count = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OrderItems", x => x.ID);
                    table.ForeignKey(
                        name: "FK_Order",
                        column: x => x.OrderID,
                        principalSchema: "dbo",
                        principalTable: "Orders",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Product",
                        column: x => x.ProductID,
                        principalSchema: "dbo",
                        principalTable: "Books",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                schema: "dbo",
                table: "Authors",
                columns: new[] { "ID", "Description", "FirstName", "LastName", "SecondName" },
                values: new object[] { 1, "куринные яйца, категории C0", "Test", "Test", "Test" });

            migrationBuilder.InsertData(
                schema: "dbo",
                table: "PublishingHouses",
                columns: new[] { "ID", "Address", "Description", "Email", "NumberPhone", "Title" },
                values: new object[,]
                {
                    { 1, "ООО 'Красная Цена' Самара", "Название Красная Цена выбрано не случайно!", "redPrice@gmail.com", "8937-216-76-11", "Красная цена " },
                    { 2, "ул.Комарова, д.41;", "У нас вы найдете экологически чистые продукты по приятным ценам", "alma@mail.ru", "8347-827-36-96", "АЛМА" },
                    { 3, "Транспортный проезд д.7 г. Одинцово Московская область", "Компания «Мясницкий ряд» основана в 2004 году на базе Первого Одинцовского мясокомбината. Наша компания активно растёт и развивается, регулярно расширяя ассортимент и повышая качество выпускаемой продукции.", "zakupki@kolbasa.ru", "+7495-411-33-41", "Мясницкий ряд" },
                    { 4, "Москва 125047 Лесная улица 5Б, бизнес - центр «Белая площадь», 12 - й этаж", "Мясное производство для нас не просто бизнес. Делать лучшие в стране продукты питания — наша страсть и призвание.", "sk@cherkizovo.com", "+7495-660-24-40", "Черкизово" }
                });

            migrationBuilder.InsertData(
                schema: "dbo",
                table: "Books",
                columns: new[] { "ID", "AuthorID", "Category", "Description", "ISBN", "Price", "PublishHousingID", "Title" },
                values: new object[,]
                {
                    { 1, 1, "яйца", "куринные яйца, категории C0", null, 30m, 1, "яйца" },
                    { 2, 1, "выпечка", "хлебо-булочные изделия", null, 20m, 2, "хлеб" },
                    { 3, 1, "мясо", "мясо из говядины и телятины", null, 30m, 3, "говядина" },
                    { 4, 1, "мясо", "мясо из свинины", null, 40m, 4, "свинина" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Books_AuthorID",
                schema: "dbo",
                table: "Books",
                column: "AuthorID");

            migrationBuilder.CreateIndex(
                name: "IX_Books_PublishHousingID",
                schema: "dbo",
                table: "Books",
                column: "PublishHousingID");

            migrationBuilder.CreateIndex(
                name: "UI_ISBN_Books",
                schema: "dbo",
                table: "Books",
                column: "ISBN",
                unique: true,
                filter: "[ISBN] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_OrderItems_OrderID",
                schema: "dbo",
                table: "OrderItems",
                column: "OrderID");

            migrationBuilder.CreateIndex(
                name: "IX_OrderItems_ProductID",
                schema: "dbo",
                table: "OrderItems",
                column: "ProductID");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "OrderItems",
                schema: "dbo");

            migrationBuilder.DropTable(
                name: "Orders",
                schema: "dbo");

            migrationBuilder.DropTable(
                name: "Books",
                schema: "dbo");

            migrationBuilder.DropTable(
                name: "Authors",
                schema: "dbo");

            migrationBuilder.DropTable(
                name: "PublishingHouses",
                schema: "dbo");
        }
    }
}
