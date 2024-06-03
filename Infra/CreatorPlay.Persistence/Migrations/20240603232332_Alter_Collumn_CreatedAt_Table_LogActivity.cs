using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CreatorPlay.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class Alter_Collumn_CreatedAt_Table_LogActivity : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedAt",
                table: "LogActivity",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2024, 6, 3, 20, 23, 32, 256, DateTimeKind.Local).AddTicks(875),
                oldClrType: typeof(DateTime),
                oldType: "datetime2");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedAt",
                table: "LogActivity",
                type: "datetime2",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2024, 6, 3, 20, 23, 32, 256, DateTimeKind.Local).AddTicks(875));
        }
    }
}
