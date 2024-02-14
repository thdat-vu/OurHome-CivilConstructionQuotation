using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SWP391.CHCQS.DataAccess.Migrations
{
    public partial class InitializeDB : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Account",
                columns: table => new
                {
                    username = table.Column<string>(type: "varchar(20)", unicode: false, maxLength: 20, nullable: false),
                    password = table.Column<string>(type: "varchar(20)", unicode: false, maxLength: 20, nullable: false),
                    role = table.Column<string>(type: "varchar(10)", unicode: false, maxLength: 10, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Account__F3DBC5731230846A", x => x.username);
                });

            migrationBuilder.CreateTable(
                name: "BasementType",
                columns: table => new
                {
                    id = table.Column<string>(type: "char(3)", unicode: false, fixedLength: true, maxLength: 3, nullable: false),
                    name = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    unitPrice = table.Column<decimal>(type: "money", nullable: false),
                    description = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BasementType", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "ConstructionType",
                columns: table => new
                {
                    id = table.Column<string>(type: "char(3)", unicode: false, fixedLength: true, maxLength: 3, nullable: false),
                    name = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    description = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ConstructionType", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "FoundationType",
                columns: table => new
                {
                    id = table.Column<string>(type: "char(3)", unicode: false, fixedLength: true, maxLength: 3, nullable: false),
                    name = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    areaRatio = table.Column<decimal>(type: "decimal(4,2)", nullable: true),
                    unitPrice = table.Column<decimal>(type: "money", nullable: false),
                    description = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FoundationType", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "InvestmentType",
                columns: table => new
                {
                    id = table.Column<string>(type: "char(3)", unicode: false, fixedLength: true, maxLength: 3, nullable: false),
                    name = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    description = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_InvestmentType", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "MaterialCategory",
                columns: table => new
                {
                    id = table.Column<string>(type: "char(3)", unicode: false, fixedLength: true, maxLength: 3, nullable: false),
                    name = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MaterialCategory", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "RooftopType",
                columns: table => new
                {
                    id = table.Column<string>(type: "char(3)", unicode: false, fixedLength: true, maxLength: 3, nullable: false),
                    name = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    unitPrice = table.Column<decimal>(type: "money", nullable: false),
                    description = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RooftopType", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "TaskCategory",
                columns: table => new
                {
                    id = table.Column<string>(type: "char(3)", unicode: false, fixedLength: true, maxLength: 3, nullable: false),
                    name = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TaskCategory", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "Customer",
                columns: table => new
                {
                    id = table.Column<string>(type: "char(5)", unicode: false, fixedLength: true, maxLength: 5, nullable: false),
                    name = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: false),
                    phoneNum = table.Column<string>(type: "varchar(15)", unicode: false, maxLength: 15, nullable: true),
                    email = table.Column<string>(type: "varchar(35)", unicode: false, maxLength: 35, nullable: true),
                    gender = table.Column<string>(type: "varchar(6)", unicode: false, maxLength: 6, nullable: false),
                    username = table.Column<string>(type: "varchar(20)", unicode: false, maxLength: 20, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Customer", x => x.id);
                    table.ForeignKey(
                        name: "FK__Customer__userna__534D60F1",
                        column: x => x.username,
                        principalTable: "Account",
                        principalColumn: "username");
                });

            migrationBuilder.CreateTable(
                name: "Staff",
                columns: table => new
                {
                    id = table.Column<string>(type: "char(5)", unicode: false, fixedLength: true, maxLength: 5, nullable: false),
                    name = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: false),
                    phoneNum = table.Column<string>(type: "varchar(15)", unicode: false, maxLength: 15, nullable: true),
                    email = table.Column<string>(type: "varchar(35)", unicode: false, maxLength: 35, nullable: true),
                    gender = table.Column<string>(type: "varchar(6)", unicode: false, maxLength: 6, nullable: false),
                    username = table.Column<string>(type: "varchar(20)", unicode: false, maxLength: 20, nullable: false),
                    managerId = table.Column<string>(type: "char(5)", unicode: false, fixedLength: true, maxLength: 5, nullable: true),
                    status = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Staff", x => x.id);
                    table.ForeignKey(
                        name: "FK__Staff__managerId__628FA481",
                        column: x => x.managerId,
                        principalTable: "Staff",
                        principalColumn: "id");
                    table.ForeignKey(
                        name: "FK__Staff__username__6383C8BA",
                        column: x => x.username,
                        principalTable: "Account",
                        principalColumn: "username");
                });

            migrationBuilder.CreateTable(
                name: "StandardQuotation",
                columns: table => new
                {
                    id = table.Column<string>(type: "char(5)", unicode: false, fixedLength: true, maxLength: 5, nullable: false),
                    name = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    description = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    price = table.Column<decimal>(type: "money", nullable: false),
                    status = table.Column<bool>(type: "bit", nullable: false),
                    constructionId = table.Column<string>(type: "char(3)", unicode: false, fixedLength: true, maxLength: 3, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_StandardQuotation", x => x.id);
                    table.ForeignKey(
                        name: "FK__StandardQ__const__6477ECF3",
                        column: x => x.constructionId,
                        principalTable: "ConstructionType",
                        principalColumn: "id");
                });

            migrationBuilder.CreateTable(
                name: "Pricing",
                columns: table => new
                {
                    ConstructTypeID = table.Column<string>(type: "char(3)", unicode: false, fixedLength: true, maxLength: 3, nullable: false),
                    InvestmentTypeID = table.Column<string>(type: "char(3)", unicode: false, fixedLength: true, maxLength: 3, nullable: false),
                    UnitPrice = table.Column<decimal>(type: "decimal(10,2)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Pricing__82221887E948CB0C", x => new { x.ConstructTypeID, x.InvestmentTypeID });
                    table.ForeignKey(
                        name: "FK__Pricing__Constru__5CD6CB2B",
                        column: x => x.ConstructTypeID,
                        principalTable: "ConstructionType",
                        principalColumn: "id");
                    table.ForeignKey(
                        name: "FK__Pricing__Investm__5DCAEF64",
                        column: x => x.InvestmentTypeID,
                        principalTable: "InvestmentType",
                        principalColumn: "id");
                });

            migrationBuilder.CreateTable(
                name: "Material",
                columns: table => new
                {
                    id = table.Column<string>(type: "char(5)", unicode: false, fixedLength: true, maxLength: 5, nullable: false),
                    name = table.Column<string>(type: "nvarchar(80)", maxLength: 80, nullable: false),
                    inventoryQuantity = table.Column<int>(type: "int", nullable: false),
                    unitPrice = table.Column<decimal>(type: "money", nullable: false),
                    unit = table.Column<string>(type: "varchar(5)", unicode: false, maxLength: 5, nullable: false),
                    status = table.Column<bool>(type: "bit", nullable: false),
                    categoryId = table.Column<string>(type: "char(3)", unicode: false, fixedLength: true, maxLength: 3, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Material", x => x.id);
                    table.ForeignKey(
                        name: "FK__Material__catego__5812160E",
                        column: x => x.categoryId,
                        principalTable: "MaterialCategory",
                        principalColumn: "id");
                });

            migrationBuilder.CreateTable(
                name: "Task",
                columns: table => new
                {
                    id = table.Column<string>(type: "char(5)", unicode: false, fixedLength: true, maxLength: 5, nullable: false),
                    name = table.Column<string>(type: "nvarchar(80)", maxLength: 80, nullable: false),
                    description = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    unitPrice = table.Column<decimal>(type: "money", nullable: false),
                    status = table.Column<bool>(type: "bit", nullable: false),
                    categoryId = table.Column<string>(type: "char(3)", unicode: false, fixedLength: true, maxLength: 3, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Task", x => x.id);
                    table.ForeignKey(
                        name: "FK__Task__categoryId__656C112C",
                        column: x => x.categoryId,
                        principalTable: "TaskCategory",
                        principalColumn: "id");
                });

            migrationBuilder.CreateTable(
                name: "Project",
                columns: table => new
                {
                    id = table.Column<string>(type: "char(5)", unicode: false, fixedLength: true, maxLength: 5, nullable: false),
                    name = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    location = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    scale = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    size = table.Column<string>(type: "varchar(10)", unicode: false, maxLength: 10, nullable: false),
                    description = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    status = table.Column<bool>(type: "bit", nullable: false),
                    customerId = table.Column<string>(type: "char(5)", unicode: false, fixedLength: true, maxLength: 5, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Project", x => x.id);
                    table.ForeignKey(
                        name: "FK__Project__custome__5AEE82B9",
                        column: x => x.customerId,
                        principalTable: "Customer",
                        principalColumn: "id");
                });

            migrationBuilder.CreateTable(
                name: "RequestForm",
                columns: table => new
                {
                    id = table.Column<string>(type: "char(5)", unicode: false, fixedLength: true, maxLength: 5, nullable: false),
                    generateDate = table.Column<DateTime>(type: "datetime", nullable: false),
                    description = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    constructType = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    acreage = table.Column<string>(type: "varchar(10)", unicode: false, maxLength: 10, nullable: true),
                    location = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: false),
                    status = table.Column<bool>(type: "bit", nullable: false),
                    customerId = table.Column<string>(type: "char(5)", unicode: false, fixedLength: true, maxLength: 5, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RequestForm", x => x.id);
                    table.ForeignKey(
                        name: "FK__RequestFo__custo__5FB337D6",
                        column: x => x.customerId,
                        principalTable: "Customer",
                        principalColumn: "id");
                });

            migrationBuilder.CreateTable(
                name: "StandardQuotationMaterial",
                columns: table => new
                {
                    quotationId = table.Column<string>(type: "char(5)", unicode: false, fixedLength: true, maxLength: 5, nullable: false),
                    materialId = table.Column<string>(type: "char(5)", unicode: false, fixedLength: true, maxLength: 5, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Standard__BCAD866D662F5E93", x => new { x.quotationId, x.materialId });
                    table.ForeignKey(
                        name: "FK__StandardQ__mater__619B8048",
                        column: x => x.materialId,
                        principalTable: "Material",
                        principalColumn: "id");
                    table.ForeignKey(
                        name: "FK__StandardQ__quota__66603565",
                        column: x => x.quotationId,
                        principalTable: "StandardQuotation",
                        principalColumn: "id");
                });

            migrationBuilder.CreateTable(
                name: "StandardQuotationTask",
                columns: table => new
                {
                    quotationId = table.Column<string>(type: "char(5)", unicode: false, fixedLength: true, maxLength: 5, nullable: false),
                    taskId = table.Column<string>(type: "char(5)", unicode: false, fixedLength: true, maxLength: 5, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Standard__48E336F665A8BFCB", x => new { x.quotationId, x.taskId });
                    table.ForeignKey(
                        name: "FK__StandardQ__quota__6754599E",
                        column: x => x.quotationId,
                        principalTable: "StandardQuotation",
                        principalColumn: "id");
                    table.ForeignKey(
                        name: "FK__StandardQ__taskI__6477ECF3",
                        column: x => x.taskId,
                        principalTable: "Task",
                        principalColumn: "id");
                });

            migrationBuilder.CreateTable(
                name: "CustomQuotation",
                columns: table => new
                {
                    id = table.Column<string>(type: "char(6)", unicode: false, fixedLength: true, maxLength: 6, nullable: false),
                    Date = table.Column<DateTime>(type: "datetime", nullable: false),
                    acreage = table.Column<string>(type: "varchar(10)", unicode: false, maxLength: 10, nullable: true),
                    location = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: false),
                    status = table.Column<bool>(type: "bit", nullable: false),
                    description = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    total = table.Column<decimal>(type: "money", nullable: false),
                    sellerId = table.Column<string>(type: "char(5)", unicode: false, fixedLength: true, maxLength: 5, nullable: false),
                    engineerId = table.Column<string>(type: "char(5)", unicode: false, fixedLength: true, maxLength: 5, nullable: false),
                    managerId = table.Column<string>(type: "char(5)", unicode: false, fixedLength: true, maxLength: 5, nullable: false),
                    requestId = table.Column<string>(type: "char(5)", unicode: false, fixedLength: true, maxLength: 5, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CustomQuotation", x => x.id);
                    table.ForeignKey(
                        name: "FK__CustomQuo__engin__5629CD9C",
                        column: x => x.engineerId,
                        principalTable: "Staff",
                        principalColumn: "id");
                    table.ForeignKey(
                        name: "FK__CustomQuo__manag__571DF1D5",
                        column: x => x.managerId,
                        principalTable: "Staff",
                        principalColumn: "id");
                    table.ForeignKey(
                        name: "FK__CustomQuo__reque__5812160E",
                        column: x => x.requestId,
                        principalTable: "RequestForm",
                        principalColumn: "id");
                    table.ForeignKey(
                        name: "FK__CustomQuo__selle__59063A47",
                        column: x => x.sellerId,
                        principalTable: "Staff",
                        principalColumn: "id");
                });

            migrationBuilder.CreateTable(
                name: "RequestFormMaterial",
                columns: table => new
                {
                    requestId = table.Column<string>(type: "char(5)", unicode: false, fixedLength: true, maxLength: 5, nullable: false),
                    materialId = table.Column<string>(type: "char(5)", unicode: false, fixedLength: true, maxLength: 5, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__RequestF__2A5EBB0EE35F5BBE", x => new { x.requestId, x.materialId });
                    table.ForeignKey(
                        name: "FK__RequestFo__mater__5CD6CB2B",
                        column: x => x.materialId,
                        principalTable: "Material",
                        principalColumn: "id");
                    table.ForeignKey(
                        name: "FK__RequestFo__reque__619B8048",
                        column: x => x.requestId,
                        principalTable: "RequestForm",
                        principalColumn: "id");
                });

            migrationBuilder.CreateTable(
                name: "ConstructDetail",
                columns: table => new
                {
                    quotationId = table.Column<string>(type: "char(6)", unicode: false, fixedLength: true, maxLength: 6, nullable: false),
                    width = table.Column<decimal>(type: "decimal(7,2)", nullable: false),
                    length = table.Column<decimal>(type: "decimal(7,2)", nullable: false),
                    facade = table.Column<int>(type: "int", nullable: false),
                    alley = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    floor = table.Column<int>(type: "int", nullable: false),
                    room = table.Column<int>(type: "int", nullable: false),
                    mezzanine = table.Column<decimal>(type: "decimal(7,2)", nullable: false),
                    rooftopFloor = table.Column<decimal>(type: "decimal(7,2)", nullable: false),
                    balcony = table.Column<bool>(type: "bit", nullable: false),
                    garden = table.Column<decimal>(type: "decimal(6,1)", nullable: false),
                    constructionId = table.Column<string>(type: "char(3)", unicode: false, fixedLength: true, maxLength: 3, nullable: false),
                    investmentId = table.Column<string>(type: "char(3)", unicode: false, fixedLength: true, maxLength: 3, nullable: false),
                    foundationId = table.Column<string>(type: "char(3)", unicode: false, fixedLength: true, maxLength: 3, nullable: false),
                    rooftopId = table.Column<string>(type: "char(3)", unicode: false, fixedLength: true, maxLength: 3, nullable: false),
                    basementId = table.Column<string>(type: "char(3)", unicode: false, fixedLength: true, maxLength: 3, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Construc__7536E3527BF2F7DA", x => x.quotationId);
                    table.ForeignKey(
                        name: "FK__Construct__basem__4D94879B",
                        column: x => x.basementId,
                        principalTable: "BasementType",
                        principalColumn: "id");
                    table.ForeignKey(
                        name: "FK__Construct__const__4E88ABD4",
                        column: x => x.constructionId,
                        principalTable: "ConstructionType",
                        principalColumn: "id");
                    table.ForeignKey(
                        name: "FK__Construct__found__4D94879B",
                        column: x => x.foundationId,
                        principalTable: "FoundationType",
                        principalColumn: "id");
                    table.ForeignKey(
                        name: "FK__Construct__inves__5070F446",
                        column: x => x.investmentId,
                        principalTable: "InvestmentType",
                        principalColumn: "id");
                    table.ForeignKey(
                        name: "FK__Construct__quota__5165187F",
                        column: x => x.quotationId,
                        principalTable: "CustomQuotation",
                        principalColumn: "id");
                    table.ForeignKey(
                        name: "FK__Construct__rooft__52593CB8",
                        column: x => x.rooftopId,
                        principalTable: "RooftopType",
                        principalColumn: "id");
                });

            migrationBuilder.CreateTable(
                name: "CustomQuotaionTask",
                columns: table => new
                {
                    taskId = table.Column<string>(type: "char(5)", unicode: false, fixedLength: true, maxLength: 5, nullable: false),
                    quotationId = table.Column<string>(type: "char(6)", unicode: false, fixedLength: true, maxLength: 6, nullable: false),
                    price = table.Column<decimal>(type: "money", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__CustomQu__EA0E34779FFE6727", x => new { x.taskId, x.quotationId });
                    table.ForeignKey(
                        name: "FK__CustomQuo__quota__5441852A",
                        column: x => x.quotationId,
                        principalTable: "CustomQuotation",
                        principalColumn: "id");
                    table.ForeignKey(
                        name: "FK__CustomQuo__taskI__534D60F1",
                        column: x => x.taskId,
                        principalTable: "Task",
                        principalColumn: "id");
                });

            migrationBuilder.CreateTable(
                name: "MaterialDetail",
                columns: table => new
                {
                    quotationId = table.Column<string>(type: "char(6)", unicode: false, fixedLength: true, maxLength: 6, nullable: false),
                    materialId = table.Column<string>(type: "char(5)", unicode: false, fixedLength: true, maxLength: 5, nullable: false),
                    quantity = table.Column<int>(type: "int", nullable: false),
                    price = table.Column<decimal>(type: "money", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Material__BCAD866D29D0C3FC", x => new { x.quotationId, x.materialId });
                    table.ForeignKey(
                        name: "FK__MaterialD__mater__59063A47",
                        column: x => x.materialId,
                        principalTable: "Material",
                        principalColumn: "id");
                    table.ForeignKey(
                        name: "FK__MaterialD__quota__5BE2A6F2",
                        column: x => x.quotationId,
                        principalTable: "CustomQuotation",
                        principalColumn: "id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_ConstructDetail_basementId",
                table: "ConstructDetail",
                column: "basementId");

            migrationBuilder.CreateIndex(
                name: "IX_ConstructDetail_constructionId",
                table: "ConstructDetail",
                column: "constructionId");

            migrationBuilder.CreateIndex(
                name: "IX_ConstructDetail_foundationId",
                table: "ConstructDetail",
                column: "foundationId");

            migrationBuilder.CreateIndex(
                name: "IX_ConstructDetail_investmentId",
                table: "ConstructDetail",
                column: "investmentId");

            migrationBuilder.CreateIndex(
                name: "IX_ConstructDetail_rooftopId",
                table: "ConstructDetail",
                column: "rooftopId");

            migrationBuilder.CreateIndex(
                name: "IX_Customer_username",
                table: "Customer",
                column: "username");

            migrationBuilder.CreateIndex(
                name: "IX_CustomQuotaionTask_quotationId",
                table: "CustomQuotaionTask",
                column: "quotationId");

            migrationBuilder.CreateIndex(
                name: "IX_CustomQuotation_engineerId",
                table: "CustomQuotation",
                column: "engineerId");

            migrationBuilder.CreateIndex(
                name: "IX_CustomQuotation_managerId",
                table: "CustomQuotation",
                column: "managerId");

            migrationBuilder.CreateIndex(
                name: "IX_CustomQuotation_requestId",
                table: "CustomQuotation",
                column: "requestId");

            migrationBuilder.CreateIndex(
                name: "IX_CustomQuotation_sellerId",
                table: "CustomQuotation",
                column: "sellerId");

            migrationBuilder.CreateIndex(
                name: "IX_Material_categoryId",
                table: "Material",
                column: "categoryId");

            migrationBuilder.CreateIndex(
                name: "IX_MaterialDetail_materialId",
                table: "MaterialDetail",
                column: "materialId");

            migrationBuilder.CreateIndex(
                name: "IX_Pricing_InvestmentTypeID",
                table: "Pricing",
                column: "InvestmentTypeID");

            migrationBuilder.CreateIndex(
                name: "IX_Project_customerId",
                table: "Project",
                column: "customerId");

            migrationBuilder.CreateIndex(
                name: "IX_RequestForm_customerId",
                table: "RequestForm",
                column: "customerId");

            migrationBuilder.CreateIndex(
                name: "IX_RequestFormMaterial_materialId",
                table: "RequestFormMaterial",
                column: "materialId");

            migrationBuilder.CreateIndex(
                name: "IX_Staff_managerId",
                table: "Staff",
                column: "managerId");

            migrationBuilder.CreateIndex(
                name: "IX_Staff_username",
                table: "Staff",
                column: "username");

            migrationBuilder.CreateIndex(
                name: "IX_StandardQuotation_constructionId",
                table: "StandardQuotation",
                column: "constructionId");

            migrationBuilder.CreateIndex(
                name: "IX_StandardQuotationMaterial_materialId",
                table: "StandardQuotationMaterial",
                column: "materialId");

            migrationBuilder.CreateIndex(
                name: "IX_StandardQuotationTask_taskId",
                table: "StandardQuotationTask",
                column: "taskId");

            migrationBuilder.CreateIndex(
                name: "IX_Task_categoryId",
                table: "Task",
                column: "categoryId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ConstructDetail");

            migrationBuilder.DropTable(
                name: "CustomQuotaionTask");

            migrationBuilder.DropTable(
                name: "MaterialDetail");

            migrationBuilder.DropTable(
                name: "Pricing");

            migrationBuilder.DropTable(
                name: "Project");

            migrationBuilder.DropTable(
                name: "RequestFormMaterial");

            migrationBuilder.DropTable(
                name: "StandardQuotationMaterial");

            migrationBuilder.DropTable(
                name: "StandardQuotationTask");

            migrationBuilder.DropTable(
                name: "BasementType");

            migrationBuilder.DropTable(
                name: "FoundationType");

            migrationBuilder.DropTable(
                name: "RooftopType");

            migrationBuilder.DropTable(
                name: "CustomQuotation");

            migrationBuilder.DropTable(
                name: "InvestmentType");

            migrationBuilder.DropTable(
                name: "Material");

            migrationBuilder.DropTable(
                name: "StandardQuotation");

            migrationBuilder.DropTable(
                name: "Task");

            migrationBuilder.DropTable(
                name: "Staff");

            migrationBuilder.DropTable(
                name: "RequestForm");

            migrationBuilder.DropTable(
                name: "MaterialCategory");

            migrationBuilder.DropTable(
                name: "ConstructionType");

            migrationBuilder.DropTable(
                name: "TaskCategory");

            migrationBuilder.DropTable(
                name: "Customer");

            migrationBuilder.DropTable(
                name: "Account");
        }
    }
}
