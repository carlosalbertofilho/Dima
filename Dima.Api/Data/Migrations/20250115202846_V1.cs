using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Dima.Api.Data.Migrations
{
    /// <inheritdoc />
    public partial class V1 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Categorias",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Title = table.Column<string>(type: "NVARCHAR(80)", maxLength: 80, nullable: false),
                    UserId = table.Column<string>(type: "VARCHAR(160)", maxLength: 160, nullable: false),
                    Description = table.Column<string>(type: "NVARCHAR(255)", maxLength: 255, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Categorias", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Transacoes",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Title = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    UserId = table.Column<string>(type: "NVARCHAR(80)", maxLength: 80, nullable: false),
                    Type = table.Column<short>(type: "SMALLINT", nullable: false),
                    Amount = table.Column<decimal>(type: "MONEY", nullable: false),
                    CategoryId = table.Column<long>(type: "BIGINT", nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "DATETIME", nullable: false),
                    PaidOrReceivedAt = table.Column<DateTime>(type: "DATETIME", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Transacoes", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Transacoes_Categorias",
                        column: x => x.CategoryId,
                        principalTable: "Categorias",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_Transacoes_CategoryId",
                table: "Transacoes",
                column: "CategoryId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Transacoes");

            migrationBuilder.DropTable(
                name: "Categorias");
        }
    }
}
