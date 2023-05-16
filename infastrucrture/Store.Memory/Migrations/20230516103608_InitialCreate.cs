using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Store.Memory.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "dbo");

            migrationBuilder.CreateTable(
                name: "Makers",
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
                    table.PrimaryKey("PK_Makers", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "Orders",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CellPhone = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: true),
                    Email = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    DeliveryUniqueCode = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: true),
                    DeliveryDescription = table.Column<string>(type: "nvarchar(1000)", maxLength: 1000, nullable: true),
                    DeliveryPrice = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
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
                name: "Products",
                schema: "dbo",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Title = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    MakerID = table.Column<int>(type: "int", nullable: false),
                    Category = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Price = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(1000)", maxLength: 1000, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Products", x => x.ID);
                    table.ForeignKey(
                        name: "FK_Maker",
                        column: x => x.MakerID,
                        principalSchema: "dbo",
                        principalTable: "Makers",
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
                    Price = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Count = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OrderItems", x => x.ID);
                    table.ForeignKey(
                        name: "FK_Order",
                        column: x => x.OrderID,
                        principalTable: "Orders",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Product",
                        column: x => x.ProductID,
                        principalSchema: "dbo",
                        principalTable: "Products",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                schema: "dbo",
                table: "Makers",
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
                table: "Products",
                columns: new[] { "ID", "Category", "Description", "MakerID", "Price", "Title" },
                values: new object[,]
                {
                    { 1, "яйца", "куринные яйца, категории C0", 1, 30m, "яйца" },
                    { 2, "выпечка", "хлебо-булочные изделия", 2, 20m, "хлеб" },
                    { 3, "мясо", "мясо из говядины и телятины", 3, 30m, "говядина" },
                    { 4, "мясо", "мясо из свинины", 4, 40m, "свинина" }
                });

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

            migrationBuilder.CreateIndex(
                name: "IX_Products_MakerID",
                schema: "dbo",
                table: "Products",
                column: "MakerID");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "OrderItems",
                schema: "dbo");

            migrationBuilder.DropTable(
                name: "Orders");

            migrationBuilder.DropTable(
                name: "Products",
                schema: "dbo");

            migrationBuilder.DropTable(
                name: "Makers",
                schema: "dbo");
        }
    }
}
