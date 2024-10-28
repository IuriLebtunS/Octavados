using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Octavados.Migrations
{
    /// <inheritdoc />
    public partial class ApagueiumaPropiedadeDeEstoque : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "EstoqueId",
                table: "Produtos");

            migrationBuilder.AddColumn<DateTime>(
                name: "DataAtualizacao",
                table: "Estoques",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<int>(
                name: "QuantidadeAdicionada",
                table: "Estoques",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DataAtualizacao",
                table: "Estoques");

            migrationBuilder.DropColumn(
                name: "QuantidadeAdicionada",
                table: "Estoques");

            migrationBuilder.AddColumn<int>(
                name: "EstoqueId",
                table: "Produtos",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }
    }
}
