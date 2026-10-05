using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Viamatica.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "AttentionStatuses",
                columns: table => new
                {
                    StatusId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AttentionStatuses", x => x.StatusId);
                });

            migrationBuilder.CreateTable(
                name: "AttentionTypes",
                columns: table => new
                {
                    AttentionTypeId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AttentionTypes", x => x.AttentionTypeId);
                });

            migrationBuilder.CreateTable(
                name: "Cashes",
                columns: table => new
                {
                    CashId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CashDescription = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Active = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Cashes", x => x.CashId);
                });

            migrationBuilder.CreateTable(
                name: "Clients",
                columns: table => new
                {
                    ClientId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    LastName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Identification = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PhoneNumber = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Address = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ReferenceAddress = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Clients", x => x.ClientId);
                });

            migrationBuilder.CreateTable(
                name: "Menus",
                columns: table => new
                {
                    MenuId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Title = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Route = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Icon = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Order = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Menus", x => x.MenuId);
                });

            migrationBuilder.CreateTable(
                name: "MethodPayments",
                columns: table => new
                {
                    MethodPaymentId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MethodPayments", x => x.MethodPaymentId);
                });

            migrationBuilder.CreateTable(
                name: "Roles",
                columns: table => new
                {
                    RolId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RolName = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Roles", x => x.RolId);
                });

            migrationBuilder.CreateTable(
                name: "Services",
                columns: table => new
                {
                    ServiceId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ServiceName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ServiceDescription = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Price = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    SpeedMbps = table.Column<decimal>(type: "decimal(18,2)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Services", x => x.ServiceId);
                });

            migrationBuilder.CreateTable(
                name: "StatusContracts",
                columns: table => new
                {
                    StatusId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_StatusContracts", x => x.StatusId);
                });

            migrationBuilder.CreateTable(
                name: "UserStatuses",
                columns: table => new
                {
                    StatusId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserStatuses", x => x.StatusId);
                });

            migrationBuilder.CreateTable(
                name: "Turns",
                columns: table => new
                {
                    TurnId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Date = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CashCashId = table.Column<int>(type: "int", nullable: false),
                    UserGestorId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Turns", x => x.TurnId);
                    table.ForeignKey(
                        name: "FK_Turns_Cashes_CashCashId",
                        column: x => x.CashCashId,
                        principalTable: "Cashes",
                        principalColumn: "CashId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Payments",
                columns: table => new
                {
                    PaymentId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PaymentDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ClientClientId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Payments", x => x.PaymentId);
                    table.ForeignKey(
                        name: "FK_Payments_Clients_ClientClientId",
                        column: x => x.ClientClientId,
                        principalTable: "Clients",
                        principalColumn: "ClientId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "RoleMenus",
                columns: table => new
                {
                    RolId = table.Column<int>(type: "int", nullable: false),
                    MenuId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RoleMenus", x => new { x.RolId, x.MenuId });
                    table.ForeignKey(
                        name: "FK_RoleMenus_Menus_MenuId",
                        column: x => x.MenuId,
                        principalTable: "Menus",
                        principalColumn: "MenuId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_RoleMenus_Roles_RolId",
                        column: x => x.RolId,
                        principalTable: "Roles",
                        principalColumn: "RolId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Devices",
                columns: table => new
                {
                    DeviceId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    DeviceName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ServiceServiceId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Devices", x => x.DeviceId);
                    table.ForeignKey(
                        name: "FK_Devices_Services_ServiceServiceId",
                        column: x => x.ServiceServiceId,
                        principalTable: "Services",
                        principalColumn: "ServiceId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Contracts",
                columns: table => new
                {
                    ContractId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    StartDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    EndDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    ServiceServiceId = table.Column<int>(type: "int", nullable: false),
                    StatusContractStatusId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ClientClientId = table.Column<int>(type: "int", nullable: false),
                    MethodPaymentMethodPaymentId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Contracts", x => x.ContractId);
                    table.ForeignKey(
                        name: "FK_Contracts_Clients_ClientClientId",
                        column: x => x.ClientClientId,
                        principalTable: "Clients",
                        principalColumn: "ClientId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Contracts_MethodPayments_MethodPaymentMethodPaymentId",
                        column: x => x.MethodPaymentMethodPaymentId,
                        principalTable: "MethodPayments",
                        principalColumn: "MethodPaymentId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Contracts_Services_ServiceServiceId",
                        column: x => x.ServiceServiceId,
                        principalTable: "Services",
                        principalColumn: "ServiceId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Contracts_StatusContracts_StatusContractStatusId",
                        column: x => x.StatusContractStatusId,
                        principalTable: "StatusContracts",
                        principalColumn: "StatusId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    UserId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Username = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Password = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CreationDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UserCreate = table.Column<int>(type: "int", nullable: true),
                    UserApproval = table.Column<int>(type: "int", nullable: true),
                    DateApproval = table.Column<DateTime>(type: "datetime2", nullable: true),
                    UserStatusStatusId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    RolRolId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.UserId);
                    table.ForeignKey(
                        name: "FK_Users_Roles_RolRolId",
                        column: x => x.RolRolId,
                        principalTable: "Roles",
                        principalColumn: "RolId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Users_UserStatuses_UserStatusStatusId",
                        column: x => x.UserStatusStatusId,
                        principalTable: "UserStatuses",
                        principalColumn: "StatusId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Attentions",
                columns: table => new
                {
                    AttentionId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TurnTurnId = table.Column<int>(type: "int", nullable: false),
                    ClientClientId = table.Column<int>(type: "int", nullable: false),
                    AttentionTypeAttentionTypeId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    AttentionStatusStatusId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Attentions", x => x.AttentionId);
                    table.ForeignKey(
                        name: "FK_Attentions_AttentionStatuses_AttentionStatusStatusId",
                        column: x => x.AttentionStatusStatusId,
                        principalTable: "AttentionStatuses",
                        principalColumn: "StatusId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Attentions_AttentionTypes_AttentionTypeAttentionTypeId",
                        column: x => x.AttentionTypeAttentionTypeId,
                        principalTable: "AttentionTypes",
                        principalColumn: "AttentionTypeId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Attentions_Clients_ClientClientId",
                        column: x => x.ClientClientId,
                        principalTable: "Clients",
                        principalColumn: "ClientId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Attentions_Turns_TurnTurnId",
                        column: x => x.TurnTurnId,
                        principalTable: "Turns",
                        principalColumn: "TurnId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "UserCashes",
                columns: table => new
                {
                    UserUserId = table.Column<int>(type: "int", nullable: false),
                    CashCashId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserCashes", x => new { x.UserUserId, x.CashCashId });
                    table.ForeignKey(
                        name: "FK_UserCashes_Cashes_CashCashId",
                        column: x => x.CashCashId,
                        principalTable: "Cashes",
                        principalColumn: "CashId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_UserCashes_Users_UserUserId",
                        column: x => x.UserUserId,
                        principalTable: "Users",
                        principalColumn: "UserId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Attentions_AttentionStatusStatusId",
                table: "Attentions",
                column: "AttentionStatusStatusId");

            migrationBuilder.CreateIndex(
                name: "IX_Attentions_AttentionTypeAttentionTypeId",
                table: "Attentions",
                column: "AttentionTypeAttentionTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_Attentions_ClientClientId",
                table: "Attentions",
                column: "ClientClientId");

            migrationBuilder.CreateIndex(
                name: "IX_Attentions_TurnTurnId",
                table: "Attentions",
                column: "TurnTurnId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Contracts_ClientClientId",
                table: "Contracts",
                column: "ClientClientId");

            migrationBuilder.CreateIndex(
                name: "IX_Contracts_MethodPaymentMethodPaymentId",
                table: "Contracts",
                column: "MethodPaymentMethodPaymentId");

            migrationBuilder.CreateIndex(
                name: "IX_Contracts_ServiceServiceId",
                table: "Contracts",
                column: "ServiceServiceId");

            migrationBuilder.CreateIndex(
                name: "IX_Contracts_StatusContractStatusId",
                table: "Contracts",
                column: "StatusContractStatusId");

            migrationBuilder.CreateIndex(
                name: "IX_Devices_ServiceServiceId",
                table: "Devices",
                column: "ServiceServiceId");

            migrationBuilder.CreateIndex(
                name: "IX_Payments_ClientClientId",
                table: "Payments",
                column: "ClientClientId");

            migrationBuilder.CreateIndex(
                name: "IX_RoleMenus_MenuId",
                table: "RoleMenus",
                column: "MenuId");

            migrationBuilder.CreateIndex(
                name: "IX_Turns_CashCashId",
                table: "Turns",
                column: "CashCashId");

            migrationBuilder.CreateIndex(
                name: "IX_UserCashes_CashCashId",
                table: "UserCashes",
                column: "CashCashId");

            migrationBuilder.CreateIndex(
                name: "IX_Users_RolRolId",
                table: "Users",
                column: "RolRolId");

            migrationBuilder.CreateIndex(
                name: "IX_Users_UserStatusStatusId",
                table: "Users",
                column: "UserStatusStatusId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Attentions");

            migrationBuilder.DropTable(
                name: "Contracts");

            migrationBuilder.DropTable(
                name: "Devices");

            migrationBuilder.DropTable(
                name: "Payments");

            migrationBuilder.DropTable(
                name: "RoleMenus");

            migrationBuilder.DropTable(
                name: "UserCashes");

            migrationBuilder.DropTable(
                name: "AttentionStatuses");

            migrationBuilder.DropTable(
                name: "AttentionTypes");

            migrationBuilder.DropTable(
                name: "Turns");

            migrationBuilder.DropTable(
                name: "MethodPayments");

            migrationBuilder.DropTable(
                name: "StatusContracts");

            migrationBuilder.DropTable(
                name: "Services");

            migrationBuilder.DropTable(
                name: "Clients");

            migrationBuilder.DropTable(
                name: "Menus");

            migrationBuilder.DropTable(
                name: "Users");

            migrationBuilder.DropTable(
                name: "Cashes");

            migrationBuilder.DropTable(
                name: "Roles");

            migrationBuilder.DropTable(
                name: "UserStatuses");
        }
    }
}
