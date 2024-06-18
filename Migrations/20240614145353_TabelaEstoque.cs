using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Octavados.Migrations
{
    /// <inheritdoc />
    public partial class TabelaEstoque : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Estoques_Categorias_CategoriaId",
                table: "Estoques");

            migrationBuilder.DropColumn(
                name: "Nome",
                table: "Estoques");

            migrationBuilder.RenameColumn(
                name: "CategoriaId",
                table: "Estoques",
                newName: "ProdutoId");

            migrationBuilder.RenameIndex(
                name: "IX_Estoques_CategoriaId",
                table: "Estoques",
                newName: "IX_Estoques_ProdutoId");

            migrationBuilder.AddForeignKey(
                name: "FK_Estoques_Produtos_ProdutoId",
                table: "Estoques",
                column: "ProdutoId",
                principalTable: "Produtos",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Estoques_Produtos_ProdutoId",
                table: "Estoques");

            migrationBuilder.RenameColumn(
                name: "ProdutoId",
                table: "Estoques",
                newName: "CategoriaId");

            migrationBuilder.RenameIndex(
                name: "IX_Estoques_ProdutoId",
                table: "Estoques",
                newName: "IX_Estoques_CategoriaId");

            migrationBuilder.AddColumn<string>(
                name: "Nome",
                table: "Estoques",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddForeignKey(
                name: "FK_Estoques_Categorias_CategoriaId",
                table: "Estoques",
                column: "CategoriaId",
                principalTable: "Categorias",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
