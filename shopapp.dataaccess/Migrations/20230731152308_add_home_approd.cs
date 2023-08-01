using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace shopapp.dataaccess.Migrations
{
    /// <inheritdoc />
    public partial class add_home_approd : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "IsApprove",
                table: "Products",
                type: "tinyint(1)",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsHome",
                table: "Products",
                type: "tinyint(1)",
                nullable: false,
                defaultValue: false);

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "8e445865-a24d-4543-a6c6-9443d048cdb9",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "890a37ea-6c1a-4679-8723-0d4ab655efed", "AQAAAAIAAYagAAAAEDNHEi+/flxvEIp47iefywaJ2aOfLefS+8pDaHOpJ+WeyPVXw4Tz5NpLqhzS+SO2vw==", "d68a53fd-a4dd-4977-a440-ea8286cf055a" });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "IsApprove", "IsHome" },
                values: new object[] { false, false });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "IsApprove", "IsHome" },
                values: new object[] { false, false });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsApprove",
                table: "Products");

            migrationBuilder.DropColumn(
                name: "IsHome",
                table: "Products");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "8e445865-a24d-4543-a6c6-9443d048cdb9",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "9adc5fa1-0877-483b-b72e-88136f662cae", "AQAAAAIAAYagAAAAEJ5cLLUXikRFzlVzgAST/TbjbAn/8b0WBxeH7c0R4/+HOwzp4lW95r62SciD987CTg==", "8ebc60fa-a7af-4364-b257-acc81e73f96d" });
        }
    }
}
