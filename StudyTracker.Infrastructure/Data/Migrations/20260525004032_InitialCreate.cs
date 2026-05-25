using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace StudyTracker.Infrastructure.Data.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "SessoesEstudo",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Materia = table.Column<string>(type: "TEXT", nullable: false),
                    Topico = table.Column<string>(type: "TEXT", nullable: false),
                    MinutosEstudados = table.Column<int>(type: "INTEGER", nullable: false),
                    Dificuldade = table.Column<int>(type: "INTEGER", nullable: false),
                    DataSessao = table.Column<DateTime>(type: "TEXT", nullable: false),
                    Concluido = table.Column<bool>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SessoesEstudo", x => x.Id);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "SessoesEstudo");
        }
    }
}
