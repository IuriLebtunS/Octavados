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

            migrationBuilder.RenameColumn(
                name: "QuantidadeEmEstoque",
                table: "Produtos",
                newName: "EstoqueId");

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

            migrationBuilder.RenameColumn(
                name: "EstoqueId",
                table: "Produtos",
                newName: "QuantidadeEmEstoque");

            migrationBuilder.CreateIndex(
                name: "IX_Estoques_ProdutoId",
                table: "Estoques",
                column: "ProdutoId");
        }
    }
}
