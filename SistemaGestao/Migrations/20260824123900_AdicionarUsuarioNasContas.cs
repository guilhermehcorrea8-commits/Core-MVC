using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SistemaGestao.Migrations
{
    /// <inheritdoc />
    public partial class AdicionarUsuarioNasContas : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "UsuarioId",
                table: "Contas",
                type: "nvarchar(450)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateIndex(
                name: "IX_Contas_UsuarioId",
                table: "Contas",
                column: "UsuarioId");

            migrationBuilder.AddForeignKey(
                name: "FK_Contas_AspNetUsers_UsuarioId",
                table: "Contas",
                column: "UsuarioId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Contas_AspNetUsers_UsuarioId",
                table: "Contas");

            migrationBuilder.DropIndex(
                name: "IX_Contas_UsuarioId",
                table: "Contas");

            migrationBuilder.DropColumn(
                name: "UsuarioId",
                table: "Contas");
        }
    }
}
