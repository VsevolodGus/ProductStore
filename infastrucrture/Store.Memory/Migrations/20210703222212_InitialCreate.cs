using Microsoft.EntityFrameworkCore.Migrations;

namespace Store.Memory.Migrations
{
    public partial class InitialCreate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Makers",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Title = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    NumberPhone = table.Column<string>(type: "nvarchar(15)", maxLength: 15, nullable: true),
                    Email = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    Addres = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Makers", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Orders",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CellPhone = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: true),
                    Email = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    DeliveryUniqueCode = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: true),
                    DeliveryDescription = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DeliveryPrice = table.Column<decimal>(type: "money", nullable: false),
                    DeliveryParameters = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PaymentUniqueCode = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: true),
                    PaymentDescription = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PaymentParametrs = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Orders", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Products",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Title = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    IdMaker = table.Column<int>(type: "int", nullable: false),
                    Category = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Price = table.Column<decimal>(type: "money", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Products", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "OrderItems",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ProductId = table.Column<int>(type: "int", nullable: false),
                    Price = table.Column<decimal>(type: "money", nullable: false),
                    Count = table.Column<int>(type: "int", nullable: false),
                    OrderId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OrderItems", x => x.Id);
                    table.ForeignKey(
                        name: "FK_OrderItems_Orders_OrderId",
                        column: x => x.OrderId,
                        principalTable: "Orders",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Makers",
                columns: new[] { "Id", "Addres", "Description", "Email", "NumberPhone", "Title" },
                values: new object[,]
                {
                    { 1, "ООО 'Красная Цена' Самара", "Название Красная Цена выбрано не случайно!", "redPrice@gmail.com", "8937-216-76-11", "Красная цена " },
                    { 2, "ул.Комарова, д.41;", "У нас вы найдете экологически чистые продукты по приятным ценам", "alma@mail.ru", "8347-827-36-96", "АЛМА" },
                    { 3, "Транспортный проезд д.7 г. Одинцово Московская область", "Компания «Мясницкий ряд» основана в 2004 году на базе Первого Одинцовского мясокомбината. Наша компания активно растёт и развивается, регулярно расширяя ассортимент и повышая качество выпускаемой продукции.", "zakupki@kolbasa.ru", "+7495-411-33-41", "Мясницкий ряд" },
                    { 4, "Москва 125047 Лесная улица 5Б, бизнес - центр «Белая площадь», 12 - й этаж", "Мясное производство для нас не просто бизнес. Делать лучшие в стране продукты питания — наша страсть и призвание.", "sk@cherkizovo.com", "+7495-660-24-40", "Черкизово" }
                });

            migrationBuilder.InsertData(
                table: "Products",
                columns: new[] { "Id", "Category", "Description", "IdMaker", "Price", "Title" },
                values: new object[,]
                {
                    { 1, "яйца", "куринные яйца, категории C0", 1, 30m, "яйца" },
                    { 2, "выпечка", "хлебо-булочные изделия", 2, 20m, "хлеб" },
                    { 3, "мясо", "мясо из говядины и телятины", 3, 30m, "говядина" },
                    { 4, "мясо", "мясо из свинины", 4, 40m, "свинина" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_OrderItems_OrderId",
                table: "OrderItems",
                column: "OrderId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Makers");

            migrationBuilder.DropTable(
                name: "OrderItems");

            migrationBuilder.DropTable(
                name: "Products");

            migrationBuilder.DropTable(
                name: "Orders");
        }
    }
}
