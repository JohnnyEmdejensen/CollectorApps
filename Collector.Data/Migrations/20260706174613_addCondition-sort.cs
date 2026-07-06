using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Collector.Data.Migrations
{
    /// <inheritdoc />
    public partial class addConditionsort : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Sort",
                table: "Conditions",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Sort",
                table: "Conditions");
        }
    }
}
