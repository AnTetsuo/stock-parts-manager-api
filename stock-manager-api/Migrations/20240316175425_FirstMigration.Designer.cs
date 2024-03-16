﻿// <auto-generated />
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;
using stock_manager_api.Repository;

#nullable disable

namespace stock_manager_api.Migrations
{
    [DbContext(typeof(StockManagerContext))]
    [Migration("20240316175425_FirstMigration")]
    partial class FirstMigration
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.3")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            NpgsqlModelBuilderExtensions.UseIdentityByDefaultColumns(modelBuilder);

            modelBuilder.Entity("stock_manager_api.Models.AutoPart", b =>
                {
                    b.Property<int>("AutoPartId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasColumnName("auto_part_id");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("AutoPartId"));

                    b.Property<int>("Budgeted")
                        .HasColumnType("integer")
                        .HasColumnName("budgeted");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("name");

                    b.Property<int>("Stock")
                        .HasColumnType("integer")
                        .HasColumnName("quantity");

                    b.HasKey("AutoPartId");

                    b.ToTable("AutoParts");
                });

            modelBuilder.Entity("stock_manager_api.Models.Budget", b =>
                {
                    b.Property<int>("BudgetId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("BudgetId"));

                    b.Property<int>("CarId")
                        .HasColumnType("integer")
                        .HasColumnName("car_id");

                    b.Property<int>("ClientId")
                        .HasColumnType("integer")
                        .HasColumnName("client_id");

                    b.HasKey("BudgetId");

                    b.HasIndex("CarId");

                    b.HasIndex("ClientId");

                    b.ToTable("Budgets");
                });

            modelBuilder.Entity("stock_manager_api.Models.BudgetedPart", b =>
                {
                    b.Property<int>("BudgetedPartId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasColumnName("budgeted_part_id");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("BudgetedPartId"));

                    b.Property<int>("AutoPartId")
                        .HasColumnType("integer")
                        .HasColumnName("auto_part_id");

                    b.Property<int>("BudgetId")
                        .HasColumnType("integer")
                        .HasColumnName("budget_id");

                    b.Property<int>("Quantity")
                        .HasColumnType("integer")
                        .HasColumnName("quantity");

                    b.HasKey("BudgetedPartId");

                    b.HasIndex("AutoPartId");

                    b.HasIndex("BudgetId");

                    b.ToTable("BudgetedParts");
                });

            modelBuilder.Entity("stock_manager_api.Models.Car", b =>
                {
                    b.Property<int>("CarId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasColumnName("car_id");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("CarId"));

                    b.Property<string>("Plate")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("plate");

                    b.HasKey("CarId");

                    b.ToTable("Cars");
                });

            modelBuilder.Entity("stock_manager_api.Models.Client", b =>
                {
                    b.Property<int>("ClientId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasColumnName("client_id");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("ClientId"));

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("name");

                    b.HasKey("ClientId");

                    b.ToTable("Clients");
                });

            modelBuilder.Entity("stock_manager_api.Models.Budget", b =>
                {
                    b.HasOne("stock_manager_api.Models.Car", "Car")
                        .WithMany("Budgets")
                        .HasForeignKey("CarId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("stock_manager_api.Models.Client", "Client")
                        .WithMany("Budgets")
                        .HasForeignKey("ClientId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Car");

                    b.Navigation("Client");
                });

            modelBuilder.Entity("stock_manager_api.Models.BudgetedPart", b =>
                {
                    b.HasOne("stock_manager_api.Models.AutoPart", "AutoPart")
                        .WithMany()
                        .HasForeignKey("AutoPartId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("stock_manager_api.Models.Budget", "Budget")
                        .WithMany()
                        .HasForeignKey("BudgetId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("AutoPart");

                    b.Navigation("Budget");
                });

            modelBuilder.Entity("stock_manager_api.Models.Car", b =>
                {
                    b.Navigation("Budgets");
                });

            modelBuilder.Entity("stock_manager_api.Models.Client", b =>
                {
                    b.Navigation("Budgets");
                });
#pragma warning restore 612, 618
        }
    }
}
