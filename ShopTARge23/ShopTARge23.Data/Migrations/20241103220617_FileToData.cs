using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ShopTARge23.Data.Migrations
{
    /// <inheritdoc />
    public partial class FileToData : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {

            migrationBuilder.CreateTable(
                name: "FileToDatas",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ExistingFilePath = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ImageTitles = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    KindergartenId = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FileToDatas", x => x.Id);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "FileToDatas");
        }
    }
}
