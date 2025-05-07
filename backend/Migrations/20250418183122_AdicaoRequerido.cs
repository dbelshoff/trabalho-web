using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace projetoX.Migrations
{
    /// <inheritdoc />
    public partial class AdicaoRequerido : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Usuarios",
                keyColumn: "Id",
                keyValue: 1,
                column: "SenhaHash",
                value: "$2a$11$M6GTLPDdDYKB1YKjmL9vMOMlb0DKDOYwph4F2Tz5hvZ3DCBLBjRM6");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Usuarios",
                keyColumn: "Id",
                keyValue: 1,
                column: "SenhaHash",
                value: "$2a$11$yLacD1VP0/693CH7NHbo5OtvtpJOuoLOwPOo4x6wtrytbuifGRwPS");
        }
    }
}
