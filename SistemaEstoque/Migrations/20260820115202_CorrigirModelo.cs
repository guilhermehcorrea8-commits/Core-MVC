using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SistemaEstoque.Migrations
{
    /// <inheritdoc />
    public partial class CorrigirModelo : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_MovimentacoesEstoque_Produtos_ProdutoId",
                table: "MovimentacoesEstoque");

            migrationBuilder.AddColumn<string>(
                name: "Descricao",
                table: "Produtos",
                type: "nvarchar(255)",
                maxLength: 255,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AlterColumn<string>(
                name: "Observacao",
                table: "MovimentacoesEstoque",
                type: "nvarchar(500)",
                maxLength: 500,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(300)",
                oldMaxLength: 300,
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_MovimentacoesEstoque_Produtos_ProdutoId",
                table: "MovimentacoesEstoque",
                column: "ProdutoId",
                principalTable: "Produtos",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_MovimentacoesEstoque_Produtos_ProdutoId",
                table: "MovimentacoesEstoque");

            migrationBuilder.DropColumn(
                name: "Descricao",
                table: "Produtos");

            migrationBuilder.AlterColumn<string>(
                name: "Observacao",
                table: "MovimentacoesEstoque",
                type: "nvarchar(300)",
                maxLength: 300,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(500)",
                oldMaxLength: 500,
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_MovimentacoesEstoque_Produtos_ProdutoId",
                table: "MovimentacoesEstoque",
                column: "ProdutoId",
                principalTable: "Produtos",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
