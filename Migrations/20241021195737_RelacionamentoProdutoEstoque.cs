using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Octavados.Migrations
{
    /// <inheritdoc />
    public partial class RelacionamentoProdutoEstoque : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Estoques_ProdutoId",
                table: "Estoques");

            migrationBuilder.AddColumn<int>(
                name: "EstoqueId",
                table: "Produtos",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Estoques_ProdutoId",
                table: "Estoques",
                column: "ProdutoId",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Estoques_ProdutoId",
                table: "Estoques");

            migrationBuilder.DropColumn(
                name: "EstoqueId",
                table: "Produtos");

            migrationBuilder.CreateIndex(
                name: "IX_Estoques_ProdutoId",
                table: "Estoques",
                column: "ProdutoId");
        }
    }
}
