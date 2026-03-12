using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Collector.Data.Migrations
{
    /// <inheritdoc />
    public partial class chkPublisedCountriesname : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_PublishedCountriesLinks",
                table: "PublishedCountriesLinks");

            migrationBuilder.RenameTable(
                name: "PublishedCountriesLinks",
                newName: "PublishedCountryCoinsLinks");

            migrationBuilder.AddPrimaryKey(
                name: "PK_PublishedCountryCoinsLinks",
                table: "PublishedCountryCoinsLinks",
                column: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_PublishedCountryCoinsLinks",
                table: "PublishedCountryCoinsLinks");

            migrationBuilder.RenameTable(
                name: "PublishedCountryCoinsLinks",
                newName: "PublishedCountriesLinks");

            migrationBuilder.AddPrimaryKey(
                name: "PK_PublishedCountriesLinks",
                table: "PublishedCountriesLinks",
                column: "Id");
        }
    }
}
