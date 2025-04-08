using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BeelineMicroService.Migrations
{
    /// <inheritdoc />
    public partial class AddIsProcessedToEventEntity : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "IsProcessed",
                table: "Events",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsProcessed",
                table: "Events");
        }
    }
}
