using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace upskilling.Repository.Migrations
{
    /// <inheritdoc />
    public partial class fortas : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "TaskId",
                table: "TeamMembers",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "TaskId",
                table: "TeamMembers");
        }
    }
}
