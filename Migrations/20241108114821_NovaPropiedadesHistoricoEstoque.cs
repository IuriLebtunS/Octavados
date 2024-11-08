using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Octavados.Migrations
{
    /// <inheritdoc />
    public partial class NovaPropiedadesHistoricoEstoque : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_HistoricoEstoque_Produtos_ProdutoId",
                table: "HistoricoEstoque");

            migrationBuilder.DropPrimaryKey(
                name: "PK_HistoricoEstoque",
                table: "HistoricoEstoque");

            migrationBuilder.DropColumn(
                name: "DataAtualizacao",
                table: "HistoricoEstoque");

            migrationBuilder.RenameTable(
                name: "HistoricoEstoque",
                newName: "HistoricoEstoques");

            migrationBuilder.RenameIndex(
                name: "IX_HistoricoEstoque_ProdutoId",
                table: "HistoricoEstoques",
                newName: "IX_HistoricoEstoques_ProdutoId");

            migrationBuilder.AddColumn<string>(
                name: "Usuario",
                table: "HistoricoEstoques",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddPrimaryKey(
                name: "PK_HistoricoEstoques",
                table: "HistoricoEstoques",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_HistoricoEstoques_Produtos_ProdutoId",
                table: "HistoricoEstoques",
                column: "ProdutoId",
                principalTable: "Produtos",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_HistoricoEstoques_Produtos_ProdutoId",
                table: "HistoricoEstoques");

            migrationBuilder.DropPrimaryKey(
                name: "PK_HistoricoEstoques",
                table: "HistoricoEstoques");

            migrationBuilder.DropColumn(
                name: "Usuario",
                table: "HistoricoEstoques");

            migrationBuilder.RenameTable(
                name: "HistoricoEstoques",
                newName: "HistoricoEstoque");

            migrationBuilder.RenameIndex(
                name: "IX_HistoricoEstoques_ProdutoId",
                table: "HistoricoEstoque",
                newName: "IX_HistoricoEstoque_ProdutoId");

            migrationBuilder.AddColumn<DateTime>(
                name: "DataAtualizacao",
                table: "HistoricoEstoque",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddPrimaryKey(
                name: "PK_HistoricoEstoque",
                table: "HistoricoEstoque",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_HistoricoEstoque_Produtos_ProdutoId",
                table: "HistoricoEstoque",
                column: "ProdutoId",
                principalTable: "Produtos",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
