using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Book4H2Ten.EntityFrameWorkCore.Migrations
{
    /// <inheritdoc />
    public partial class addRoleName : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "RoleName",
                table: "Users",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "RoleName",
                table: "Users");
        }
    }
}
