using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace TenancyContract.Migrations
{
    public partial class Initial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "AbuseReports",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    SenderId = table.Column<string>(nullable: true),
                    ReportText = table.Column<string>(nullable: true),
                    ReportTime = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AbuseReports", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Districts",
                columns: table => new
                {
                    DistrictId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    DivisionId = table.Column<int>(nullable: false),
                    DistrictName = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Districts", x => x.DistrictId);
                });

            migrationBuilder.CreateTable(
                name: "Divisions",
                columns: table => new
                {
                    DivisionId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    DivisionName = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Divisions", x => x.DivisionId);
                });

            migrationBuilder.CreateTable(
                name: "HouseOwners",
                columns: table => new
                {
                    Id = table.Column<string>(nullable: false),
                    Name = table.Column<string>(nullable: true),
                    Image = table.Column<string>(nullable: true),
                    Gender = table.Column<string>(nullable: true),
                    AddressDivision = table.Column<string>(nullable: true),
                    AddressDistrict = table.Column<string>(nullable: true),
                    AddressThana = table.Column<string>(nullable: true),
                    AddressPO = table.Column<string>(nullable: true),
                    AddressVillage = table.Column<string>(nullable: true),
                    AddressRoad = table.Column<string>(nullable: true),
                    Mobile = table.Column<string>(nullable: true),
                    Email = table.Column<string>(nullable: true),
                    NID = table.Column<string>(nullable: true),
                    DOB = table.Column<DateTime>(nullable: false),
                    PasswordHash = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_HouseOwners", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Houses",
                columns: table => new
                {
                    Id = table.Column<string>(nullable: false),
                    DagNo = table.Column<string>(nullable: true),
                    HouseNo = table.Column<string>(nullable: true),
                    AddressDivision = table.Column<string>(nullable: true),
                    AddressDistrict = table.Column<string>(nullable: true),
                    AddressThana = table.Column<string>(nullable: true),
                    AddressPO = table.Column<string>(nullable: true),
                    AddressVillage = table.Column<string>(nullable: true),
                    AddressRoad = table.Column<string>(nullable: true),
                    ServiceCentres = table.Column<string>(nullable: true),
                    CityCorporation = table.Column<string>(nullable: true),
                    NID = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Houses", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "LogInInfos",
                columns: table => new
                {
                    Id = table.Column<string>(nullable: false),
                    Password = table.Column<string>(nullable: true),
                    UserType = table.Column<string>(nullable: true),
                    UserStatus = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LogInInfos", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Messages",
                columns: table => new
                {
                    Id = table.Column<string>(nullable: false),
                    SenderId = table.Column<string>(nullable: true),
                    ReceiverId = table.Column<string>(nullable: true),
                    MessageText = table.Column<string>(nullable: true),
                    MessageTime = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Messages", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Moderators",
                columns: table => new
                {
                    Id = table.Column<string>(nullable: false),
                    Name = table.Column<string>(nullable: true),
                    Image = table.Column<string>(nullable: true),
                    Gender = table.Column<string>(nullable: true),
                    AddressDivision = table.Column<string>(nullable: true),
                    AddressDistrict = table.Column<string>(nullable: true),
                    AddressThana = table.Column<string>(nullable: true),
                    AddressPO = table.Column<string>(nullable: true),
                    AddressVillage = table.Column<string>(nullable: true),
                    AddressRoad = table.Column<string>(nullable: true),
                    Mobile = table.Column<string>(nullable: true),
                    Email = table.Column<string>(nullable: true),
                    NID = table.Column<string>(nullable: true),
                    DOB = table.Column<DateTime>(nullable: false),
                    PasswordHash = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Moderators", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Notifications",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Text = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Notifications", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Reviews",
                columns: table => new
                {
                    Id = table.Column<string>(nullable: false),
                    SenderId = table.Column<string>(nullable: true),
                    ReceiverId = table.Column<string>(nullable: true),
                    ReviewText = table.Column<string>(nullable: true),
                    ReviewTime = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Reviews", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Tenants",
                columns: table => new
                {
                    Id = table.Column<string>(nullable: false),
                    Name = table.Column<string>(nullable: true),
                    Image = table.Column<string>(nullable: true),
                    Gender = table.Column<string>(nullable: true),
                    AddressDivision = table.Column<string>(nullable: true),
                    AddressDistrict = table.Column<string>(nullable: true),
                    AddressThana = table.Column<string>(nullable: true),
                    AddressPO = table.Column<string>(nullable: true),
                    AddressVillage = table.Column<string>(nullable: true),
                    AddressRoad = table.Column<string>(nullable: true),
                    Mobile = table.Column<string>(nullable: true),
                    Email = table.Column<string>(nullable: true),
                    NID = table.Column<string>(nullable: true),
                    DOB = table.Column<DateTime>(nullable: false),
                    UserName = table.Column<string>(nullable: true),
                    NormalizedUserName = table.Column<string>(nullable: true),
                    PasswordHash = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Tenants", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Thanas",
                columns: table => new
                {
                    ThanaId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    ThanaName = table.Column<string>(nullable: true),
                    DistrictId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Thanas", x => x.ThanaId);
                });

            migrationBuilder.CreateTable(
                name: "NotificationApplicationUsers",
                columns: table => new
                {
                    NotificationId = table.Column<int>(nullable: false),
                    TenantNID = table.Column<string>(nullable: true),
                    HouseOwnerNID = table.Column<string>(nullable: true),
                    IsRead = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_NotificationApplicationUsers", x => x.NotificationId);
                    table.ForeignKey(
                        name: "FK_NotificationApplicationUsers_Notifications_NotificationId",
                        column: x => x.NotificationId,
                        principalTable: "Notifications",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Contracts",
                columns: table => new
                {
                    Id = table.Column<string>(nullable: false),
                    HouseOwnerId = table.Column<string>(nullable: true),
                    HouseId = table.Column<string>(nullable: true),
                    TenantId = table.Column<string>(nullable: true),
                    StartDate = table.Column<DateTime>(nullable: false),
                    EndDate = table.Column<DateTime>(nullable: false),
                    AgreementDate = table.Column<DateTime>(nullable: false),
                    Advance = table.Column<int>(nullable: false),
                    GasBill = table.Column<int>(nullable: false),
                    ElectricityBill = table.Column<int>(nullable: false),
                    TenantNID = table.Column<string>(nullable: true),
                    HouseOwnerNID = table.Column<string>(nullable: true),
                    MaintainCost = table.Column<int>(nullable: false),
                    IncreaseRate = table.Column<int>(nullable: false),
                    IncreaseTime = table.Column<DateTime>(nullable: false),
                    AcceptTenant = table.Column<bool>(nullable: false),
                    AcceptHO = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Contracts", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Contracts_Houses_HouseId",
                        column: x => x.HouseId,
                        principalTable: "Houses",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Contracts_HouseOwners_HouseOwnerId",
                        column: x => x.HouseOwnerId,
                        principalTable: "HouseOwners",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Contracts_Tenants_TenantId",
                        column: x => x.TenantId,
                        principalTable: "Tenants",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Contracts_HouseId",
                table: "Contracts",
                column: "HouseId");

            migrationBuilder.CreateIndex(
                name: "IX_Contracts_HouseOwnerId",
                table: "Contracts",
                column: "HouseOwnerId");

            migrationBuilder.CreateIndex(
                name: "IX_Contracts_TenantId",
                table: "Contracts",
                column: "TenantId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AbuseReports");

            migrationBuilder.DropTable(
                name: "Contracts");

            migrationBuilder.DropTable(
                name: "Districts");

            migrationBuilder.DropTable(
                name: "Divisions");

            migrationBuilder.DropTable(
                name: "LogInInfos");

            migrationBuilder.DropTable(
                name: "Messages");

            migrationBuilder.DropTable(
                name: "Moderators");

            migrationBuilder.DropTable(
                name: "NotificationApplicationUsers");

            migrationBuilder.DropTable(
                name: "Reviews");

            migrationBuilder.DropTable(
                name: "Thanas");

            migrationBuilder.DropTable(
                name: "Houses");

            migrationBuilder.DropTable(
                name: "HouseOwners");

            migrationBuilder.DropTable(
                name: "Tenants");

            migrationBuilder.DropTable(
                name: "Notifications");
        }
    }
}
