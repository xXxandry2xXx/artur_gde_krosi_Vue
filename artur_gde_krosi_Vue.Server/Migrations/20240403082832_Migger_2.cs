using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace artur_gde_krosi_Vue.Server.Migrations
{
    /// <inheritdoc />
    public partial class Migger_2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "views",
                table: "Products",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "views",
                table: "Products");
        }
    }
}
