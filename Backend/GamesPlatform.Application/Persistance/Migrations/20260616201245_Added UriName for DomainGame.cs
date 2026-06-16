using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GamesPlatform.Application.Persistance.Migrations
{
    /// <inheritdoc />
    public partial class AddedUriNameforDomainGame : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "UriName",
                table: "Games",
                type: "text",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "UriName",
                table: "Games");
        }
    }
}
