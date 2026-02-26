using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Collector.Data.Migrations
{
    /// <inheritdoc />
    public partial class addCategoryUKValue : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "UKValue",
                table: "Categories",
                type: "nvarchar(128)",
                maxLength: 128,
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "UKValue",
                table: "Categories");
        }
    }
}
