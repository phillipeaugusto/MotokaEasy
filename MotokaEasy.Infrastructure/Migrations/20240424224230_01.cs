using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace MotokaEasy.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class _01 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Configuracao",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    CodigoParametro = table.Column<int>(type: "int", nullable: false),
                    Descricao = table.Column<string>(type: "varchar(150)", nullable: false),
                    Valor = table.Column<string>(type: "varchar(1000)", nullable: false),
                    Status = table.Column<string>(type: "varchar(1)", nullable: false),
                    DataCriacao = table.Column<DateTime>(type: "timestamp(0) without time zone", nullable: false),
                    DataAlteracao = table.Column<DateTime>(type: "timestamp(0) without time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Configuracao", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Entregador",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Nome = table.Column<string>(type: "varchar(150)", nullable: false),
                    CnpjCpf = table.Column<string>(type: "varchar(14)", nullable: false),
                    DataNascimento = table.Column<DateTime>(type: "timestamp(0) without time zone", nullable: false),
                    NumeroCnh = table.Column<string>(type: "varchar(11)", nullable: false),
                    TipoCnh = table.Column<int>(type: "int", nullable: false),
                    ImagemCnh = table.Column<string>(type: "varchar(1000)", nullable: true),
                    Status = table.Column<string>(type: "varchar(1)", nullable: false),
                    DataCriacao = table.Column<DateTime>(type: "timestamp(0) without time zone", nullable: false),
                    DataAlteracao = table.Column<DateTime>(type: "timestamp(0) without time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Entregador", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ModeloVeiculo",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Descricao = table.Column<string>(type: "varchar(200)", nullable: false),
                    Status = table.Column<string>(type: "varchar(1)", nullable: false),
                    DataCriacao = table.Column<DateTime>(type: "timestamp(0) without time zone", nullable: false),
                    DataAlteracao = table.Column<DateTime>(type: "timestamp(0) without time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ModeloVeiculo", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Plano",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Descricao = table.Column<string>(type: "varchar(200)", nullable: false),
                    QuantidadeDias = table.Column<int>(type: "int", nullable: false),
                    Valor = table.Column<double>(type: "float", nullable: false),
                    Status = table.Column<string>(type: "varchar(1)", nullable: false),
                    DataCriacao = table.Column<DateTime>(type: "timestamp(0) without time zone", nullable: false),
                    DataAlteracao = table.Column<DateTime>(type: "timestamp(0) without time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Plano", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "TipoVeiculo",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Descricao = table.Column<string>(type: "varchar(200)", nullable: false),
                    Status = table.Column<string>(type: "varchar(1)", nullable: false),
                    DataCriacao = table.Column<DateTime>(type: "timestamp(0) without time zone", nullable: false),
                    DataAlteracao = table.Column<DateTime>(type: "timestamp(0) without time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TipoVeiculo", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Usuario",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Nome = table.Column<string>(type: "varchar(200)", nullable: false),
                    Email = table.Column<string>(type: "varchar(200)", nullable: false),
                    Senha = table.Column<string>(type: "varchar(2000)", nullable: false),
                    Numero = table.Column<string>(type: "varchar(200)", nullable: false),
                    Role = table.Column<string>(type: "varchar(50)", nullable: false),
                    Status = table.Column<string>(type: "varchar(1)", nullable: false),
                    DataCriacao = table.Column<DateTime>(type: "timestamp(0) without time zone", nullable: false),
                    DataAlteracao = table.Column<DateTime>(type: "timestamp(0) without time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Usuario", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Veiculo",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Ano = table.Column<int>(type: "int", nullable: false),
                    ModeloVeiculoId = table.Column<Guid>(type: "uuid", nullable: false),
                    Placa = table.Column<string>(type: "varchar(8)", nullable: false),
                    TipoVeiculoId = table.Column<Guid>(type: "uuid", nullable: false),
                    Status = table.Column<string>(type: "varchar(1)", nullable: false),
                    DataCriacao = table.Column<DateTime>(type: "timestamp(0) without time zone", nullable: false),
                    DataAlteracao = table.Column<DateTime>(type: "timestamp(0) without time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Veiculo", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Veiculo_ModeloVeiculo_ModeloVeiculoId",
                        column: x => x.ModeloVeiculoId,
                        principalTable: "ModeloVeiculo",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Veiculo_TipoVeiculo_TipoVeiculoId",
                        column: x => x.TipoVeiculoId,
                        principalTable: "TipoVeiculo",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Locacao",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    EntregadorId = table.Column<Guid>(type: "uuid", nullable: false),
                    PlanoId = table.Column<Guid>(type: "uuid", nullable: false),
                    QuantidadeDeDiasDoPlano = table.Column<int>(type: "int", nullable: false),
                    DataInicio = table.Column<DateTime>(type: "timestamp(0) without time zone", nullable: false),
                    DataTerminio = table.Column<DateTime>(type: "timestamp(0) without time zone", nullable: false),
                    DataPrevisaoTerminio = table.Column<DateTime>(type: "timestamp(0) without time zone", nullable: false),
                    VeiculoId = table.Column<Guid>(type: "uuid", nullable: false),
                    Status = table.Column<string>(type: "varchar(1)", nullable: false),
                    DataCriacao = table.Column<DateTime>(type: "timestamp(0) without time zone", nullable: false),
                    DataAlteracao = table.Column<DateTime>(type: "timestamp(0) without time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Locacao", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Locacao_Entregador_EntregadorId",
                        column: x => x.EntregadorId,
                        principalTable: "Entregador",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Locacao_Plano_PlanoId",
                        column: x => x.PlanoId,
                        principalTable: "Plano",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Locacao_Veiculo_VeiculoId",
                        column: x => x.VeiculoId,
                        principalTable: "Veiculo",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Configuracao",
                columns: new[] { "Id", "CodigoParametro", "DataAlteracao", "DataCriacao", "Descricao", "Status", "Valor" },
                values: new object[,]
                {
                    { new Guid("277c6b9d-ec48-4e0a-ba43-049f2b9829f5"), 2, new DateTime(2024, 4, 24, 19, 42, 30, 797, DateTimeKind.Local).AddTicks(6060), new DateTime(2024, 4, 24, 19, 42, 30, 797, DateTimeKind.Local).AddTicks(6060), "Guid do Plano de 15 dias multa de 40% sobre o valor das diárias não efetivadas", "A", "ecef67ef-8b5b-4dfe-a9d2-5a2989c4503f" },
                    { new Guid("dc07a66a-e8ea-410b-950b-9a403a8d0805"), 1, new DateTime(2024, 4, 24, 19, 42, 30, 797, DateTimeKind.Local).AddTicks(1940), new DateTime(2024, 4, 24, 19, 42, 30, 797, DateTimeKind.Local).AddTicks(1940), "Guid do Plano de 7 dias multa de 20% sobre o valor das diárias não efetivadas", "A", "01884af8-8e90-4401-906d-293f7c8a3f72" }
                });

            migrationBuilder.InsertData(
                table: "ModeloVeiculo",
                columns: new[] { "Id", "DataAlteracao", "DataCriacao", "Descricao", "Status" },
                values: new object[,]
                {
                    { new Guid("44fe2ff1-118c-48e8-8467-2acbae72b1db"), new DateTime(2024, 4, 24, 19, 42, 30, 795, DateTimeKind.Local).AddTicks(4860), new DateTime(2024, 4, 24, 19, 42, 30, 795, DateTimeKind.Local).AddTicks(4860), "Honda CRF 250", "A" },
                    { new Guid("51734813-971a-4729-bae5-f770bf4442c3"), new DateTime(2024, 4, 24, 19, 42, 30, 795, DateTimeKind.Local).AddTicks(7500), new DateTime(2024, 4, 24, 19, 42, 30, 795, DateTimeKind.Local).AddTicks(7500), "Yamaha Fazer", "A" },
                    { new Guid("84276269-34c9-4ea0-96fc-7e30eb975cb4"), new DateTime(2024, 4, 24, 19, 42, 30, 795, DateTimeKind.Local).AddTicks(2210), new DateTime(2024, 4, 24, 19, 42, 30, 795, DateTimeKind.Local).AddTicks(2210), "Honda Nxr Bros 160", "A" },
                    { new Guid("89ce3ddc-8198-4135-8f8c-07eaa9f891dd"), new DateTime(2024, 4, 24, 19, 42, 30, 794, DateTimeKind.Local).AddTicks(9410), new DateTime(2024, 4, 24, 19, 42, 30, 794, DateTimeKind.Local).AddTicks(9410), "Honda Pop 100", "A" },
                    { new Guid("df1ab9f8-82cf-483c-8488-21fe4ab2c569"), new DateTime(2024, 4, 24, 19, 42, 30, 796, DateTimeKind.Local).AddTicks(120), new DateTime(2024, 4, 24, 19, 42, 30, 796, DateTimeKind.Local).AddTicks(120), "Yamaha TTR", "A" }
                });

            migrationBuilder.InsertData(
                table: "Plano",
                columns: new[] { "Id", "DataAlteracao", "DataCriacao", "Descricao", "QuantidadeDias", "Status", "Valor" },
                values: new object[,]
                {
                    { new Guid("01884af8-8e90-4401-906d-293f7c8a3f72"), new DateTime(2024, 4, 24, 19, 42, 30, 791, DateTimeKind.Local).AddTicks(360), new DateTime(2024, 4, 24, 19, 42, 30, 791, DateTimeKind.Local).AddTicks(330), "7 dias", 7, "A", 30.0 },
                    { new Guid("50ba38a0-7de9-4438-894d-74c7cbfed8e4"), new DateTime(2024, 4, 24, 19, 42, 30, 793, DateTimeKind.Local).AddTicks(7540), new DateTime(2024, 4, 24, 19, 42, 30, 793, DateTimeKind.Local).AddTicks(7530), "50 dias", 50, "A", 18.0 },
                    { new Guid("5aa095f8-154b-4e1f-b344-a0f89709defa"), new DateTime(2024, 4, 24, 19, 42, 30, 792, DateTimeKind.Local).AddTicks(4800), new DateTime(2024, 4, 24, 19, 42, 30, 792, DateTimeKind.Local).AddTicks(4800), "30 dias", 30, "A", 22.0 },
                    { new Guid("d8d49646-2a2b-4142-8f4d-cd71f9b61d50"), new DateTime(2024, 4, 24, 19, 42, 30, 793, DateTimeKind.Local).AddTicks(1170), new DateTime(2024, 4, 24, 19, 42, 30, 793, DateTimeKind.Local).AddTicks(1170), "45 dias", 45, "A", 20.0 },
                    { new Guid("ecef67ef-8b5b-4dfe-a9d2-5a2989c4503f"), new DateTime(2024, 4, 24, 19, 42, 30, 791, DateTimeKind.Local).AddTicks(8180), new DateTime(2024, 4, 24, 19, 42, 30, 791, DateTimeKind.Local).AddTicks(8180), "15 dias", 15, "A", 28.0 }
                });

            migrationBuilder.InsertData(
                table: "TipoVeiculo",
                columns: new[] { "Id", "DataAlteracao", "DataCriacao", "Descricao", "Status" },
                values: new object[,]
                {
                    { new Guid("1b7fbf89-4e93-4611-b7da-7d28673709b3"), new DateTime(2024, 4, 24, 19, 42, 30, 794, DateTimeKind.Local).AddTicks(3950), new DateTime(2024, 4, 24, 19, 42, 30, 794, DateTimeKind.Local).AddTicks(3940), "Moto", "A" },
                    { new Guid("3a575e43-63d1-4052-b351-2f4b222e0690"), new DateTime(2024, 4, 24, 19, 42, 30, 794, DateTimeKind.Local).AddTicks(6710), new DateTime(2024, 4, 24, 19, 42, 30, 794, DateTimeKind.Local).AddTicks(6710), "Carro", "A" }
                });

            migrationBuilder.InsertData(
                table: "Usuario",
                columns: new[] { "Id", "DataAlteracao", "DataCriacao", "Email", "Nome", "Numero", "Role", "Senha", "Status" },
                values: new object[,]
                {
                    { new Guid("1f4a4ce9-be3e-455f-b674-b6011d1960c6"), new DateTime(2024, 4, 24, 19, 42, 30, 796, DateTimeKind.Local).AddTicks(5860), new DateTime(2024, 4, 24, 19, 42, 30, 796, DateTimeKind.Local).AddTicks(5860), "adm@motokaeasy.com", "UsuárioADM", "9999999998", "adm", "e0bc60c82713f64ef8a57c0c40d02ce24fd0141d5cc3086259c19b1e62a62bea", "A" },
                    { new Guid("33176d9b-eb69-4b43-a497-e2c7ecb4f354"), new DateTime(2024, 4, 24, 19, 42, 30, 796, DateTimeKind.Local).AddTicks(2780), new DateTime(2024, 4, 24, 19, 42, 30, 796, DateTimeKind.Local).AddTicks(2780), "useradm@motokaeasy.com", "UsuárioAdmUser", "9999999997", "adm,user", "e0bc60c82713f64ef8a57c0c40d02ce24fd0141d5cc3086259c19b1e62a62bea", "A" },
                    { new Guid("a9e5d5cf-0c51-4534-b792-7284299cd8fc"), new DateTime(2024, 4, 24, 19, 42, 30, 796, DateTimeKind.Local).AddTicks(8880), new DateTime(2024, 4, 24, 19, 42, 30, 796, DateTimeKind.Local).AddTicks(8880), "user@motokaeasy.com", "UsuárioNormal", "9999999999", "user", "e0bc60c82713f64ef8a57c0c40d02ce24fd0141d5cc3086259c19b1e62a62bea", "A" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Locacao_EntregadorId",
                table: "Locacao",
                column: "EntregadorId");

            migrationBuilder.CreateIndex(
                name: "IX_Locacao_PlanoId",
                table: "Locacao",
                column: "PlanoId");

            migrationBuilder.CreateIndex(
                name: "IX_Locacao_VeiculoId",
                table: "Locacao",
                column: "VeiculoId");

            migrationBuilder.CreateIndex(
                name: "IX_Veiculo_ModeloVeiculoId",
                table: "Veiculo",
                column: "ModeloVeiculoId");

            migrationBuilder.CreateIndex(
                name: "IX_Veiculo_TipoVeiculoId",
                table: "Veiculo",
                column: "TipoVeiculoId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Configuracao");

            migrationBuilder.DropTable(
                name: "Locacao");

            migrationBuilder.DropTable(
                name: "Usuario");

            migrationBuilder.DropTable(
                name: "Entregador");

            migrationBuilder.DropTable(
                name: "Plano");

            migrationBuilder.DropTable(
                name: "Veiculo");

            migrationBuilder.DropTable(
                name: "ModeloVeiculo");

            migrationBuilder.DropTable(
                name: "TipoVeiculo");
        }
    }
}
