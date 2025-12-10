using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SimpleGraphqlCrud.Migrations
{
    /// <inheritdoc />
    public partial class InitialMigrateremoveloginflag : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "LoginFlag",
                table: "Painters");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "LoginFlag",
                table: "Painters",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }
    }
}
