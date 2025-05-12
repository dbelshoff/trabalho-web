using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace projetoX.Migrations
{
    /// <inheritdoc />
    public partial class addTabelas : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Usuarios",
                keyColumn: "Id",
                keyValue: 1);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Usuarios",
                columns: new[] { "Id", "Email", "IsAdmin", "Nome", "SenhaHash" },
                values: new object[] { 1, "admin@projetox.com", true, "Administrador", "$2a$11$BY3pcXfPZCatGLI0NBTa9e4hZ8gbkQ0bUl.2P7JG1VeRctwnjo2sq" });
        }
    }
}
