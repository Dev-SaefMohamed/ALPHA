using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Hero.Data.Migrations
{
    /// <inheritdoc />
    public partial class UpdateSubscriptionModel : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "ApiCallsUsedThisMonth",
                table: "Subscriptions",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "ApiKey",
                table: "Subscriptions",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<int>(
                name: "MonthlyApiLimit",
                table: "Subscriptions",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "PlanName",
                table: "Subscriptions",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<decimal>(
                name: "PricePerMonth",
                table: "Subscriptions",
                type: "decimal(18,2)",
                nullable: false,
                defaultValue: 0m);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ApiCallsUsedThisMonth",
                table: "Subscriptions");

            migrationBuilder.DropColumn(
                name: "ApiKey",
                table: "Subscriptions");

            migrationBuilder.DropColumn(
                name: "MonthlyApiLimit",
                table: "Subscriptions");

            migrationBuilder.DropColumn(
                name: "PlanName",
                table: "Subscriptions");

            migrationBuilder.DropColumn(
                name: "PricePerMonth",
                table: "Subscriptions");
        }
    }
}
