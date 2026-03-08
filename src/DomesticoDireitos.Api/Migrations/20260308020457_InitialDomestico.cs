using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DomesticoDireitos.Api.Migrations
{
    /// <inheritdoc />
    public partial class InitialDomestico : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Categorias",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nome = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Categorias", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Diagnosticos",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    DataCriacao = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Diagnosticos", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Subitens",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Titulo = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Conteudo = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CategoriaId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Subitens", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Subitens_Categorias_CategoriaId",
                        column: x => x.CategoriaId,
                        principalTable: "Categorias",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "DiagnosticosItens",
                columns: table => new
                {
                    DiagnosticoId = table.Column<int>(type: "int", nullable: false),
                    SubitemId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DiagnosticosItens", x => new { x.DiagnosticoId, x.SubitemId });
                    table.ForeignKey(
                        name: "FK_DiagnosticosItens_Diagnosticos_DiagnosticoId",
                        column: x => x.DiagnosticoId,
                        principalTable: "Diagnosticos",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_DiagnosticosItens_Subitens_SubitemId",
                        column: x => x.SubitemId,
                        principalTable: "Subitens",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_DiagnosticosItens_SubitemId",
                table: "DiagnosticosItens",
                column: "SubitemId");

            migrationBuilder.CreateIndex(
                name: "IX_Subitens_CategoriaId",
                table: "Subitens",
                column: "CategoriaId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "DiagnosticosItens");

            migrationBuilder.DropTable(
                name: "Diagnosticos");

            migrationBuilder.DropTable(
                name: "Subitens");

            migrationBuilder.DropTable(
                name: "Categorias");
        }
    }
}
