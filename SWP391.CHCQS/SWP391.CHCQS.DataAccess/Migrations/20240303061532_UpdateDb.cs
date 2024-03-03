using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SWP391.CHCQS.DataAccess.Migrations
{
    public partial class UpdateDb : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Accounts",
                columns: table => new
                {
                    Username = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Password = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Role = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Accounts", x => x.Username);
                });

            migrationBuilder.CreateTable(
                name: "BasementTypes",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: false),
                    Name = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    AreaFactor = table.Column<decimal>(type: "decimal(4,2)", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BasementTypes", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ConstructionTypes",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: false),
                    Name = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Description = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ConstructionTypes", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "FoundationTypes",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: false),
                    Name = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    AreaFactor = table.Column<decimal>(type: "decimal(4,2)", nullable: true),
                    Description = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FoundationTypes", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "InvestmentTypes",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: false),
                    Name = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Description = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_InvestmentTypes", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "MaterialCategories",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: false),
                    Name = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MaterialCategories", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "RooftopTypes",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: false),
                    Name = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: false),
                    AreaFactor = table.Column<decimal>(type: "decimal(4,2)", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RooftopTypes", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "TaskCategories",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: false),
                    Name = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TaskCategories", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "UserConnections",
                columns: table => new
                {
                    UserId = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: false),
                    ConnectionId = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserConnections", x => x.UserId);
                });

            migrationBuilder.CreateTable(
                name: "Customers",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: false),
                    Name = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    PhoneNum = table.Column<string>(type: "nvarchar(12)", maxLength: 12, nullable: true),
                    Email = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    Gender = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: true),
                    Username = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    UsernameNavigationUsername = table.Column<string>(type: "nvarchar(100)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Customers", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Customers_Accounts_UsernameNavigationUsername",
                        column: x => x.UsernameNavigationUsername,
                        principalTable: "Accounts",
                        principalColumn: "Username");
                });

            migrationBuilder.CreateTable(
                name: "Staff",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: false),
                    Name = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: false),
                    PhoneNum = table.Column<string>(type: "nvarchar(12)", maxLength: 12, nullable: true),
                    Email = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: true),
                    Gender = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: false),
                    Username = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: false),
                    ManagerId = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: true),
                    Status = table.Column<bool>(type: "bit", nullable: false),
                    UsernameNavigationUsername = table.Column<string>(type: "nvarchar(100)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Staff", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Staff_Accounts_UsernameNavigationUsername",
                        column: x => x.UsernameNavigationUsername,
                        principalTable: "Accounts",
                        principalColumn: "Username");
                    table.ForeignKey(
                        name: "FK_Staff_Staff_ManagerId",
                        column: x => x.ManagerId,
                        principalTable: "Staff",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Combos",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: false),
                    Name = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    Description = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    Price = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Status = table.Column<bool>(type: "bit", nullable: false),
                    ConstructionId = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: false),
                    ImageUrl = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Combos", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Combos_ConstructionTypes_ConstructionId",
                        column: x => x.ConstructionId,
                        principalTable: "ConstructionTypes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Pricings",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", maxLength: 10, nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ConstructTypeId = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: false),
                    InvestmentTypeId = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: false),
                    UnitPrice = table.Column<decimal>(type: "decimal(18,2)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Pricings", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Pricings_ConstructionTypes_ConstructTypeId",
                        column: x => x.ConstructTypeId,
                        principalTable: "ConstructionTypes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Pricings_InvestmentTypes_InvestmentTypeId",
                        column: x => x.InvestmentTypeId,
                        principalTable: "InvestmentTypes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Materials",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: false),
                    Name = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    UnitPrice = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Unit = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: false),
                    Status = table.Column<bool>(type: "bit", nullable: false),
                    CategoryId = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: false),
                    ImageUrl = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Materials", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Materials_MaterialCategories_CategoryId",
                        column: x => x.CategoryId,
                        principalTable: "MaterialCategories",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Tasks",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: false),
                    Name = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: false),
                    Description = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    UnitPrice = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Status = table.Column<bool>(type: "bit", nullable: false),
                    CategoryId = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Tasks", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Tasks_TaskCategories_CategoryId",
                        column: x => x.CategoryId,
                        principalTable: "TaskCategories",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Projects",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: false),
                    Name = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    Location = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    Scale = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    Size = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Description = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    Overview = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Status = table.Column<bool>(type: "bit", nullable: false),
                    Date = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CustomerId = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Projects", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Projects_Customers_CustomerId",
                        column: x => x.CustomerId,
                        principalTable: "Customers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ComboMaterial",
                columns: table => new
                {
                    CombosId = table.Column<string>(type: "nvarchar(10)", nullable: false),
                    MaterialsId = table.Column<string>(type: "nvarchar(10)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ComboMaterial", x => new { x.CombosId, x.MaterialsId });
                    table.ForeignKey(
                        name: "FK_ComboMaterial_Combos_CombosId",
                        column: x => x.CombosId,
                        principalTable: "Combos",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ComboMaterial_Materials_MaterialsId",
                        column: x => x.MaterialsId,
                        principalTable: "Materials",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "RequestForms",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: false),
                    GenerateDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    ConstructType = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: true),
                    Acreage = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: true),
                    Location = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    Status = table.Column<bool>(type: "bit", nullable: false),
                    CustomerId = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: false),
                    MaterialId = table.Column<string>(type: "nvarchar(10)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RequestForms", x => x.Id);
                    table.ForeignKey(
                        name: "FK_RequestForms_Customers_CustomerId",
                        column: x => x.CustomerId,
                        principalTable: "Customers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_RequestForms_Materials_MaterialId",
                        column: x => x.MaterialId,
                        principalTable: "Materials",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "ComboTask",
                columns: table => new
                {
                    CombosId = table.Column<string>(type: "nvarchar(10)", nullable: false),
                    TasksId = table.Column<string>(type: "nvarchar(10)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ComboTask", x => new { x.CombosId, x.TasksId });
                    table.ForeignKey(
                        name: "FK_ComboTask_Combos_CombosId",
                        column: x => x.CombosId,
                        principalTable: "Combos",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ComboTask_Tasks_TasksId",
                        column: x => x.TasksId,
                        principalTable: "Tasks",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ProjectImages",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", maxLength: 10, nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ImageUrl = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    ProjectId = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProjectImages", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ProjectImages_Projects_ProjectId",
                        column: x => x.ProjectId,
                        principalTable: "Projects",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "CustomQuotations",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: false),
                    Date = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Acreage = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: true),
                    Location = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Status = table.Column<int>(type: "int", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    Total = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    RequestId = table.Column<string>(type: "nvarchar(10)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CustomQuotations", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CustomQuotations_RequestForms_RequestId",
                        column: x => x.RequestId,
                        principalTable: "RequestForms",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "WorkingReports",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    StaffId = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: false),
                    RequestId = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: false),
                    ReceiveDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    SubmitDate = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_WorkingReports", x => x.Id);
                    table.ForeignKey(
                        name: "FK_WorkingReports_RequestForms_RequestId",
                        column: x => x.RequestId,
                        principalTable: "RequestForms",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_WorkingReports_Staff_StaffId",
                        column: x => x.StaffId,
                        principalTable: "Staff",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ConstructDetails",
                columns: table => new
                {
                    QuotationId = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: false),
                    Width = table.Column<decimal>(type: "decimal(6,2)", nullable: false),
                    Length = table.Column<decimal>(type: "decimal(6,2)", nullable: false),
                    Facade = table.Column<int>(type: "int", nullable: false),
                    Alley = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Floor = table.Column<int>(type: "int", nullable: false),
                    Room = table.Column<int>(type: "int", nullable: false),
                    Mezzanine = table.Column<decimal>(type: "decimal(6,2)", nullable: false),
                    RooftopFloor = table.Column<decimal>(type: "decimal(6,2)", nullable: false),
                    Balcony = table.Column<bool>(type: "bit", nullable: false),
                    Garden = table.Column<decimal>(type: "decimal(6,2)", nullable: false),
                    ConstructionId = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: false),
                    InvestmentId = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: false),
                    FoundationId = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: false),
                    RooftopId = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: false),
                    BasementId = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ConstructDetails", x => x.QuotationId);
                    table.ForeignKey(
                        name: "FK_ConstructDetails_BasementTypes_BasementId",
                        column: x => x.BasementId,
                        principalTable: "BasementTypes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ConstructDetails_ConstructionTypes_ConstructionId",
                        column: x => x.ConstructionId,
                        principalTable: "ConstructionTypes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ConstructDetails_CustomQuotations_QuotationId",
                        column: x => x.QuotationId,
                        principalTable: "CustomQuotations",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ConstructDetails_FoundationTypes_FoundationId",
                        column: x => x.FoundationId,
                        principalTable: "FoundationTypes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ConstructDetails_InvestmentTypes_InvestmentId",
                        column: x => x.InvestmentId,
                        principalTable: "InvestmentTypes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ConstructDetails_RooftopTypes_RooftopId",
                        column: x => x.RooftopId,
                        principalTable: "RooftopTypes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "MaterialDetails",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", maxLength: 10, nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    QuotationId = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: false),
                    MaterialId = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: false),
                    Quantity = table.Column<int>(type: "int", nullable: false),
                    Price = table.Column<decimal>(type: "decimal(18,2)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MaterialDetails", x => x.Id);
                    table.ForeignKey(
                        name: "FK_MaterialDetails_CustomQuotations_QuotationId",
                        column: x => x.QuotationId,
                        principalTable: "CustomQuotations",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_MaterialDetails_Materials_MaterialId",
                        column: x => x.MaterialId,
                        principalTable: "Materials",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "RejectionReports",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: false),
                    Date = table.Column<DateTime>(type: "datetime2", nullable: false),
                    RejectedQuotationId = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: false),
                    EngineerId = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: false),
                    ManagerId = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: false),
                    Reason = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RejectionReports", x => x.Id);
                    table.ForeignKey(
                        name: "FK_RejectionReports_CustomQuotations_RejectedQuotationId",
                        column: x => x.RejectedQuotationId,
                        principalTable: "CustomQuotations",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_RejectionReports_Staff_EngineerId",
                        column: x => x.EngineerId,
                        principalTable: "Staff",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_RejectionReports_Staff_ManagerId",
                        column: x => x.ManagerId,
                        principalTable: "Staff",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.NoAction);
                });

            migrationBuilder.CreateTable(
                name: "TaskDetails",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TaskId = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: false),
                    QuotationId = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: false),
                    Price = table.Column<decimal>(type: "decimal(18,2)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TaskDetails", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TaskDetails_CustomQuotations_QuotationId",
                        column: x => x.QuotationId,
                        principalTable: "CustomQuotations",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_TaskDetails_Tasks_TaskId",
                        column: x => x.TaskId,
                        principalTable: "Tasks",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ComboMaterial_MaterialsId",
                table: "ComboMaterial",
                column: "MaterialsId");

            migrationBuilder.CreateIndex(
                name: "IX_Combos_ConstructionId",
                table: "Combos",
                column: "ConstructionId");

            migrationBuilder.CreateIndex(
                name: "IX_ComboTask_TasksId",
                table: "ComboTask",
                column: "TasksId");

            migrationBuilder.CreateIndex(
                name: "IX_ConstructDetails_BasementId",
                table: "ConstructDetails",
                column: "BasementId");

            migrationBuilder.CreateIndex(
                name: "IX_ConstructDetails_ConstructionId",
                table: "ConstructDetails",
                column: "ConstructionId");

            migrationBuilder.CreateIndex(
                name: "IX_ConstructDetails_FoundationId",
                table: "ConstructDetails",
                column: "FoundationId");

            migrationBuilder.CreateIndex(
                name: "IX_ConstructDetails_InvestmentId",
                table: "ConstructDetails",
                column: "InvestmentId");

            migrationBuilder.CreateIndex(
                name: "IX_ConstructDetails_RooftopId",
                table: "ConstructDetails",
                column: "RooftopId");

            migrationBuilder.CreateIndex(
                name: "IX_Customers_UsernameNavigationUsername",
                table: "Customers",
                column: "UsernameNavigationUsername");

            migrationBuilder.CreateIndex(
                name: "IX_CustomQuotations_RequestId",
                table: "CustomQuotations",
                column: "RequestId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_MaterialDetails_MaterialId",
                table: "MaterialDetails",
                column: "MaterialId");

            migrationBuilder.CreateIndex(
                name: "IX_MaterialDetails_QuotationId",
                table: "MaterialDetails",
                column: "QuotationId");

            migrationBuilder.CreateIndex(
                name: "IX_Materials_CategoryId",
                table: "Materials",
                column: "CategoryId");

            migrationBuilder.CreateIndex(
                name: "IX_Pricings_ConstructTypeId",
                table: "Pricings",
                column: "ConstructTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_Pricings_InvestmentTypeId",
                table: "Pricings",
                column: "InvestmentTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_ProjectImages_ProjectId",
                table: "ProjectImages",
                column: "ProjectId");

            migrationBuilder.CreateIndex(
                name: "IX_Projects_CustomerId",
                table: "Projects",
                column: "CustomerId");

            migrationBuilder.CreateIndex(
                name: "IX_RejectionReports_EngineerId",
                table: "RejectionReports",
                column: "EngineerId");

            migrationBuilder.CreateIndex(
                name: "IX_RejectionReports_ManagerId",
                table: "RejectionReports",
                column: "ManagerId");

            migrationBuilder.CreateIndex(
                name: "IX_RejectionReports_RejectedQuotationId",
                table: "RejectionReports",
                column: "RejectedQuotationId");

            migrationBuilder.CreateIndex(
                name: "IX_RequestForms_CustomerId",
                table: "RequestForms",
                column: "CustomerId");

            migrationBuilder.CreateIndex(
                name: "IX_RequestForms_MaterialId",
                table: "RequestForms",
                column: "MaterialId");

            migrationBuilder.CreateIndex(
                name: "IX_Staff_ManagerId",
                table: "Staff",
                column: "ManagerId");

            migrationBuilder.CreateIndex(
                name: "IX_Staff_UsernameNavigationUsername",
                table: "Staff",
                column: "UsernameNavigationUsername");

            migrationBuilder.CreateIndex(
                name: "IX_TaskDetails_QuotationId",
                table: "TaskDetails",
                column: "QuotationId");

            migrationBuilder.CreateIndex(
                name: "IX_TaskDetails_TaskId",
                table: "TaskDetails",
                column: "TaskId");

            migrationBuilder.CreateIndex(
                name: "IX_Tasks_CategoryId",
                table: "Tasks",
                column: "CategoryId");

            migrationBuilder.CreateIndex(
                name: "IX_WorkingReports_RequestId",
                table: "WorkingReports",
                column: "RequestId");

            migrationBuilder.CreateIndex(
                name: "IX_WorkingReports_StaffId",
                table: "WorkingReports",
                column: "StaffId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ComboMaterial");

            migrationBuilder.DropTable(
                name: "ComboTask");

            migrationBuilder.DropTable(
                name: "ConstructDetails");

            migrationBuilder.DropTable(
                name: "MaterialDetails");

            migrationBuilder.DropTable(
                name: "Pricings");

            migrationBuilder.DropTable(
                name: "ProjectImages");

            migrationBuilder.DropTable(
                name: "RejectionReports");

            migrationBuilder.DropTable(
                name: "TaskDetails");

            migrationBuilder.DropTable(
                name: "UserConnections");

            migrationBuilder.DropTable(
                name: "WorkingReports");

            migrationBuilder.DropTable(
                name: "Combos");

            migrationBuilder.DropTable(
                name: "BasementTypes");

            migrationBuilder.DropTable(
                name: "FoundationTypes");

            migrationBuilder.DropTable(
                name: "RooftopTypes");

            migrationBuilder.DropTable(
                name: "InvestmentTypes");

            migrationBuilder.DropTable(
                name: "Projects");

            migrationBuilder.DropTable(
                name: "CustomQuotations");

            migrationBuilder.DropTable(
                name: "Tasks");

            migrationBuilder.DropTable(
                name: "Staff");

            migrationBuilder.DropTable(
                name: "ConstructionTypes");

            migrationBuilder.DropTable(
                name: "RequestForms");

            migrationBuilder.DropTable(
                name: "TaskCategories");

            migrationBuilder.DropTable(
                name: "Customers");

            migrationBuilder.DropTable(
                name: "Materials");

            migrationBuilder.DropTable(
                name: "Accounts");

            migrationBuilder.DropTable(
                name: "MaterialCategories");
        }
    }
}
