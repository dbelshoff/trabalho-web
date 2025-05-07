using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace projetoX.Migrations
{
    /// <inheritdoc />
    public partial class AdicaoEmail : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Email",
                table: "Clientes",
                type: "longtext",
                nullable: false);

            migrationBuilder.UpdateData(
                table: "Usuarios",
                keyColumn: "Id",
                keyValue: 1,
                column: "SenhaHash",
                value: "$2a$11$BY3pcXfPZCatGLI0NBTa9e4hZ8gbkQ0bUl.2P7JG1VeRctwnjo2sq");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Email",
                table: "Clientes");

            migrationBuilder.UpdateData(
                table: "Usuarios",
                keyColumn: "Id",
                keyValue: 1,
                column: "SenhaHash",
                value: "$2a$11$M6GTLPDdDYKB1YKjmL9vMOMlb0DKDOYwph4F2Tz5hvZ3DCBLBjRM6");
        }
    }
}
