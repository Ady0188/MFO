using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace MFO.Infrastructure.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class FullContext : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<decimal>(
                name: "InterestRate",
                table: "Loans",
                type: "numeric(7,4)",
                precision: 7,
                scale: 4,
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "numeric(5,2)",
                oldPrecision: 5,
                oldScale: 2);

            migrationBuilder.AddColumn<DateOnly>(
                name: "ApprovedOn",
                table: "Loans",
                type: "date",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "BranchId",
                table: "Loans",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedAt",
                table: "Loans",
                type: "timestamp with time zone",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<Guid>(
                name: "CuratorId",
                table: "Loans",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<Guid>(
                name: "CurrencyId",
                table: "Loans",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<DateOnly>(
                name: "DisbursedOn",
                table: "Loans",
                type: "date",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "DisbursementMethodId",
                table: "Loans",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<decimal>(
                name: "FeesAmount",
                table: "Loans",
                type: "numeric(18,2)",
                precision: 18,
                scale: 2,
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<string>(
                name: "LoanNumber",
                table: "Loans",
                type: "character varying(50)",
                maxLength: 50,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<DateOnly>(
                name: "MaturityOn",
                table: "Loans",
                type: "date",
                nullable: true);

            migrationBuilder.AddColumn<decimal>(
                name: "OutstandingInterest",
                table: "Loans",
                type: "numeric(18,2)",
                precision: 18,
                scale: 2,
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<decimal>(
                name: "OutstandingPrincipal",
                table: "Loans",
                type: "numeric(18,2)",
                precision: 18,
                scale: 2,
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<Guid>(
                name: "PaymentFrequencyId",
                table: "Loans",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<Guid>(
                name: "PenaltyPolicyId",
                table: "Loans",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<decimal>(
                name: "PenaltyRate",
                table: "Loans",
                type: "numeric(7,4)",
                precision: 7,
                scale: 4,
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<Guid>(
                name: "ProductId",
                table: "Loans",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<Guid>(
                name: "PurposeId",
                table: "Loans",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<Guid>(
                name: "RepaymentMethodId",
                table: "Loans",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<Guid>(
                name: "StatusId",
                table: "Loans",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<decimal>(
                name: "TotalPayable",
                table: "Loans",
                type: "numeric(18,2)",
                precision: 18,
                scale: 2,
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<DateTime>(
                name: "UpdatedAt",
                table: "Loans",
                type: "timestamp with time zone",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "CustomerTypeId",
                table: "Customers",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<Guid>(
                name: "StatusId",
                table: "Customers",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.CreateTable(
                name: "Branches",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Code = table.Column<string>(type: "character varying(10)", maxLength: 10, nullable: false),
                    Name = table.Column<string>(type: "character varying(150)", maxLength: 150, nullable: false),
                    IsActive = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Branches", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Currencies",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Code = table.Column<string>(type: "character varying(3)", maxLength: 3, nullable: false),
                    NumericCode = table.Column<string>(type: "character varying(3)", maxLength: 3, nullable: false),
                    Name = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    Symbol = table.Column<string>(type: "character varying(10)", maxLength: 10, nullable: false),
                    IsActive = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Currencies", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "CustomerStatuses",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Code = table.Column<string>(type: "character varying(30)", maxLength: 30, nullable: false),
                    Name = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    IsActive = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CustomerStatuses", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "CustomerTypes",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Code = table.Column<string>(type: "character varying(30)", maxLength: 30, nullable: false),
                    Name = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    IsActive = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CustomerTypes", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "DisbursementMethods",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Code = table.Column<string>(type: "character varying(30)", maxLength: 30, nullable: false),
                    Name = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    IsActive = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DisbursementMethods", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "LoanAccounts",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    LoanId = table.Column<Guid>(type: "uuid", nullable: false),
                    AccountNumber = table.Column<string>(type: "character varying(30)", maxLength: 30, nullable: false),
                    Balance = table.Column<decimal>(type: "numeric(18,2)", precision: 18, scale: 2, nullable: false),
                    OpenedOn = table.Column<DateOnly>(type: "date", nullable: false),
                    ClosedOn = table.Column<DateOnly>(type: "date", nullable: true),
                    IsActive = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LoanAccounts", x => x.Id);
                    table.ForeignKey(
                        name: "FK_LoanAccounts_Loans_LoanId",
                        column: x => x.LoanId,
                        principalTable: "Loans",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "LoanStatuses",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Code = table.Column<string>(type: "character varying(30)", maxLength: 30, nullable: false),
                    Name = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    IsClosed = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LoanStatuses", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "LoanTransactionTypes",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Code = table.Column<string>(type: "character varying(30)", maxLength: 30, nullable: false),
                    Name = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    IsActive = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LoanTransactionTypes", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "PaymentFrequencies",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Code = table.Column<string>(type: "character varying(30)", maxLength: 30, nullable: false),
                    Name = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    IntervalDays = table.Column<int>(type: "integer", nullable: false),
                    IsActive = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PaymentFrequencies", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "PenaltyPolicies",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Code = table.Column<string>(type: "character varying(30)", maxLength: 30, nullable: false),
                    Name = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    PenaltyRate = table.Column<decimal>(type: "numeric(7,4)", precision: 7, scale: 4, nullable: false),
                    FixedFee = table.Column<decimal>(type: "numeric(18,2)", precision: 18, scale: 2, nullable: false),
                    IsActive = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PenaltyPolicies", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Purposes",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Code = table.Column<string>(type: "character varying(30)", maxLength: 30, nullable: false),
                    Name = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    IsActive = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Purposes", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "RepaymentMethods",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Code = table.Column<string>(type: "character varying(30)", maxLength: 30, nullable: false),
                    Name = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    IsActive = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RepaymentMethods", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Employees",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    FullName = table.Column<string>(type: "character varying(200)", maxLength: 200, nullable: false),
                    BranchId = table.Column<Guid>(type: "uuid", nullable: false),
                    IsActive = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Employees", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Employees_Branches_BranchId",
                        column: x => x.BranchId,
                        principalTable: "Branches",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "LoanTransactions",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    LoanId = table.Column<Guid>(type: "uuid", nullable: false),
                    LoanAccountId = table.Column<Guid>(type: "uuid", nullable: false),
                    TransactionTypeId = table.Column<Guid>(type: "uuid", nullable: false),
                    Amount = table.Column<decimal>(type: "numeric(18,2)", precision: 18, scale: 2, nullable: false),
                    OccurredOn = table.Column<DateOnly>(type: "date", nullable: false),
                    Description = table.Column<string>(type: "character varying(500)", maxLength: 500, nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LoanTransactions", x => x.Id);
                    table.ForeignKey(
                        name: "FK_LoanTransactions_LoanAccounts_LoanAccountId",
                        column: x => x.LoanAccountId,
                        principalTable: "LoanAccounts",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_LoanTransactions_LoanTransactionTypes_TransactionTypeId",
                        column: x => x.TransactionTypeId,
                        principalTable: "LoanTransactionTypes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_LoanTransactions_Loans_LoanId",
                        column: x => x.LoanId,
                        principalTable: "Loans",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "LoanProducts",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Code = table.Column<string>(type: "character varying(30)", maxLength: 30, nullable: false),
                    Name = table.Column<string>(type: "character varying(150)", maxLength: 150, nullable: false),
                    InterestRate = table.Column<decimal>(type: "numeric(7,4)", precision: 7, scale: 4, nullable: false),
                    OriginationFee = table.Column<decimal>(type: "numeric(18,2)", precision: 18, scale: 2, nullable: false),
                    MinAmount = table.Column<decimal>(type: "numeric(18,2)", precision: 18, scale: 2, nullable: false),
                    MaxAmount = table.Column<decimal>(type: "numeric(18,2)", precision: 18, scale: 2, nullable: false),
                    MinTermMonths = table.Column<int>(type: "integer", nullable: false),
                    MaxTermMonths = table.Column<int>(type: "integer", nullable: false),
                    CurrencyId = table.Column<Guid>(type: "uuid", nullable: false),
                    PaymentFrequencyId = table.Column<Guid>(type: "uuid", nullable: false),
                    PenaltyPolicyId = table.Column<Guid>(type: "uuid", nullable: false),
                    IsActive = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LoanProducts", x => x.Id);
                    table.ForeignKey(
                        name: "FK_LoanProducts_Currencies_CurrencyId",
                        column: x => x.CurrencyId,
                        principalTable: "Currencies",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_LoanProducts_PaymentFrequencies_PaymentFrequencyId",
                        column: x => x.PaymentFrequencyId,
                        principalTable: "PaymentFrequencies",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_LoanProducts_PenaltyPolicies_PenaltyPolicyId",
                        column: x => x.PenaltyPolicyId,
                        principalTable: "PenaltyPolicies",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Branches",
                columns: new[] { "Id", "Code", "IsActive", "Name" },
                values: new object[,]
                {
                    { new Guid("0d2d0d7d-7317-4f0e-a62a-6e7f8a3f7f3c"), "002", true, "Филиал 2" },
                    { new Guid("a22c6f48-1d5c-4d03-86b8-1f2f9b3a3e14"), "001", true, "Головной офис" }
                });

            migrationBuilder.InsertData(
                table: "Currencies",
                columns: new[] { "Id", "Code", "IsActive", "Name", "NumericCode", "Symbol" },
                values: new object[,]
                {
                    { new Guid("1c2d3e4f-5a6b-7c8d-9e0f-1a2b3c4d5e6f"), "RUB", true, "Российский рубль", "643", "RUB" },
                    { new Guid("5d2c7a1f-8b9e-4c2a-9f1d-2b3c4d5e6f70"), "TJS", true, "Таджикский сомони", "972", "TJS" },
                    { new Guid("7a3d2f1c-9b4e-4a1b-8d2c-6f1a9d3b2c4d"), "USD", true, "Доллар США", "840", "USD" },
                    { new Guid("b2c3d4e5-f6a7-48b9-0c1d-2e3f4a5b6c7d"), "EUR", true, "Евро", "978", "EUR" }
                });

            migrationBuilder.InsertData(
                table: "CustomerStatuses",
                columns: new[] { "Id", "Code", "IsActive", "Name" },
                values: new object[,]
                {
                    { new Guid("4a2f9f2b-7f9f-41c4-9f3b-0c3d24b7c2a1"), "ACTIVE", true, "Активный" },
                    { new Guid("9d2f7a6c-221b-4e8a-8d1f-8af2d5a3a7a7"), "INACTIVE", false, "Неактивный" },
                    { new Guid("f1a7c4a9-5f1c-4e2a-9b4d-3f2a1d6c8e7a"), "BLACKLISTED", false, "В черном списке" }
                });

            migrationBuilder.InsertData(
                table: "CustomerTypes",
                columns: new[] { "Id", "Code", "IsActive", "Name" },
                values: new object[,]
                {
                    { new Guid("7c6b2b1f-8a0c-4f2b-9a5b-4b1f7c2b5d3a"), "INDIVIDUAL", true, "Физическое лицо" },
                    { new Guid("d1a9b5c3-2f4e-4b8d-8c2e-5f3a1b7c9d0e"), "BUSINESS", true, "Юридическое лицо" }
                });

            migrationBuilder.InsertData(
                table: "DisbursementMethods",
                columns: new[] { "Id", "Code", "IsActive", "Name" },
                values: new object[,]
                {
                    { new Guid("9a0b1c2d-3e4f-45a6-b7c8-d9e0f1a2b3c4"), "CASH", true, "Наличные" },
                    { new Guid("c4b3a2f1-0e9d-48c7-b6a5-4f3e2d1c0b9a"), "CARD", true, "Карта" },
                    { new Guid("f0e9d8c7-b6a5-44f3-a2b1-c0d9e8f7a6b5"), "BANK_TRANSFER", true, "Банковский перевод" }
                });

            migrationBuilder.InsertData(
                table: "LoanStatuses",
                columns: new[] { "Id", "Code", "IsClosed", "Name" },
                values: new object[,]
                {
                    { new Guid("14cf2ac0-3d2d-4f35-9b64-8d9ef0d2a343"), "DISBURSED", false, "Выдан" },
                    { new Guid("2eb7d12d-1a2c-4e2d-8bbf-ff18c89c41f3"), "ACTIVE", false, "Работает" },
                    { new Guid("6b5a3f41-2f04-4eb6-a4e8-1c4f55b2a6f1"), "DRAFT", false, "Черновик" },
                    { new Guid("6b8949d8-1f89-4d2b-9d39-6d3b0e2d9fd2"), "WRITTEN_OFF", true, "Списан" },
                    { new Guid("6e0f0f4e-6d4b-4d18-9ed9-5d6a6f2bc71f"), "PROLONGED", false, "Пролонгирован" },
                    { new Guid("8b9f77f8-9f0d-4f5c-9a49-3d1dfc55f9d7"), "APPROVED", false, "Одобрен" },
                    { new Guid("b93efb6a-1249-4b5b-9a4c-5c7a3bb4d3ae"), "RESTRUCTURED", false, "Реструктурирован" },
                    { new Guid("c72b1c34-2b5d-45ad-9c3c-1c8b6d9c0f2a"), "PENDING", false, "В ожидании" },
                    { new Guid("d2a3547d-0b6c-4c17-9f73-7d8f2f4d6a1e"), "CLOSED", true, "Закрыт" },
                    { new Guid("f2e57b5b-f2be-4fb7-9a4e-4bb2b57c6a2a"), "OVERDUE", false, "Просрочен" },
                    { new Guid("f7c6ec3f-5b18-4fd1-9bb1-5f0c7c8d2a91"), "CANCELLED", true, "Отменен" }
                });

            migrationBuilder.InsertData(
                table: "LoanTransactionTypes",
                columns: new[] { "Id", "Code", "IsActive", "Name" },
                values: new object[,]
                {
                    { new Guid("7e2a4d8d-49f1-4f0b-9c2d-7b1f8f6a1e2c"), "ACCOUNT_TOPUP", true, "Пополнение счета" },
                    { new Guid("8b72e8f5-51a6-4c72-95c2-4ad5f6a7d8f1"), "REPAYMENT", true, "Погашение кредита" },
                    { new Guid("a1c4d7e9-3b5f-4b2c-9a3e-2d6f8b1c7e0a"), "RESTRUCTURE", true, "Реструктуризация" },
                    { new Guid("b2d5e8f1-4c6a-4d3b-8e1f-3c7a9d2e6f0b"), "PROLONGATION", true, "Пролонгация" },
                    { new Guid("bf7b7a28-0e9a-4b5d-b9b2-2f4f6a6c0e3b"), "DISBURSEMENT", true, "Выдача кредита" },
                    { new Guid("c3e6f9a2-5d7b-4e4c-9f2a-4d8b0e3f7a1c"), "PENALTY", true, "Штраф" },
                    { new Guid("d4f7a0b3-6e8c-4f5d-8a3b-5e9c1f4a8b2d"), "WRITE_OFF", true, "Списание" }
                });

            migrationBuilder.InsertData(
                table: "PaymentFrequencies",
                columns: new[] { "Id", "Code", "IntervalDays", "IsActive", "Name" },
                values: new object[,]
                {
                    { new Guid("12b4c5d6-7e8f-49a0-b1c2-3d4e5f6a7b8c"), "WEEKLY", 7, true, "Еженедельно" },
                    { new Guid("a1b2c3d4-e5f6-47a8-9b0c-1d2e3f4a5b6c"), "BIWEEKLY", 14, true, "Раз в две недели" },
                    { new Guid("b7c8d9e0-f1a2-43b4-85c6-d7e8f9a0b1c2"), "MONTHLY", 30, true, "Ежемесячно" }
                });

            migrationBuilder.InsertData(
                table: "PenaltyPolicies",
                columns: new[] { "Id", "Code", "FixedFee", "IsActive", "Name", "PenaltyRate" },
                values: new object[,]
                {
                    { new Guid("5f6a7b8c-9d0e-41f2-3a4b-5c6d7e8f9a0b"), "STANDARD", 0m, true, "Стандартный", 0.5m },
                    { new Guid("6a7b8c9d-0e1f-42a3-4b5c-6d7e8f9a0b1c"), "STRICT", 5m, true, "Строгий", 1.0m },
                    { new Guid("7b8c9d0e-1f2a-43b4-5c6d-7e8f9a0b1c2d"), "SOFT", 0m, true, "Мягкий", 0.2m }
                });

            migrationBuilder.InsertData(
                table: "Purposes",
                columns: new[] { "Id", "Code", "IsActive", "Name" },
                values: new object[,]
                {
                    { new Guid("0a1b2c3d-4e5f-46a7-8b9c-0d1e2f3a4b5c"), "PERSONAL", true, "Личные нужды" },
                    { new Guid("1b2c3d4e-5f6a-47b8-9c0d-1e2f3a4b5c6d"), "BUSINESS", true, "Бизнес" },
                    { new Guid("2c3d4e5f-6a7b-48c9-0d1e-2f3a4b5c6d7e"), "EDUCATION", true, "Образование" },
                    { new Guid("3d4e5f6a-7b8c-49d0-1e2f-3a4b5c6d7e8f"), "MEDICAL", true, "Медицина" },
                    { new Guid("4e5f6a7b-8c9d-40e1-2f3a-4b5c6d7e8f9a"), "OTHER", true, "Другое" }
                });

            migrationBuilder.InsertData(
                table: "RepaymentMethods",
                columns: new[] { "Id", "Code", "IsActive", "Name" },
                values: new object[,]
                {
                    { new Guid("3f2e1d0c-9b8a-47c6-b5a4-3f2e1d0c9b8a"), "CASH", true, "Наличные" },
                    { new Guid("4c5b6a7d-8e9f-40a1-b2c3-d4e5f6a7b8c9"), "BANK_TRANSFER", true, "Банковский перевод" },
                    { new Guid("8a9b0c1d-2e3f-4a5b-8c9d-0e1f2a3b4c5d"), "CARD", true, "Карта" },
                    { new Guid("e1f2a3b4-c5d6-47e8-9f0a-1b2c3d4e5f6a"), "ONLINE", true, "Онлайн" }
                });

            migrationBuilder.InsertData(
                table: "Employees",
                columns: new[] { "Id", "BranchId", "FullName", "IsActive" },
                values: new object[,]
                {
                    { new Guid("c9c6dfd0-93b4-4d0e-86fa-3e7a4f6e5f2f"), new Guid("a22c6f48-1d5c-4d03-86b8-1f2f9b3a3e14"), "Куратор", true },
                    { new Guid("e6a1f9a5-0a5b-4a4c-9d22-8a1f78e3e81f"), new Guid("a22c6f48-1d5c-4d03-86b8-1f2f9b3a3e14"), "Администратор", true }
                });

            migrationBuilder.InsertData(
                table: "LoanProducts",
                columns: new[] { "Id", "Code", "CurrencyId", "InterestRate", "IsActive", "MaxAmount", "MaxTermMonths", "MinAmount", "MinTermMonths", "Name", "OriginationFee", "PaymentFrequencyId", "PenaltyPolicyId" },
                values: new object[,]
                {
                    { new Guid("8c9d0e1f-2a3b-44c5-6d7e-8f9a0b1c2d3e"), "SHORT_TERM", new Guid("7a3d2f1c-9b4e-4a1b-8d2c-6f1a9d3b2c4d"), 2.5m, true, 500m, 3, 50m, 1, "Краткосрочный кредит", 5m, new Guid("12b4c5d6-7e8f-49a0-b1c2-3d4e5f6a7b8c"), new Guid("5f6a7b8c-9d0e-41f2-3a4b-5c6d7e8f9a0b") },
                    { new Guid("9d0e1f2a-3b4c-45d6-7e8f-9a0b1c2d3e4f"), "LONG_TERM", new Guid("7a3d2f1c-9b4e-4a1b-8d2c-6f1a9d3b2c4d"), 1.5m, true, 2000m, 24, 300m, 6, "Долгосрочный кредит", 10m, new Guid("b7c8d9e0-f1a2-43b4-85c6-d7e8f9a0b1c2"), new Guid("6a7b8c9d-0e1f-42a3-4b5c-6d7e8f9a0b1c") }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Loans_BranchId",
                table: "Loans",
                column: "BranchId");

            migrationBuilder.CreateIndex(
                name: "IX_Loans_CuratorId",
                table: "Loans",
                column: "CuratorId");

            migrationBuilder.CreateIndex(
                name: "IX_Loans_CurrencyId",
                table: "Loans",
                column: "CurrencyId");

            migrationBuilder.CreateIndex(
                name: "IX_Loans_DisbursementMethodId",
                table: "Loans",
                column: "DisbursementMethodId");

            migrationBuilder.CreateIndex(
                name: "IX_Loans_PaymentFrequencyId",
                table: "Loans",
                column: "PaymentFrequencyId");

            migrationBuilder.CreateIndex(
                name: "IX_Loans_PenaltyPolicyId",
                table: "Loans",
                column: "PenaltyPolicyId");

            migrationBuilder.CreateIndex(
                name: "IX_Loans_ProductId",
                table: "Loans",
                column: "ProductId");

            migrationBuilder.CreateIndex(
                name: "IX_Loans_PurposeId",
                table: "Loans",
                column: "PurposeId");

            migrationBuilder.CreateIndex(
                name: "IX_Loans_RepaymentMethodId",
                table: "Loans",
                column: "RepaymentMethodId");

            migrationBuilder.CreateIndex(
                name: "IX_Loans_StatusId",
                table: "Loans",
                column: "StatusId");

            migrationBuilder.CreateIndex(
                name: "IX_Customers_CustomerTypeId",
                table: "Customers",
                column: "CustomerTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_Customers_StatusId",
                table: "Customers",
                column: "StatusId");

            migrationBuilder.CreateIndex(
                name: "IX_Employees_BranchId",
                table: "Employees",
                column: "BranchId");

            migrationBuilder.CreateIndex(
                name: "IX_LoanAccounts_LoanId",
                table: "LoanAccounts",
                column: "LoanId");

            migrationBuilder.CreateIndex(
                name: "IX_LoanProducts_CurrencyId",
                table: "LoanProducts",
                column: "CurrencyId");

            migrationBuilder.CreateIndex(
                name: "IX_LoanProducts_PaymentFrequencyId",
                table: "LoanProducts",
                column: "PaymentFrequencyId");

            migrationBuilder.CreateIndex(
                name: "IX_LoanProducts_PenaltyPolicyId",
                table: "LoanProducts",
                column: "PenaltyPolicyId");

            migrationBuilder.CreateIndex(
                name: "IX_LoanTransactions_LoanAccountId",
                table: "LoanTransactions",
                column: "LoanAccountId");

            migrationBuilder.CreateIndex(
                name: "IX_LoanTransactions_LoanId",
                table: "LoanTransactions",
                column: "LoanId");

            migrationBuilder.CreateIndex(
                name: "IX_LoanTransactions_TransactionTypeId",
                table: "LoanTransactions",
                column: "TransactionTypeId");

            migrationBuilder.AddForeignKey(
                name: "FK_Customers_CustomerStatuses_StatusId",
                table: "Customers",
                column: "StatusId",
                principalTable: "CustomerStatuses",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Customers_CustomerTypes_CustomerTypeId",
                table: "Customers",
                column: "CustomerTypeId",
                principalTable: "CustomerTypes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Loans_Branches_BranchId",
                table: "Loans",
                column: "BranchId",
                principalTable: "Branches",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Loans_Currencies_CurrencyId",
                table: "Loans",
                column: "CurrencyId",
                principalTable: "Currencies",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Loans_DisbursementMethods_DisbursementMethodId",
                table: "Loans",
                column: "DisbursementMethodId",
                principalTable: "DisbursementMethods",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Loans_Employees_CuratorId",
                table: "Loans",
                column: "CuratorId",
                principalTable: "Employees",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Loans_LoanProducts_ProductId",
                table: "Loans",
                column: "ProductId",
                principalTable: "LoanProducts",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Loans_LoanStatuses_StatusId",
                table: "Loans",
                column: "StatusId",
                principalTable: "LoanStatuses",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Loans_PaymentFrequencies_PaymentFrequencyId",
                table: "Loans",
                column: "PaymentFrequencyId",
                principalTable: "PaymentFrequencies",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Loans_PenaltyPolicies_PenaltyPolicyId",
                table: "Loans",
                column: "PenaltyPolicyId",
                principalTable: "PenaltyPolicies",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Loans_Purposes_PurposeId",
                table: "Loans",
                column: "PurposeId",
                principalTable: "Purposes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Loans_RepaymentMethods_RepaymentMethodId",
                table: "Loans",
                column: "RepaymentMethodId",
                principalTable: "RepaymentMethods",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Customers_CustomerStatuses_StatusId",
                table: "Customers");

            migrationBuilder.DropForeignKey(
                name: "FK_Customers_CustomerTypes_CustomerTypeId",
                table: "Customers");

            migrationBuilder.DropForeignKey(
                name: "FK_Loans_Branches_BranchId",
                table: "Loans");

            migrationBuilder.DropForeignKey(
                name: "FK_Loans_Currencies_CurrencyId",
                table: "Loans");

            migrationBuilder.DropForeignKey(
                name: "FK_Loans_DisbursementMethods_DisbursementMethodId",
                table: "Loans");

            migrationBuilder.DropForeignKey(
                name: "FK_Loans_Employees_CuratorId",
                table: "Loans");

            migrationBuilder.DropForeignKey(
                name: "FK_Loans_LoanProducts_ProductId",
                table: "Loans");

            migrationBuilder.DropForeignKey(
                name: "FK_Loans_LoanStatuses_StatusId",
                table: "Loans");

            migrationBuilder.DropForeignKey(
                name: "FK_Loans_PaymentFrequencies_PaymentFrequencyId",
                table: "Loans");

            migrationBuilder.DropForeignKey(
                name: "FK_Loans_PenaltyPolicies_PenaltyPolicyId",
                table: "Loans");

            migrationBuilder.DropForeignKey(
                name: "FK_Loans_Purposes_PurposeId",
                table: "Loans");

            migrationBuilder.DropForeignKey(
                name: "FK_Loans_RepaymentMethods_RepaymentMethodId",
                table: "Loans");

            migrationBuilder.DropTable(
                name: "CustomerStatuses");

            migrationBuilder.DropTable(
                name: "CustomerTypes");

            migrationBuilder.DropTable(
                name: "DisbursementMethods");

            migrationBuilder.DropTable(
                name: "Employees");

            migrationBuilder.DropTable(
                name: "LoanProducts");

            migrationBuilder.DropTable(
                name: "LoanStatuses");

            migrationBuilder.DropTable(
                name: "LoanTransactions");

            migrationBuilder.DropTable(
                name: "Purposes");

            migrationBuilder.DropTable(
                name: "RepaymentMethods");

            migrationBuilder.DropTable(
                name: "Branches");

            migrationBuilder.DropTable(
                name: "Currencies");

            migrationBuilder.DropTable(
                name: "PaymentFrequencies");

            migrationBuilder.DropTable(
                name: "PenaltyPolicies");

            migrationBuilder.DropTable(
                name: "LoanAccounts");

            migrationBuilder.DropTable(
                name: "LoanTransactionTypes");

            migrationBuilder.DropIndex(
                name: "IX_Loans_BranchId",
                table: "Loans");

            migrationBuilder.DropIndex(
                name: "IX_Loans_CuratorId",
                table: "Loans");

            migrationBuilder.DropIndex(
                name: "IX_Loans_CurrencyId",
                table: "Loans");

            migrationBuilder.DropIndex(
                name: "IX_Loans_DisbursementMethodId",
                table: "Loans");

            migrationBuilder.DropIndex(
                name: "IX_Loans_PaymentFrequencyId",
                table: "Loans");

            migrationBuilder.DropIndex(
                name: "IX_Loans_PenaltyPolicyId",
                table: "Loans");

            migrationBuilder.DropIndex(
                name: "IX_Loans_ProductId",
                table: "Loans");

            migrationBuilder.DropIndex(
                name: "IX_Loans_PurposeId",
                table: "Loans");

            migrationBuilder.DropIndex(
                name: "IX_Loans_RepaymentMethodId",
                table: "Loans");

            migrationBuilder.DropIndex(
                name: "IX_Loans_StatusId",
                table: "Loans");

            migrationBuilder.DropIndex(
                name: "IX_Customers_CustomerTypeId",
                table: "Customers");

            migrationBuilder.DropIndex(
                name: "IX_Customers_StatusId",
                table: "Customers");

            migrationBuilder.DropColumn(
                name: "ApprovedOn",
                table: "Loans");

            migrationBuilder.DropColumn(
                name: "BranchId",
                table: "Loans");

            migrationBuilder.DropColumn(
                name: "CreatedAt",
                table: "Loans");

            migrationBuilder.DropColumn(
                name: "CuratorId",
                table: "Loans");

            migrationBuilder.DropColumn(
                name: "CurrencyId",
                table: "Loans");

            migrationBuilder.DropColumn(
                name: "DisbursedOn",
                table: "Loans");

            migrationBuilder.DropColumn(
                name: "DisbursementMethodId",
                table: "Loans");

            migrationBuilder.DropColumn(
                name: "FeesAmount",
                table: "Loans");

            migrationBuilder.DropColumn(
                name: "LoanNumber",
                table: "Loans");

            migrationBuilder.DropColumn(
                name: "MaturityOn",
                table: "Loans");

            migrationBuilder.DropColumn(
                name: "OutstandingInterest",
                table: "Loans");

            migrationBuilder.DropColumn(
                name: "OutstandingPrincipal",
                table: "Loans");

            migrationBuilder.DropColumn(
                name: "PaymentFrequencyId",
                table: "Loans");

            migrationBuilder.DropColumn(
                name: "PenaltyPolicyId",
                table: "Loans");

            migrationBuilder.DropColumn(
                name: "PenaltyRate",
                table: "Loans");

            migrationBuilder.DropColumn(
                name: "ProductId",
                table: "Loans");

            migrationBuilder.DropColumn(
                name: "PurposeId",
                table: "Loans");

            migrationBuilder.DropColumn(
                name: "RepaymentMethodId",
                table: "Loans");

            migrationBuilder.DropColumn(
                name: "StatusId",
                table: "Loans");

            migrationBuilder.DropColumn(
                name: "TotalPayable",
                table: "Loans");

            migrationBuilder.DropColumn(
                name: "UpdatedAt",
                table: "Loans");

            migrationBuilder.DropColumn(
                name: "CustomerTypeId",
                table: "Customers");

            migrationBuilder.DropColumn(
                name: "StatusId",
                table: "Customers");

            migrationBuilder.AlterColumn<decimal>(
                name: "InterestRate",
                table: "Loans",
                type: "numeric(5,2)",
                precision: 5,
                scale: 2,
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "numeric(7,4)",
                oldPrecision: 7,
                oldScale: 4);
        }
    }
}
