using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CreatorPlay.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class Remove_Collumn_ApplictionType_Table_AspNetRoles : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
			migrationBuilder.DropColumn(
			   name: "ApplicationType",
			   table: "AspNetRoles");
		}

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
			migrationBuilder.AddColumn<int>(
			 name: "ApplicationType",
			 table: "AspNetRoles",
			 type: "int",
			 nullable: false,
			 defaultValue: 0);
		}
    }
}
