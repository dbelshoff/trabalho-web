using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace projetoX.Migrations
{
    /// <inheritdoc />
    public partial class AdicaoNivel : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "Complemento",
                table: "Enderecos",
                type: "longtext",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "longtext");

            migrationBuilder.AddColumn<string>(
                name: "Nivel",
                table: "Empresas",
                type: "longtext",
                nullable: false);

            migrationBuilder.AddColumn<bool>(
                name: "Requerido",
                table: "Empresas",
                type: "tinyint(1)",
                nullable: false,
                defaultValue: false);

            migrationBuilder.UpdateData(
                table: "Usuarios",
                keyColumn: "Id",
                keyValue: 1,
                column: "SenhaHash",
                value: "$2a$11$yLacD1VP0/693CH7NHbo5OtvtpJOuoLOwPOo4x6wtrytbuifGRwPS");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Nivel",
                table: "Empresas");

            migrationBuilder.DropColumn(
                name: "Requerido",
                table: "Empresas");

            migrationBuilder.AlterColumn<string>(
                name: "Complemento",
                table: "Enderecos",
                type: "longtext",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "longtext",
                oldNullable: true);

            migrationBuilder.UpdateData(
                table: "Usuarios",
                keyColumn: "Id",
                keyValue: 1,
                column: "SenhaHash",
                value: "$2a$11$kJkR8pYqYBVGloKATXqw5OWRJJboeUDsnMj/vsCh3n/d97znHb3Uq");
        }
    }
}
