﻿// <auto-generated />
using System;
using AdjutantApi.Data;
using AdjutantApi.Data.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

namespace AdjutantApi.Migrations
{
    [DbContext(typeof(AdjutantContext))]
    partial class AdjutantContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn)
                .HasAnnotation("ProductVersion", "2.2.4-servicing-10062")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            modelBuilder.Entity("AdjutantApi.Data.Models.DiscordAccount", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("AccountId");

                    b.Property<int?>("BoundKeyId");

                    b.HasKey("Id");

                    b.HasIndex("BoundKeyId");

                    b.ToTable("DiscordAccounts");
                });

            modelBuilder.Entity("AdjutantApi.Data.Models.VerificationKey", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("ConsumptionState");

                    b.Property<string>("DisplayName");

                    b.Property<string>("KeyValue");

                    b.HasKey("Id");

                    b.ToTable("VerificationKeys");
                });

            modelBuilder.Entity("AdjutantApi.Data.Models.DiscordAccount", b =>
                {
                    b.HasOne("AdjutantApi.Data.Models.VerificationKey", "BoundKey")
                        .WithMany()
                        .HasForeignKey("BoundKeyId");
                });
#pragma warning restore 612, 618
        }
    }
}
