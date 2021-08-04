﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Waffle.Inc.Repositories;

namespace Waffle.Inc.Migrations
{
    [DbContext(typeof(WaffleIncDbContext))]
    partial class WaffleIncDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("ProductVersion", "5.0.8")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("Waffle.Inc.Domain.Funcionario", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Email")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int?>("LiderId")
                        .HasColumnType("int");

                    b.Property<string>("LiderNome")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Nome")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("NumeroChapa")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("Role")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Senha")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Sobrenome")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("NumeroChapa")
                        .IsUnique()
                        .HasFilter("[NumeroChapa] IS NOT NULL");

                    b.ToTable("Funcionarios");
                });

            modelBuilder.Entity("Waffle.Inc.Domain.Telefone", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("CodigoDeArea")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("FuncionarioId")
                        .HasColumnType("int");

                    b.Property<string>("Numero")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("FuncionarioId");

                    b.ToTable("Telefones");
                });

            modelBuilder.Entity("Waffle.Inc.Domain.Telefone", b =>
                {
                    b.HasOne("Waffle.Inc.Domain.Funcionario", "Funcionario")
                        .WithMany("Telefones")
                        .HasForeignKey("FuncionarioId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Funcionario");
                });

            modelBuilder.Entity("Waffle.Inc.Domain.Funcionario", b =>
                {
                    b.Navigation("Telefones");
                });
#pragma warning restore 612, 618
        }
    }
}