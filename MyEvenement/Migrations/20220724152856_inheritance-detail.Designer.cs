﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using MyEvenement.Data;

namespace MyEvenement.Migrations
{
    [DbContext(typeof(MyEvenementContext))]
    [Migration("20220724152856_inheritance-detail")]
    partial class inheritancedetail
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("ProductVersion", "5.0.17")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("MyEvenement.Models.Detail", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Discriminator")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Nationalite")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Pays")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("ID");

                    b.ToTable("Detai");

                    b.HasDiscriminator<string>("Discriminator").HasValue("Detail");
                });

            modelBuilder.Entity("MyEvenement.Models.Evenement", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<DateTime>("Date")
                        .HasColumnType("datetime2");

                    b.Property<string>("Nom")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Text")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("ID");

                    b.ToTable("Evenement");
                });

            modelBuilder.Entity("MyEvenement.Models.Inscription", b =>
                {
                    b.Property<int>("InscriptionID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Adress")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Email")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("EvenementID")
                        .HasColumnType("int");

                    b.Property<string>("Nationalite")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Nom")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Prenom")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Telephone")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("InscriptionID");

                    b.HasIndex("EvenementID");

                    b.ToTable("Inscription");
                });

            modelBuilder.Entity("MyEvenement.Models.DetailInternational", b =>
                {
                    b.HasBaseType("MyEvenement.Models.Detail");

                    b.Property<string>("Passeport")
                        .HasColumnType("nvarchar(max)");

                    b.HasDiscriminator().HasValue("DetailInternational");
                });

            modelBuilder.Entity("MyEvenement.Models.DetailNational", b =>
                {
                    b.HasBaseType("MyEvenement.Models.Detail");

                    b.Property<string>("Adress")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("CIN")
                        .HasColumnType("nvarchar(max)");

                    b.HasDiscriminator().HasValue("DetailNational");
                });

            modelBuilder.Entity("MyEvenement.Models.Inscription", b =>
                {
                    b.HasOne("MyEvenement.Models.Evenement", "Evenement")
                        .WithMany("Inscriptions")
                        .HasForeignKey("EvenementID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Evenement");
                });

            modelBuilder.Entity("MyEvenement.Models.Evenement", b =>
                {
                    b.Navigation("Inscriptions");
                });
#pragma warning restore 612, 618
        }
    }
}
