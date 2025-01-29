using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BikeKinnus.DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class PaymentSummaryModelAddedInDBSetSomeChangesMADE : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "FinalStatus",
                table: "PaymentSummaries",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "FinalStatus",
                table: "PaymentSummaries");
        }
    }
}
