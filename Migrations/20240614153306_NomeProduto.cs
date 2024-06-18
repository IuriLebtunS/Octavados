using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Octavados.Migrations
{
    /// <inheritdoc />
    public partial class NomeProduto : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "NomeProduto",
                table: "Estoques",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "NomeProduto",
                table: "Estoques");
        }
    }
}
