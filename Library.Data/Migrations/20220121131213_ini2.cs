using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Library.Data.Migrations
{
    public partial class ini2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Loans",
                keyColumn: "Id",
                keyValue: 1L,
                column: "CreatedOn",
                value: new DateTime(2022, 1, 21, 15, 12, 12, 160, DateTimeKind.Local).AddTicks(9124));

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 1L,
                columns: new[] { "CreatedOn", "Password" },
                values: new object[] { new DateTime(2022, 1, 21, 15, 12, 11, 944, DateTimeKind.Local).AddTicks(7289), "$2a$11$BMh.2wQgIKT.2IjIFGlQiu7XZRs1FfUiesEbHOT.Xj53QKJQyA/Py" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Loans",
                keyColumn: "Id",
                keyValue: 1L,
                column: "CreatedOn",
                value: new DateTime(2022, 1, 21, 15, 0, 3, 627, DateTimeKind.Local).AddTicks(6243));

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 1L,
                columns: new[] { "CreatedOn", "Password" },
                values: new object[] { new DateTime(2022, 1, 21, 15, 0, 3, 367, DateTimeKind.Local).AddTicks(6434), "$2a$11$UoTSp2pGsTliI/aBGcZhNeBPTUzUcwwPqvvEDIfvYAbYSjhorNoqG" });
        }
    }
}
