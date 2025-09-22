using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SH1ProjeUygulamasi.Data.Migrations
{
    /// <inheritdoc />
    public partial class IsHomeEklendi : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "IsHome",
                table: "Products",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "CreateDate", "UserGuid" },
                values: new object[] { new DateTime(2025, 9, 22, 21, 34, 21, 266, DateTimeKind.Local).AddTicks(4767), null });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsHome",
                table: "Products");

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "CreateDate", "UserGuid" },
                values: new object[] { new DateTime(2025, 9, 11, 22, 25, 43, 337, DateTimeKind.Local).AddTicks(8995), new Guid("b50b9cd5-101e-4770-bf24-828c520df830") });
        }
    }
}
