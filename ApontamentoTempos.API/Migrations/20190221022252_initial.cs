using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace ApontamentoTempos.API.Migrations
{
    public partial class initial : Migration
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
                name: "apontamento_tempos",
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
                    table.PrimaryKey("pk_apontamento_tempos", x => x.id);
                    table.ForeignKey(
                        name: "fk_apontamento_tempos_projetos_projeto_id",
                        column: x => x.projeto_id,
                        principalTable: "projetos",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "fk_apontamento_tempos_usuarios_usuario_id",
                        column: x => x.usuario_id,
                        principalTable: "usuarios",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "recuperacao_senhas",
                columns: table => new
                {
                    id = table.Column<Guid>(nullable: false),
                    usuario_id = table.Column<Guid>(nullable: false),
                    data = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_recuperacao_senhas", x => x.id);
                    table.ForeignKey(
                        name: "fk_recuperacao_senhas_usuarios_usuario_id",
                        column: x => x.usuario_id,
                        principalTable: "usuarios",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "refresh_tokens",
                columns: table => new
                {
                    id = table.Column<Guid>(nullable: false),
                    usuario_id = table.Column<Guid>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_refresh_tokens", x => x.id);
                    table.ForeignKey(
                        name: "fk_refresh_tokens_usuarios_usuario_id",
                        column: x => x.usuario_id,
                        principalTable: "usuarios",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "ix_apontamento_tempos_projeto_id",
                table: "apontamento_tempos",
                column: "projeto_id");

            migrationBuilder.CreateIndex(
                name: "ix_apontamento_tempos_usuario_id",
                table: "apontamento_tempos",
                column: "usuario_id");

            migrationBuilder.CreateIndex(
                name: "ix_recuperacao_senhas_usuario_id",
                table: "recuperacao_senhas",
                column: "usuario_id");

            migrationBuilder.CreateIndex(
                name: "ix_refresh_tokens_usuario_id",
                table: "refresh_tokens",
                column: "usuario_id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "apontamento_tempos");

            migrationBuilder.DropTable(
                name: "recuperacao_senhas");

            migrationBuilder.DropTable(
                name: "refresh_tokens");

            migrationBuilder.DropTable(
                name: "projetos");

            migrationBuilder.DropTable(
                name: "usuarios");
        }
    }
}
