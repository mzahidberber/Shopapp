using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace shopapp.dataaccess.Migrations
{
    /// <inheritdoc />
    public partial class init1 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Brands_Categories_CategoryId",
                table: "Brands");

            migrationBuilder.DropTable(
                name: "CategoryFeatureValues");

            migrationBuilder.DropTable(
                name: "CategoryFeatures");

            migrationBuilder.RenameColumn(
                name: "CategoryId",
                table: "Brands",
                newName: "SubCategoryId");

            migrationBuilder.RenameIndex(
                name: "IX_Brands_CategoryId",
                table: "Brands",
                newName: "IX_Brands_SubCategoryId");

            migrationBuilder.AddColumn<int>(
                name: "SubCategoryId",
                table: "Products",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "SubCategories",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Name = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Url = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    CategoryId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SubCategories", x => x.Id);
                    table.ForeignKey(
                        name: "FK_SubCategories_Categories_CategoryId",
                        column: x => x.CategoryId,
                        principalTable: "Categories",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "SubCategoryFeatures",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Name = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    SubCategoryId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SubCategoryFeatures", x => x.Id);
                    table.ForeignKey(
                        name: "FK_SubCategoryFeatures_SubCategories_SubCategoryId",
                        column: x => x.SubCategoryId,
                        principalTable: "SubCategories",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "SubCategoryFeatureValues",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Value = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    SubCategoryFeatureId = table.Column<int>(type: "int", nullable: false),
                    ProductId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SubCategoryFeatureValues", x => x.Id);
                    table.ForeignKey(
                        name: "FK_SubCategoryFeatureValues_Products_ProductId",
                        column: x => x.ProductId,
                        principalTable: "Products",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_SubCategoryFeatureValues_SubCategoryFeatures_SubCategoryFeat~",
                        column: x => x.SubCategoryFeatureId,
                        principalTable: "SubCategoryFeatures",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "8e445865-a24d-4543-a6c6-9443d048cdb9",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "b3405efb-8211-4780-ab08-130e102a422b", "AQAAAAIAAYagAAAAEJGzK+T/8FCEu1ylPUzc2e+HFXOwe1ZLkBZJgqCatxmbHagZJUa2rA8cmYmD4SJ8SQ==", "d1e853e2-80d6-498c-9b8d-a0680180049b" });

            migrationBuilder.UpdateData(
                table: "Brands",
                keyColumn: "Id",
                keyValue: 5,
                columns: new[] { "Name", "Url" },
                values: new object[] { "Dell", "dell" });

            migrationBuilder.UpdateData(
                table: "Brands",
                keyColumn: "Id",
                keyValue: 6,
                columns: new[] { "Name", "Url" },
                values: new object[] { "Asus", "asus" });

            migrationBuilder.UpdateData(
                table: "Brands",
                keyColumn: "Id",
                keyValue: 7,
                columns: new[] { "Name", "Url" },
                values: new object[] { "Dell", "dell" });

            migrationBuilder.UpdateData(
                table: "Brands",
                keyColumn: "Id",
                keyValue: 8,
                columns: new[] { "Name", "Url" },
                values: new object[] { "Asus", "asus" });

            migrationBuilder.UpdateData(
                table: "Brands",
                keyColumn: "Id",
                keyValue: 9,
                columns: new[] { "Name", "Url" },
                values: new object[] { "Samsung", "samsung" });

            migrationBuilder.UpdateData(
                table: "Brands",
                keyColumn: "Id",
                keyValue: 10,
                columns: new[] { "Name", "Url" },
                values: new object[] { "Apple", "apple" });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 1,
                column: "SubCategoryId",
                value: 1);

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 2,
                column: "SubCategoryId",
                value: 5);

            migrationBuilder.InsertData(
                table: "SubCategories",
                columns: new[] { "Id", "CategoryId", "Name", "Url" },
                values: new object[,]
                {
                    { 1, 1, "Laptop", "laptop" },
                    { 2, 1, "Oyun Bilgisayarı", "oyunpc" },
                    { 3, 2, "Masaüstü", "masaustu" },
                    { 4, 2, "Oyun Bilgisayarı", "oyunpc" },
                    { 5, 3, "Akıllı Telefon", "akillitelefon" },
                    { 6, 4, "Tablet", "tablet" },
                    { 7, 5, "Full HD", "fullhd" }
                });

            migrationBuilder.InsertData(
                table: "Brands",
                columns: new[] { "Id", "Name", "SubCategoryId", "Url" },
                values: new object[,]
                {
                    { 11, "Apple", 6, "apple" },
                    { 12, "Lenova", 6, "lenova" },
                    { 13, "Vestel", 7, "vestel" },
                    { 14, "Samsung", 7, "samsung" }
                });

            migrationBuilder.InsertData(
                table: "SubCategoryFeatures",
                columns: new[] { "Id", "Name", "SubCategoryId" },
                values: new object[,]
                {
                    { 1, "Ekran Kartı", 1 },
                    { 2, "İşlemci", 1 },
                    { 3, "İşlemci", 2 },
                    { 4, "Ekran Kartı", 2 },
                    { 5, "İşlemci", 3 },
                    { 6, "Ekran Kartı", 3 },
                    { 7, "İşlemci", 4 },
                    { 8, "Ekran Kartı", 4 },
                    { 9, "Kamera", 5 },
                    { 10, "Hafıza", 5 },
                    { 11, "İşlemci", 6 },
                    { 12, "Ekran Kartı", 6 },
                    { 13, "Ekran Boyutu", 7 },
                    { 14, "Çözünürlük", 7 }
                });

            migrationBuilder.InsertData(
                table: "SubCategoryFeatureValues",
                columns: new[] { "Id", "ProductId", "SubCategoryFeatureId", "Value" },
                values: new object[,]
                {
                    { 1, 1, 1, "GTX 3060" },
                    { 2, 1, 2, "Intel i7" },
                    { 3, 2, 9, "Intel i7" },
                    { 4, 2, 10, "GT 360" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Products_SubCategoryId",
                table: "Products",
                column: "SubCategoryId");

            migrationBuilder.CreateIndex(
                name: "IX_SubCategories_CategoryId",
                table: "SubCategories",
                column: "CategoryId");

            migrationBuilder.CreateIndex(
                name: "IX_SubCategoryFeatures_SubCategoryId",
                table: "SubCategoryFeatures",
                column: "SubCategoryId");

            migrationBuilder.CreateIndex(
                name: "IX_SubCategoryFeatureValues_ProductId",
                table: "SubCategoryFeatureValues",
                column: "ProductId");

            migrationBuilder.CreateIndex(
                name: "IX_SubCategoryFeatureValues_SubCategoryFeatureId",
                table: "SubCategoryFeatureValues",
                column: "SubCategoryFeatureId");

            migrationBuilder.AddForeignKey(
                name: "FK_Brands_SubCategories_SubCategoryId",
                table: "Brands",
                column: "SubCategoryId",
                principalTable: "SubCategories",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Products_SubCategories_SubCategoryId",
                table: "Products",
                column: "SubCategoryId",
                principalTable: "SubCategories",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Brands_SubCategories_SubCategoryId",
                table: "Brands");

            migrationBuilder.DropForeignKey(
                name: "FK_Products_SubCategories_SubCategoryId",
                table: "Products");

            migrationBuilder.DropTable(
                name: "SubCategoryFeatureValues");

            migrationBuilder.DropTable(
                name: "SubCategoryFeatures");

            migrationBuilder.DropTable(
                name: "SubCategories");

            migrationBuilder.DropIndex(
                name: "IX_Products_SubCategoryId",
                table: "Products");

            migrationBuilder.DeleteData(
                table: "Brands",
                keyColumn: "Id",
                keyValue: 11);

            migrationBuilder.DeleteData(
                table: "Brands",
                keyColumn: "Id",
                keyValue: 12);

            migrationBuilder.DeleteData(
                table: "Brands",
                keyColumn: "Id",
                keyValue: 13);

            migrationBuilder.DeleteData(
                table: "Brands",
                keyColumn: "Id",
                keyValue: 14);

            migrationBuilder.DropColumn(
                name: "SubCategoryId",
                table: "Products");

            migrationBuilder.RenameColumn(
                name: "SubCategoryId",
                table: "Brands",
                newName: "CategoryId");

            migrationBuilder.RenameIndex(
                name: "IX_Brands_SubCategoryId",
                table: "Brands",
                newName: "IX_Brands_CategoryId");

            migrationBuilder.CreateTable(
                name: "CategoryFeatures",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    CategoryId = table.Column<int>(type: "int", nullable: false),
                    Name = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CategoryFeatures", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CategoryFeatures_Categories_CategoryId",
                        column: x => x.CategoryId,
                        principalTable: "Categories",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "CategoryFeatureValues",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    CategoryFeatureId = table.Column<int>(type: "int", nullable: false),
                    ProductId = table.Column<int>(type: "int", nullable: false),
                    Value = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CategoryFeatureValues", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CategoryFeatureValues_CategoryFeatures_CategoryFeatureId",
                        column: x => x.CategoryFeatureId,
                        principalTable: "CategoryFeatures",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_CategoryFeatureValues_Products_ProductId",
                        column: x => x.ProductId,
                        principalTable: "Products",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "8e445865-a24d-4543-a6c6-9443d048cdb9",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "ee7ade70-ff0c-4803-af56-9459abdf5781", "AQAAAAIAAYagAAAAEFklyAbz3i4+xn5a/WbG42MUV//9G40lXVq7tia9+bau3aeLkt3H0MVtcS+Fb1JxQg==", "01e2d067-6094-43c8-8fef-ef317b87053a" });

            migrationBuilder.UpdateData(
                table: "Brands",
                keyColumn: "Id",
                keyValue: 5,
                columns: new[] { "Name", "Url" },
                values: new object[] { "Samsung", "samsung" });

            migrationBuilder.UpdateData(
                table: "Brands",
                keyColumn: "Id",
                keyValue: 6,
                columns: new[] { "Name", "Url" },
                values: new object[] { "Apple", "apple" });

            migrationBuilder.UpdateData(
                table: "Brands",
                keyColumn: "Id",
                keyValue: 7,
                columns: new[] { "Name", "Url" },
                values: new object[] { "Apple", "apple" });

            migrationBuilder.UpdateData(
                table: "Brands",
                keyColumn: "Id",
                keyValue: 8,
                columns: new[] { "Name", "Url" },
                values: new object[] { "Lenova", "lenova" });

            migrationBuilder.UpdateData(
                table: "Brands",
                keyColumn: "Id",
                keyValue: 9,
                columns: new[] { "Name", "Url" },
                values: new object[] { "Vestel", "vestel" });

            migrationBuilder.UpdateData(
                table: "Brands",
                keyColumn: "Id",
                keyValue: 10,
                columns: new[] { "Name", "Url" },
                values: new object[] { "Samsung", "samsung" });

            migrationBuilder.InsertData(
                table: "CategoryFeatures",
                columns: new[] { "Id", "CategoryId", "Name" },
                values: new object[,]
                {
                    { 1, 1, "Ekran Kartı" },
                    { 2, 1, "İşlemci" },
                    { 3, 2, "İşlemci" },
                    { 4, 2, "Ekran Kartı" },
                    { 5, 3, "Kamera" },
                    { 6, 3, "Hafıza" },
                    { 7, 4, "İşlemci" },
                    { 8, 4, "Ekran Kartı" },
                    { 9, 5, "Ekran Boyutu" },
                    { 10, 5, "Çözünürlük" }
                });

            migrationBuilder.InsertData(
                table: "CategoryFeatureValues",
                columns: new[] { "Id", "CategoryFeatureId", "ProductId", "Value" },
                values: new object[,]
                {
                    { 1, 1, 1, "GTX 3060" },
                    { 2, 2, 1, "Intel i7" },
                    { 3, 7, 2, "Intel i7" },
                    { 4, 8, 2, "GT 360" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_CategoryFeatures_CategoryId",
                table: "CategoryFeatures",
                column: "CategoryId");

            migrationBuilder.CreateIndex(
                name: "IX_CategoryFeatureValues_CategoryFeatureId",
                table: "CategoryFeatureValues",
                column: "CategoryFeatureId");

            migrationBuilder.CreateIndex(
                name: "IX_CategoryFeatureValues_ProductId",
                table: "CategoryFeatureValues",
                column: "ProductId");

            migrationBuilder.AddForeignKey(
                name: "FK_Brands_Categories_CategoryId",
                table: "Brands",
                column: "CategoryId",
                principalTable: "Categories",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
