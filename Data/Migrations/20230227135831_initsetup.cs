using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace appjoke.Data.Migrations
{
    public partial class initsetup : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "joke",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    JokeQuestion = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    JokeAnswer = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_joke", x => x.ID);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "joke");
        }
    }
}
