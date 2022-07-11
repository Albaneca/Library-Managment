using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Library.Data.Migrations
{
    public partial class initial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Authors",
                keyColumn: "Id",
                keyValue: 1L,
                column: "URL",
                value: "https://res.cloudinary.com/denfz6l1q/image/upload/v1639012542/profilePictures/oymjlgd6v2yi9dm3pynn.jpg");

            migrationBuilder.UpdateData(
                table: "Authors",
                keyColumn: "Id",
                keyValue: 2L,
                column: "URL",
                value: "https://res.cloudinary.com/denfz6l1q/image/upload/v1639012542/profilePictures/oymjlgd6v2yi9dm3pynn.jpg");

            migrationBuilder.UpdateData(
                table: "Books",
                keyColumn: "Id",
                keyValue: 1L,
                column: "URL",
                value: "https://res.cloudinary.com/denfz6l1q/image/upload/v1644718347/booksPictures/leather-book-preview_gyrmg9.png");

            migrationBuilder.UpdateData(
                table: "Books",
                keyColumn: "Id",
                keyValue: 2L,
                column: "URL",
                value: "https://res.cloudinary.com/denfz6l1q/image/upload/v1644718347/booksPictures/leather-book-preview_gyrmg9.png");

            migrationBuilder.UpdateData(
                table: "Loans",
                keyColumn: "Id",
                keyValue: 1L,
                columns: new[] { "CreatedOn", "Status" },
                values: new object[] { new DateTime(2022, 7, 11, 17, 14, 35, 783, DateTimeKind.Local).AddTicks(8284), "Waiting" });

            migrationBuilder.UpdateData(
                table: "PublishHouses",
                keyColumn: "Id",
                keyValue: 1L,
                column: "URL",
                value: "https://res.cloudinary.com/denfz6l1q/image/upload/v1645362793/publishHousePictures/Random_House_logo_bw2-1024x819_ptore3.jpg");

            migrationBuilder.UpdateData(
                table: "PublishHouses",
                keyColumn: "Id",
                keyValue: 2L,
                column: "URL",
                value: "https://res.cloudinary.com/denfz6l1q/image/upload/v1645362793/publishHousePictures/Random_House_logo_bw2-1024x819_ptore3.jpg");

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 1L,
                columns: new[] { "ApplicationRoleId", "CreatedOn", "Password" },
                values: new object[] { 1L, new DateTime(2022, 7, 11, 17, 14, 35, 633, DateTimeKind.Local).AddTicks(7258), "$2a$11$mgCNQGQc9nWxjho6i2OtVeghsz9B3awYgvDWed5PAqrkzyuEZIOgS" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Authors",
                keyColumn: "Id",
                keyValue: 1L,
                column: "URL",
                value: "");

            migrationBuilder.UpdateData(
                table: "Authors",
                keyColumn: "Id",
                keyValue: 2L,
                column: "URL",
                value: "");

            migrationBuilder.UpdateData(
                table: "Books",
                keyColumn: "Id",
                keyValue: 1L,
                column: "URL",
                value: "");

            migrationBuilder.UpdateData(
                table: "Books",
                keyColumn: "Id",
                keyValue: 2L,
                column: "URL",
                value: "");

            migrationBuilder.UpdateData(
                table: "Loans",
                keyColumn: "Id",
                keyValue: 1L,
                columns: new[] { "CreatedOn", "Status" },
                values: new object[] { new DateTime(2022, 1, 21, 15, 12, 12, 160, DateTimeKind.Local).AddTicks(9124), "Waiting for approval" });

            migrationBuilder.UpdateData(
                table: "PublishHouses",
                keyColumn: "Id",
                keyValue: 1L,
                column: "URL",
                value: "");

            migrationBuilder.UpdateData(
                table: "PublishHouses",
                keyColumn: "Id",
                keyValue: 2L,
                column: "URL",
                value: "");

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 1L,
                columns: new[] { "ApplicationRoleId", "CreatedOn", "Password" },
                values: new object[] { 2L, new DateTime(2022, 1, 21, 15, 12, 11, 944, DateTimeKind.Local).AddTicks(7289), "$2a$11$BMh.2wQgIKT.2IjIFGlQiu7XZRs1FfUiesEbHOT.Xj53QKJQyA/Py" });
        }
    }
}
