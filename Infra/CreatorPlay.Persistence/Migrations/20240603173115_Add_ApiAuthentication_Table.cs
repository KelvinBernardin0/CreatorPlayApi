using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CreatorPlay.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class Add_ApiAuthentication_Table : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "ApiAuthentication",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(150)", nullable: false),
                    ClientId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Secret = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    AuthenticationEndpoint = table.Column<string>(type: "nvarchar(150)", nullable: false),
                    Status = table.Column<int>(type: "int", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ModifiedAt = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ApiAuthentication", x => x.Id)
                        .Annotation("SqlServer:Clustered", true);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ApiAuthentication");
        }
    }
}
