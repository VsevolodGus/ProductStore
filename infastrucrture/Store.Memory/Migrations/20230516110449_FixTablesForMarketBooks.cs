using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Store.Memory.Migrations
{
    /// <inheritdoc />
    public partial class FixTablesForMarketBooks : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Maker",
                schema: "dbo",
                table: "Books");

            migrationBuilder.DropTable(
                name: "Makers",
                schema: "dbo");

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

            migrationBuilder.AddForeignKey(
                name: "FK_PublishingHouses",
                schema: "dbo",
                table: "Books",
                column: "MakerID",
                principalSchema: "dbo",
                principalTable: "PublishingHouses",
                principalColumn: "ID",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PublishingHouses",
                schema: "dbo",
                table: "Books");

            migrationBuilder.DropTable(
                name: "PublishingHouses",
                schema: "dbo");

            migrationBuilder.CreateTable(
                name: "Makers",
                schema: "dbo",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Address = table.Column<string>(type: "nvarchar(1000)", maxLength: 1000, nullable: true),
                    Description = table.Column<string>(type: "nvarchar(1000)", maxLength: 1000, nullable: true),
                    Email = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    NumberPhone = table.Column<string>(type: "nvarchar(15)", maxLength: 15, nullable: true),
                    Title = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Makers", x => x.ID);
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

            migrationBuilder.AddForeignKey(
                name: "FK_Maker",
                schema: "dbo",
                table: "Books",
                column: "MakerID",
                principalSchema: "dbo",
                principalTable: "Makers",
                principalColumn: "ID",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
