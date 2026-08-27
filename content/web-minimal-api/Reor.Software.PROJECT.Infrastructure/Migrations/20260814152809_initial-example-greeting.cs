using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Reor.Software.PROJECT.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class initialexamplegreeting : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "greeting_event",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "TEXT", nullable: false),
                    Greetee = table.Column<string>(type: "TEXT", nullable: false),
                    Greeter = table.Column<string>(type: "TEXT", nullable: false),
                    CreatedAt = table.Column<DateTimeOffset>(type: "TEXT", nullable: false),
                    UpdatedAt = table.Column<DateTimeOffset>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_greeting_event", x => x.Id);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "greeting_event");
        }
    }
}
