using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Octavados.Migrations
{
    /// <inheritdoc />
    public partial class AdicioneiPropiedadeNaClaseVenda : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_ProdutoVendas_ProdutoId",
                table: "ProdutoVendas",
                column: "ProdutoId");

            migrationBuilder.AddForeignKey(
                name: "FK_ProdutoVendas_Produtos_ProdutoId",
                table: "ProdutoVendas",
                column: "ProdutoId",
                principalTable: "Produtos",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ProdutoVendas_Produtos_ProdutoId",
                table: "ProdutoVendas");

            migrationBuilder.DropIndex(
                name: "IX_ProdutoVendas_ProdutoId",
                table: "ProdutoVendas");
        }
    }
}
