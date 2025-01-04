using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BikeKinnus.DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class SomeModelsUpdated : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "StreetAddress",
                table: "OrderHeaders",
                newName: "Email");

            migrationBuilder.AddColumn<int>(
                name: "Age",
                table: "OrderHeaders",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Age",
                table: "OrderHeaders");

            migrationBuilder.RenameColumn(
                name: "Email",
                table: "OrderHeaders",
                newName: "StreetAddress");
        }
    }
}
