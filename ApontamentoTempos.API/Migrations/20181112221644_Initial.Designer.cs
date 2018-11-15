﻿// <auto-generated />
using System;
using ApontamentoTempos.API.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

namespace ApontamentoTempos.API.Migrations
{
    [DbContext(typeof(MyDbContext))]
    [Migration("20181112221644_Initial")]
    partial class Initial
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn)
                .HasAnnotation("ProductVersion", "2.1.4-rtm-31024")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            modelBuilder.Entity("ApontamentoTempos.API.Model.ApontamentoTempo", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnName("id");

                    b.Property<int>("Atividade")
                        .HasColumnName("atividade");

                    b.Property<DateTime>("Data")
                        .HasColumnName("data");

                    b.Property<string>("Issue")
                        .IsRequired()
                        .HasColumnName("issue");

                    b.Property<string>("Observacao")
                        .HasColumnName("observacao")
                        .HasColumnType("text");

                    b.Property<bool>("Produtivo")
                        .HasColumnName("produtivo");

                    b.Property<Guid>("ProjetoId")
                        .HasColumnName("projeto_id");

                    b.Property<decimal>("Tempo")
                        .HasColumnName("tempo");

                    b.Property<Guid>("UsuarioId")
                        .HasColumnName("usuario_id");

                    b.HasKey("Id")
                        .HasName("pk_apontamentos_tempo");

                    b.HasIndex("ProjetoId")
                        .HasName("ix_apontamentos_tempo_projeto_id");

                    b.HasIndex("UsuarioId")
                        .HasName("ix_apontamentos_tempo_usuario_id");

                    b.ToTable("apontamentos_tempo");
                });

            modelBuilder.Entity("ApontamentoTempos.API.Model.Projeto", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnName("id");

                    b.Property<string>("Nome")
                        .IsRequired()
                        .HasColumnName("nome")
                        .HasColumnType("varchar(64)");

                    b.HasKey("Id")
                        .HasName("pk_projetos");

                    b.ToTable("projetos");
                });

            modelBuilder.Entity("ApontamentoTempos.API.Model.RecuperacaoSenha", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnName("id");

                    b.Property<DateTime>("Data")
                        .HasColumnName("data");

                    b.Property<Guid>("UsuarioId")
                        .HasColumnName("usuario_id");

                    b.HasKey("Id")
                        .HasName("pk_recuperacoes_senha");

                    b.HasIndex("UsuarioId")
                        .HasName("ix_recuperacoes_senha_usuario_id");

                    b.ToTable("recuperacoes_senha");
                });

            modelBuilder.Entity("ApontamentoTempos.API.Model.Usuario", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnName("id");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnName("email")
                        .HasColumnType("varchar(64)");

                    b.Property<string>("Nome")
                        .IsRequired()
                        .HasColumnName("nome")
                        .HasColumnType("varchar(64)");

                    b.Property<string>("Senha")
                        .IsRequired()
                        .HasColumnName("senha");

                    b.HasKey("Id")
                        .HasName("pk_usuarios");

                    b.ToTable("usuarios");
                });

            modelBuilder.Entity("ApontamentoTempos.API.Model.ApontamentoTempo", b =>
                {
                    b.HasOne("ApontamentoTempos.API.Model.Projeto", "Projeto")
                        .WithMany()
                        .HasForeignKey("ProjetoId")
                        .HasConstraintName("fk_apontamentos_tempo_projetos_projeto_id")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("ApontamentoTempos.API.Model.Usuario", "Usuario")
                        .WithMany()
                        .HasForeignKey("UsuarioId")
                        .HasConstraintName("fk_apontamentos_tempo_usuarios_usuario_id")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("ApontamentoTempos.API.Model.RecuperacaoSenha", b =>
                {
                    b.HasOne("ApontamentoTempos.API.Model.Usuario", "Usuario")
                        .WithMany()
                        .HasForeignKey("UsuarioId")
                        .HasConstraintName("fk_recuperacoes_senha_usuarios_usuario_id")
                        .OnDelete(DeleteBehavior.Cascade);
                });
#pragma warning restore 612, 618
        }
    }
}
