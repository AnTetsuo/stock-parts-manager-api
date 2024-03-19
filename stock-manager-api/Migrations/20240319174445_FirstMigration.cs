using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace stock_manager_api.Migrations
{
    /// <inheritdoc />
    public partial class FirstMigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "AutoParts",
                columns: table => new
                {
                    auto_part_id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    name = table.Column<string>(type: "text", nullable: false),
                    quantity = table.Column<int>(type: "integer", nullable: false),
                    budgeted = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AutoParts", x => x.auto_part_id);
                });

            migrationBuilder.CreateTable(
                name: "Cars",
                columns: table => new
                {
                    car_id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    plate = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Cars", x => x.car_id);
                });

            migrationBuilder.CreateTable(
                name: "Clients",
                columns: table => new
                {
                    client_id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    name = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Clients", x => x.client_id);
                });

            migrationBuilder.CreateTable(
                name: "Budgets",
                columns: table => new
                {
                    BudgetId = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    client_id = table.Column<int>(type: "integer", nullable: false),
                    car_id = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Budgets", x => x.BudgetId);
                    table.ForeignKey(
                        name: "FK_Budgets_Cars_car_id",
                        column: x => x.car_id,
                        principalTable: "Cars",
                        principalColumn: "car_id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Budgets_Clients_client_id",
                        column: x => x.client_id,
                        principalTable: "Clients",
                        principalColumn: "client_id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "BudgetedParts",
                columns: table => new
                {
                    budgeted_part_id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    auto_part_id = table.Column<int>(type: "integer", nullable: false),
                    budget_id = table.Column<int>(type: "integer", nullable: false),
                    quantity = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BudgetedParts", x => x.budgeted_part_id);
                    table.ForeignKey(
                        name: "FK_BudgetedParts_AutoParts_auto_part_id",
                        column: x => x.auto_part_id,
                        principalTable: "AutoParts",
                        principalColumn: "auto_part_id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_BudgetedParts_Budgets_budget_id",
                        column: x => x.budget_id,
                        principalTable: "Budgets",
                        principalColumn: "BudgetId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "AutoParts",
                columns: new[] { "auto_part_id", "budgeted", "name", "quantity" },
                values: new object[,]
                {
                    { 1, 0, "BRACKET-ENGINE MOUNTING", 10 },
                    { 2, 0, "INSULATOR ASSY-ENGINE MOUNTING", 10 },
                    { 3, 0, "BLOCK ASSY-CYLINDER", 10 },
                    { 4, 0, "SEAL-OIL LEVEL GAUGE GUIDE", 10 },
                    { 5, 0, "COLLECTOR-INTAKE MANIFOLD", 10 }
                });

            migrationBuilder.InsertData(
                table: "Cars",
                columns: new[] { "car_id", "plate" },
                values: new object[,]
                {
                    { 1, "KLV0553" },
                    { 2, "BLN4551" },
                    { 3, "LBT0505" },
                    { 4, "ASF6752" },
                    { 5, "JNQ7346" }
                });

            migrationBuilder.InsertData(
                table: "Clients",
                columns: new[] { "client_id", "name" },
                values: new object[,]
                {
                    { 1, "Camila Tristão" },
                    { 2, "Teobaldo Albano" },
                    { 3, "Ivan Roval" },
                    { 4, "Fabricio Eliseu" },
                    { 5, "Arnaldo Reynaldo" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_BudgetedParts_auto_part_id",
                table: "BudgetedParts",
                column: "auto_part_id");

            migrationBuilder.CreateIndex(
                name: "IX_BudgetedParts_budget_id",
                table: "BudgetedParts",
                column: "budget_id");

            migrationBuilder.CreateIndex(
                name: "IX_Budgets_car_id",
                table: "Budgets",
                column: "car_id");

            migrationBuilder.CreateIndex(
                name: "IX_Budgets_client_id",
                table: "Budgets",
                column: "client_id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "BudgetedParts");

            migrationBuilder.DropTable(
                name: "AutoParts");

            migrationBuilder.DropTable(
                name: "Budgets");

            migrationBuilder.DropTable(
                name: "Cars");

            migrationBuilder.DropTable(
                name: "Clients");
        }
    }
}
