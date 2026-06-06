using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace OdinApi.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Operadores",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Nome = table.Column<string>(type: "TEXT", maxLength: 100, nullable: false),
                    Email = table.Column<string>(type: "TEXT", maxLength: 100, nullable: false),
                    NivelAcesso = table.Column<string>(type: "TEXT", maxLength: 20, nullable: false),
                    DataCriacao = table.Column<DateTime>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Operadores", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Detritos",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Identificacao = table.Column<string>(type: "TEXT", maxLength: 100, nullable: false),
                    Latitude = table.Column<decimal>(type: "TEXT", nullable: false),
                    Longitude = table.Column<decimal>(type: "TEXT", nullable: false),
                    Altitude = table.Column<decimal>(type: "TEXT", nullable: false),
                    Velocidade = table.Column<decimal>(type: "TEXT", nullable: false),
                    NivelRisco = table.Column<int>(type: "INTEGER", nullable: false),
                    OperadorId = table.Column<int>(type: "INTEGER", nullable: false),
                    DataDeteccao = table.Column<DateTime>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Detritos", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Detritos_Operadores_OperadorId",
                        column: x => x.OperadorId,
                        principalTable: "Operadores",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Satelites",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Nome = table.Column<string>(type: "TEXT", maxLength: 100, nullable: false),
                    CombustivelAtual = table.Column<decimal>(type: "TEXT", nullable: false),
                    StatusOperacional = table.Column<string>(type: "TEXT", maxLength: 20, nullable: false),
                    DataLancamento = table.Column<DateTime>(type: "TEXT", nullable: false),
                    OperadorId = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Satelites", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Satelites_Operadores_OperadorId",
                        column: x => x.OperadorId,
                        principalTable: "Operadores",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Alertas",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    SateliteId = table.Column<int>(type: "INTEGER", nullable: false),
                    DebitoId = table.Column<int>(type: "INTEGER", nullable: false),
                    Severidade = table.Column<string>(type: "TEXT", maxLength: 20, nullable: false),
                    Mensagem = table.Column<string>(type: "TEXT", maxLength: 500, nullable: false),
                    Status = table.Column<string>(type: "TEXT", maxLength: 20, nullable: false),
                    DataCriacao = table.Column<DateTime>(type: "TEXT", nullable: false),
                    DataResolucao = table.Column<DateTime>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Alertas", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Alertas_Detritos_DebitoId",
                        column: x => x.DebitoId,
                        principalTable: "Detritos",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Alertas_Satelites_SateliteId",
                        column: x => x.SateliteId,
                        principalTable: "Satelites",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Manobras",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    SateliteId = table.Column<int>(type: "INTEGER", nullable: false),
                    OperadorId = table.Column<int>(type: "INTEGER", nullable: false),
                    Tipo = table.Column<string>(type: "TEXT", maxLength: 50, nullable: false),
                    Status = table.Column<string>(type: "TEXT", maxLength: 20, nullable: false),
                    DataSolicitacao = table.Column<DateTime>(type: "TEXT", nullable: false),
                    DataExecucao = table.Column<DateTime>(type: "TEXT", nullable: true),
                    CombustivelConsumido = table.Column<decimal>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Manobras", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Manobras_Operadores_OperadorId",
                        column: x => x.OperadorId,
                        principalTable: "Operadores",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Manobras_Satelites_SateliteId",
                        column: x => x.SateliteId,
                        principalTable: "Satelites",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Operadores",
                columns: new[] { "Id", "DataCriacao", "Email", "NivelAcesso", "Nome" },
                values: new object[,]
                {
                    { 1, new DateTime(2026, 6, 6, 6, 55, 20, 625, DateTimeKind.Utc).AddTicks(1503), "marcus@odin.local", "ADMIN", "Marcus Vinícius" },
                    { 2, new DateTime(2026, 6, 6, 6, 55, 20, 625, DateTimeKind.Utc).AddTicks(1508), "hebert@odin.local", "OPERADOR", "Hebert Lopes" },
                    { 3, new DateTime(2026, 6, 6, 6, 55, 20, 625, DateTimeKind.Utc).AddTicks(1510), "nicolas@odin.local", "OPERADOR", "Nicolas Monteiro" },
                    { 4, new DateTime(2026, 6, 6, 6, 55, 20, 625, DateTimeKind.Utc).AddTicks(1512), "ana@odin.local", "USUARIO", "Ana Silva" },
                    { 5, new DateTime(2026, 6, 6, 6, 55, 20, 625, DateTimeKind.Utc).AddTicks(1514), "carlos@odin.local", "USUARIO", "Carlos Santos" }
                });

            migrationBuilder.InsertData(
                table: "Detritos",
                columns: new[] { "Id", "Altitude", "DataDeteccao", "Identificacao", "Latitude", "Longitude", "NivelRisco", "OperadorId", "Velocidade" },
                values: new object[,]
                {
                    { 1, 36000m, new DateTime(2026, 6, 6, 6, 55, 20, 625, DateTimeKind.Utc).AddTicks(1885), "DEB-001", 0m, 79.5m, 45, 1, 3.07m },
                    { 2, 35786m, new DateTime(2026, 6, 6, 6, 55, 20, 625, DateTimeKind.Utc).AddTicks(1890), "DEB-002", 15.5m, 93.5m, 65, 2, 3.08m },
                    { 3, 800m, new DateTime(2026, 6, 6, 6, 55, 20, 625, DateTimeKind.Utc).AddTicks(1893), "DEB-003", -10.2m, 110.3m, 85, 3, 7.45m },
                    { 4, 400m, new DateTime(2026, 6, 6, 6, 55, 20, 625, DateTimeKind.Utc).AddTicks(1895), "DEB-004", 28.5m, 77.2m, 90, 1, 7.82m },
                    { 5, 600m, new DateTime(2026, 6, 6, 6, 55, 20, 625, DateTimeKind.Utc).AddTicks(1898), "DEB-005", 5.8m, 102.7m, 55, 2, 7.65m }
                });

            migrationBuilder.InsertData(
                table: "Satelites",
                columns: new[] { "Id", "CombustivelAtual", "DataLancamento", "Nome", "OperadorId", "StatusOperacional" },
                values: new object[,]
                {
                    { 1, 500m, new DateTime(2013, 9, 26, 0, 0, 0, 0, DateTimeKind.Unspecified), "INSAT-3D", 1, "ATIVO" },
                    { 2, 450m, new DateTime(2014, 12, 4, 0, 0, 0, 0, DateTimeKind.Unspecified), "CBERS-4", 2, "ATIVO" },
                    { 3, 380m, new DateTime(2021, 2, 28, 0, 0, 0, 0, DateTimeKind.Unspecified), "Amazonia-1", 3, "ATIVO" },
                    { 4, 420m, new DateTime(2017, 5, 9, 0, 0, 0, 0, DateTimeKind.Unspecified), "SGDC-1", 1, "ATIVO" },
                    { 5, 350m, new DateTime(1998, 3, 4, 0, 0, 0, 0, DateTimeKind.Unspecified), "BRASILSAT-B2", 2, "MANUTENCAO" }
                });

            migrationBuilder.InsertData(
                table: "Alertas",
                columns: new[] { "Id", "DataCriacao", "DataResolucao", "DebitoId", "Mensagem", "SateliteId", "Severidade", "Status" },
                values: new object[,]
                {
                    { 1, new DateTime(2026, 6, 5, 6, 55, 20, 625, DateTimeKind.Utc).AddTicks(1995), new DateTime(2026, 6, 6, 6, 55, 20, 625, DateTimeKind.Utc).AddTicks(1996), 1, "Aproximação de detrito detectada", 1, "MEDIA", "RESOLVIDO" },
                    { 2, new DateTime(2026, 6, 6, 6, 55, 20, 625, DateTimeKind.Utc).AddTicks(1999), null, 2, "Risco iminente de colisão", 2, "ALTA", "ATIVO" },
                    { 3, new DateTime(2026, 6, 6, 6, 55, 20, 625, DateTimeKind.Utc).AddTicks(2001), null, 3, "Alerta crítico - ação imediata necessária", 3, "CRITICA", "ATIVO" },
                    { 4, new DateTime(2026, 6, 4, 6, 55, 20, 625, DateTimeKind.Utc).AddTicks(2003), new DateTime(2026, 6, 5, 6, 55, 20, 625, DateTimeKind.Utc).AddTicks(2004), 4, "Trajetória de colisão confirmada", 4, "ALTA", "RESOLVIDO" },
                    { 5, new DateTime(2026, 6, 3, 6, 55, 20, 625, DateTimeKind.Utc).AddTicks(2006), null, 5, "Monitoramento de detrito em progresso", 5, "BAIXA", "ATIVO" }
                });

            migrationBuilder.InsertData(
                table: "Manobras",
                columns: new[] { "Id", "CombustivelConsumido", "DataExecucao", "DataSolicitacao", "OperadorId", "SateliteId", "Status", "Tipo" },
                values: new object[,]
                {
                    { 1, 5m, new DateTime(2026, 6, 6, 6, 55, 20, 625, DateTimeKind.Utc).AddTicks(1943), new DateTime(2026, 6, 5, 6, 55, 20, 625, DateTimeKind.Utc).AddTicks(1937), 1, 1, "EXECUTADA", "DESVIO" },
                    { 2, 3m, new DateTime(2026, 6, 5, 6, 55, 20, 625, DateTimeKind.Utc).AddTicks(1954), new DateTime(2026, 6, 4, 6, 55, 20, 625, DateTimeKind.Utc).AddTicks(1954), 2, 2, "EXECUTADA", "ACELERACAO" },
                    { 3, 0m, null, new DateTime(2026, 6, 6, 6, 55, 20, 625, DateTimeKind.Utc).AddTicks(1957), 3, 3, "PENDENTE", "DESACELERACAO" },
                    { 4, 0m, null, new DateTime(2026, 6, 3, 6, 55, 20, 625, DateTimeKind.Utc).AddTicks(1958), 1, 4, "CANCELADA", "DESVIO" },
                    { 5, 4m, new DateTime(2026, 6, 2, 6, 55, 20, 625, DateTimeKind.Utc).AddTicks(1961), new DateTime(2026, 6, 1, 6, 55, 20, 625, DateTimeKind.Utc).AddTicks(1961), 2, 5, "EXECUTADA", "ACELERACAO" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Alertas_DebitoId",
                table: "Alertas",
                column: "DebitoId");

            migrationBuilder.CreateIndex(
                name: "IX_Alertas_SateliteId",
                table: "Alertas",
                column: "SateliteId");

            migrationBuilder.CreateIndex(
                name: "IX_Detritos_OperadorId",
                table: "Detritos",
                column: "OperadorId");

            migrationBuilder.CreateIndex(
                name: "IX_Manobras_OperadorId",
                table: "Manobras",
                column: "OperadorId");

            migrationBuilder.CreateIndex(
                name: "IX_Manobras_SateliteId",
                table: "Manobras",
                column: "SateliteId");

            migrationBuilder.CreateIndex(
                name: "IX_Satelites_OperadorId",
                table: "Satelites",
                column: "OperadorId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Alertas");

            migrationBuilder.DropTable(
                name: "Manobras");

            migrationBuilder.DropTable(
                name: "Detritos");

            migrationBuilder.DropTable(
                name: "Satelites");

            migrationBuilder.DropTable(
                name: "Operadores");
        }
    }
}
