using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace ApontamentoTempos.API.Migrations
{
    public partial class Initial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "projetos",
                columns: table => new
                {
                    id = table.Column<Guid>(nullable: false),
                    nome = table.Column<string>(type: "varchar(64)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_projetos", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "usuarios",
                columns: table => new
                {
                    id = table.Column<Guid>(nullable: false),
                    nome = table.Column<string>(type: "varchar(64)", nullable: false),
                    email = table.Column<string>(type: "varchar(64)", nullable: false),
                    senha = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_usuarios", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "apontamentos_tempo",
                columns: table => new
                {
                    id = table.Column<Guid>(nullable: false),
                    usuario_id = table.Column<Guid>(nullable: false),
                    projeto_id = table.Column<Guid>(nullable: false),
                    data = table.Column<DateTime>(nullable: false),
                    issue = table.Column<string>(nullable: false),
                    tempo = table.Column<decimal>(nullable: false),
                    produtivo = table.Column<bool>(nullable: false),
                    atividade = table.Column<int>(nullable: false),
                    observacao = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_apontamentos_tempo", x => x.id);
                    table.ForeignKey(
                        name: "fk_apontamentos_tempo_projetos_projeto_id",
                        column: x => x.projeto_id,
                        principalTable: "projetos",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "fk_apontamentos_tempo_usuarios_usuario_id",
                        column: x => x.usuario_id,
                        principalTable: "usuarios",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "recuperacoes_senha",
                columns: table => new
                {
                    id = table.Column<Guid>(nullable: false),
                    usuario_id = table.Column<Guid>(nullable: false),
                    data = table.Column<DateTime>(nullable: false),
                    usado = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_recuperacoes_senha", x => x.id);
                    table.ForeignKey(
                        name: "fk_recuperacoes_senha_usuarios_usuario_id",
                        column: x => x.usuario_id,
                        principalTable: "usuarios",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "ix_apontamentos_tempo_projeto_id",
                table: "apontamentos_tempo",
                column: "projeto_id");

            migrationBuilder.CreateIndex(
                name: "ix_apontamentos_tempo_usuario_id",
                table: "apontamentos_tempo",
                column: "usuario_id");

            migrationBuilder.CreateIndex(
                name: "ix_recuperacoes_senha_usuario_id",
                table: "recuperacoes_senha",
                column: "usuario_id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "apontamentos_tempo");

            migrationBuilder.DropTable(
                name: "recuperacoes_senha");

            migrationBuilder.DropTable(
                name: "projetos");

            migrationBuilder.DropTable(
                name: "usuarios");
        }
    }
}
