using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ToDoListNTier.DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class AddedDescriptionToItem : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Description",
                table: "TodoItems",
                type: "character varying(1000)",
                maxLength: 1000,
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Description",
                table: "TodoItems");
        }
    }
}
