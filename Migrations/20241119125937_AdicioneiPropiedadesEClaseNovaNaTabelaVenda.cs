using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Octavados.Migrations
{
    /// <inheritdoc />
    public partial class AdicioneiPropiedadesEClaseNovaNaTabelaVenda : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Vendas_Detalhes_DetalheId",
                table: "Vendas");

            migrationBuilder.DropIndex(
                name: "IX_Vendas_DetalheId",
                table: "Vendas");

            migrationBuilder.DropColumn(
                name: "Desconto",
                table: "Vendas");

            migrationBuilder.DropColumn(
                name: "DetalheId",
                table: "Vendas");

            migrationBuilder.DropColumn(
                name: "Quantidade",
                table: "Vendas");

            migrationBuilder.DropColumn(
                name: "Total",
                table: "Vendas");

            migrationBuilder.AddColumn<DateTime>(
                name: "DataVenda",
                table: "Vendas",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.CreateTable(
                name: "DetalheDaVenda",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ProdutoId = table.Column<int>(type: "int", nullable: false),
                    Quantidade = table.Column<int>(type: "int", nullable: false),
                    PrecoUnitario = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Desconto = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    VendaId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DetalheDaVenda", x => x.Id);
                    table.ForeignKey(
                        name: "FK_DetalheDaVenda_Vendas_VendaId",
                        column: x => x.VendaId,
                        principalTable: "Vendas",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_DetalheDaVenda_VendaId",
                table: "DetalheDaVenda",
                column: "VendaId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "DetalheDaVenda");

            migrationBuilder.DropColumn(
                name: "DataVenda",
                table: "Vendas");

            migrationBuilder.AddColumn<decimal>(
                name: "Desconto",
                table: "Vendas",
                type: "decimal(18,2)",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<int>(
                name: "DetalheId",
                table: "Vendas",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "Quantidade",
                table: "Vendas",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<decimal>(
                name: "Total",
                table: "Vendas",
                type: "decimal(18,2)",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.CreateIndex(
                name: "IX_Vendas_DetalheId",
                table: "Vendas",
                column: "DetalheId");

            migrationBuilder.AddForeignKey(
                name: "FK_Vendas_Detalhes_DetalheId",
                table: "Vendas",
                column: "DetalheId",
                principalTable: "Detalhes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
