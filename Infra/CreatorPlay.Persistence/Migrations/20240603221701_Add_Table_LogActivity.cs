using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CreatorPlay.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class Add_Table_LogActivity : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "LogActivity",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    LogLevel = table.Column<int>(type: "int", nullable: false),
                    User = table.Column<string>(type: "varchar(100)", nullable: false),
                    TypeUser = table.Column<int>(type: "int", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Message = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LogActivity", x => x.Id)
                        .Annotation("SqlServer:Clustered", true);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "LogActivity");
        }
    }
}
