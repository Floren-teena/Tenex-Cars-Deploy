using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace TenexCarsDeploy.Data.Migrations
{
    /// <inheritdoc />
    public partial class InitialMigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "AspNetRoles",
                columns: table => new
                {
                    Id = table.Column<string>(type: "text", nullable: false),
                    Name = table.Column<string>(type: "character varying(256)", maxLength: 256, nullable: true),
                    NormalizedName = table.Column<string>(type: "character varying(256)", maxLength: 256, nullable: true),
                    ConcurrencyStamp = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetRoles", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUsers",
                columns: table => new
                {
                    Id = table.Column<string>(type: "text", nullable: false),
                    FirstName = table.Column<string>(type: "text", nullable: true),
                    LastName = table.Column<string>(type: "text", nullable: true),
                    Type = table.Column<string>(type: "text", nullable: true),
                    OperatorId = table.Column<string>(type: "text", nullable: true),
                    UserName = table.Column<string>(type: "character varying(256)", maxLength: 256, nullable: true),
                    NormalizedUserName = table.Column<string>(type: "character varying(256)", maxLength: 256, nullable: true),
                    Email = table.Column<string>(type: "character varying(256)", maxLength: 256, nullable: true),
                    NormalizedEmail = table.Column<string>(type: "character varying(256)", maxLength: 256, nullable: true),
                    EmailConfirmed = table.Column<bool>(type: "boolean", nullable: false),
                    PasswordHash = table.Column<string>(type: "text", nullable: true),
                    SecurityStamp = table.Column<string>(type: "text", nullable: true),
                    ConcurrencyStamp = table.Column<string>(type: "text", nullable: true),
                    PhoneNumber = table.Column<string>(type: "text", nullable: true),
                    PhoneNumberConfirmed = table.Column<bool>(type: "boolean", nullable: false),
                    TwoFactorEnabled = table.Column<bool>(type: "boolean", nullable: false),
                    LockoutEnd = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: true),
                    LockoutEnabled = table.Column<bool>(type: "boolean", nullable: false),
                    AccessFailedCount = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUsers", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AspNetRoleClaims",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    RoleId = table.Column<string>(type: "text", nullable: false),
                    ClaimType = table.Column<string>(type: "text", nullable: true),
                    ClaimValue = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetRoleClaims", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AspNetRoleClaims_AspNetRoles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "AspNetRoles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserClaims",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    UserId = table.Column<string>(type: "text", nullable: false),
                    ClaimType = table.Column<string>(type: "text", nullable: true),
                    ClaimValue = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserClaims", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AspNetUserClaims_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserLogins",
                columns: table => new
                {
                    LoginProvider = table.Column<string>(type: "text", nullable: false),
                    ProviderKey = table.Column<string>(type: "text", nullable: false),
                    ProviderDisplayName = table.Column<string>(type: "text", nullable: true),
                    UserId = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserLogins", x => new { x.LoginProvider, x.ProviderKey });
                    table.ForeignKey(
                        name: "FK_AspNetUserLogins_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserRoles",
                columns: table => new
                {
                    UserId = table.Column<string>(type: "text", nullable: false),
                    RoleId = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserRoles", x => new { x.UserId, x.RoleId });
                    table.ForeignKey(
                        name: "FK_AspNetUserRoles_AspNetRoles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "AspNetRoles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AspNetUserRoles_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserTokens",
                columns: table => new
                {
                    UserId = table.Column<string>(type: "text", nullable: false),
                    LoginProvider = table.Column<string>(type: "text", nullable: false),
                    Name = table.Column<string>(type: "text", nullable: false),
                    Value = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserTokens", x => new { x.UserId, x.LoginProvider, x.Name });
                    table.ForeignKey(
                        name: "FK_AspNetUserTokens_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Operators",
                columns: table => new
                {
                    Id = table.Column<string>(type: "text", nullable: false),
                    PhoneNumber = table.Column<string>(type: "text", nullable: true),
                    CompanyName = table.Column<string>(type: "text", nullable: true),
                    CompanyAddress = table.Column<string>(type: "text", nullable: true),
                    ContactDOB = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    Country = table.Column<string>(type: "text", nullable: true),
                    State = table.Column<string>(type: "text", nullable: true),
                    City = table.Column<string>(type: "text", nullable: true),
                    Zip = table.Column<string>(type: "text", nullable: true),
                    IdentificationDocument = table.Column<string>(type: "text", nullable: true),
                    CompanyRegistrationDocument = table.Column<string>(type: "text", nullable: true),
                    OperatorSubscriptionDuration = table.Column<string>(type: "text", nullable: true),
                    BusinessName = table.Column<string>(type: "text", nullable: true),
                    InsuranceOffering = table.Column<int>(type: "integer", nullable: false),
                    MultipleSubscribers = table.Column<int>(type: "integer", nullable: false),
                    CompanyLogo = table.Column<string>(type: "text", nullable: true),
                    HeroGraphics = table.Column<string>(type: "text", nullable: true),
                    FAQLink = table.Column<string>(type: "text", nullable: true),
                    ContactLink = table.Column<string>(type: "text", nullable: true),
                    SupportContact1 = table.Column<string>(type: "text", nullable: true),
                    SupportContact2 = table.Column<string>(type: "text", nullable: true),
                    AppUserId = table.Column<string>(type: "text", nullable: true),
                    AppUserId1 = table.Column<string>(type: "text", nullable: true),
                    FirstName = table.Column<string>(type: "text", nullable: false),
                    LastName = table.Column<string>(type: "text", nullable: false),
                    Email = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Operators", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Operators_AspNetUsers_AppUserId1",
                        column: x => x.AppUserId1,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Subscribers",
                columns: table => new
                {
                    Id = table.Column<string>(type: "text", nullable: false),
                    PhoneNumber = table.Column<string>(type: "text", nullable: true),
                    HomeAddress = table.Column<string>(type: "text", nullable: true),
                    Country = table.Column<string>(type: "text", nullable: true),
                    State = table.Column<int>(type: "integer", nullable: false),
                    City = table.Column<string>(type: "text", nullable: true),
                    ZipCode = table.Column<string>(type: "text", nullable: true),
                    DateOfBirth = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    AppUserId = table.Column<string>(type: "text", nullable: true),
                    FirstName = table.Column<string>(type: "text", nullable: false),
                    LastName = table.Column<string>(type: "text", nullable: false),
                    Email = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Subscribers", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Subscribers_AspNetUsers_AppUserId",
                        column: x => x.AppUserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "OperatorMembers",
                columns: table => new
                {
                    Id = table.Column<string>(type: "text", nullable: false),
                    OperatorId = table.Column<string>(type: "text", nullable: true),
                    AppUserId = table.Column<string>(type: "text", nullable: true),
                    Role = table.Column<string>(type: "text", nullable: true),
                    FirstName = table.Column<string>(type: "text", nullable: false),
                    LastName = table.Column<string>(type: "text", nullable: false),
                    Email = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OperatorMembers", x => x.Id);
                    table.ForeignKey(
                        name: "FK_OperatorMembers_AspNetUsers_AppUserId",
                        column: x => x.AppUserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_OperatorMembers_Operators_OperatorId",
                        column: x => x.OperatorId,
                        principalTable: "Operators",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Vehicles",
                columns: table => new
                {
                    Id = table.Column<string>(type: "text", nullable: false),
                    Make = table.Column<string>(type: "text", nullable: true),
                    Model = table.Column<string>(type: "text", nullable: true),
                    PlateNumber = table.Column<string>(type: "text", nullable: true),
                    CarDescription = table.Column<string>(type: "text", nullable: true),
                    ChasisNumber = table.Column<string>(type: "text", nullable: true),
                    SeatNumbers = table.Column<string>(type: "text", nullable: true),
                    Mileage = table.Column<string>(type: "text", nullable: true),
                    TrunkSize = table.Column<string>(type: "text", nullable: true),
                    Color = table.Column<string>(type: "text", nullable: true),
                    VIN = table.Column<string>(type: "text", nullable: true),
                    PickupAddress = table.Column<string>(type: "text", nullable: true),
                    City = table.Column<string>(type: "text", nullable: true),
                    ZIP = table.Column<string>(type: "text", nullable: true),
                    OperatorId = table.Column<string>(type: "text", nullable: true),
                    CarDealerName = table.Column<string>(type: "text", nullable: true),
                    FinacialStartDate = table.Column<string>(type: "text", nullable: true),
                    FinacialEndDate = table.Column<string>(type: "text", nullable: true),
                    CarWarrantyOverview = table.Column<string>(type: "text", nullable: true),
                    Torque = table.Column<string>(type: "text", nullable: true),
                    TransmissionType = table.Column<string>(type: "text", nullable: true),
                    Horsepower = table.Column<string>(type: "text", nullable: true),
                    TurningDiameter = table.Column<string>(type: "text", nullable: true),
                    CurbWeight = table.Column<string>(type: "text", nullable: true),
                    DiscBrakes = table.Column<string>(type: "text", nullable: true),
                    TransmissionSpeed = table.Column<string>(type: "text", nullable: true),
                    DriveAxleRatio = table.Column<string>(type: "text", nullable: true),
                    StabilityControl = table.Column<string>(type: "text", nullable: true),
                    RangeOfFullCharge = table.Column<string>(type: "text", nullable: true),
                    CargoSpace = table.Column<string>(type: "text", nullable: true),
                    TouchScreenDisplay = table.Column<bool>(type: "boolean", nullable: true),
                    DriverLumbarSupport = table.Column<bool>(type: "boolean", nullable: true),
                    NumberOfSpeakers = table.Column<string>(type: "text", nullable: true),
                    Radio = table.Column<bool>(type: "boolean", nullable: true),
                    AndroidAuto_AppleCarPlay = table.Column<bool>(type: "boolean", nullable: true),
                    TouchScreenNavSystem = table.Column<bool>(type: "boolean", nullable: true),
                    ProjectorBeamLedHeadlight = table.Column<bool>(type: "boolean", nullable: true),
                    RemoteKeylessEntry = table.Column<bool>(type: "boolean", nullable: true),
                    StandardLowTirePressureWarning = table.Column<bool>(type: "boolean", nullable: true),
                    BluetoothSystem = table.Column<bool>(type: "boolean", nullable: true),
                    WheelDrive = table.Column<int>(type: "integer", nullable: true),
                    EngineType = table.Column<int>(type: "integer", nullable: true),
                    State = table.Column<int>(type: "integer", nullable: true),
                    DcFastCharging = table.Column<int>(type: "integer", nullable: true),
                    HomeCharger = table.Column<int>(type: "integer", nullable: true),
                    SeatHeater = table.Column<int>(type: "integer", nullable: true),
                    Cartype = table.Column<int>(type: "integer", nullable: true),
                    ReservationFee = table.Column<double>(type: "double precision", nullable: true),
                    TotalCostOfCar = table.Column<double>(type: "double precision", nullable: true),
                    ActivationFee = table.Column<double>(type: "double precision", nullable: true),
                    MonthlyCost = table.Column<double>(type: "double precision", nullable: true),
                    MarketValue = table.Column<double>(type: "double precision", nullable: true),
                    DecrementMarketValue = table.Column<double>(type: "double precision", nullable: true),
                    ImageUrl = table.Column<string>(type: "text", nullable: true),
                    PublicId = table.Column<string>(type: "text", nullable: true),
                    InsuranceDocument = table.Column<string>(type: "text", nullable: true),
                    VehicleStatus = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Vehicles", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Vehicles_Operators_OperatorId",
                        column: x => x.OperatorId,
                        principalTable: "Operators",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Co_SubscriberInvitees",
                columns: table => new
                {
                    Id = table.Column<string>(type: "text", nullable: false),
                    SubscriberId = table.Column<string>(type: "text", nullable: true),
                    AppUserId = table.Column<string>(type: "text", nullable: true),
                    VehicleId = table.Column<string>(type: "text", nullable: true),
                    FirstName = table.Column<string>(type: "text", nullable: false),
                    LastName = table.Column<string>(type: "text", nullable: false),
                    Email = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Co_SubscriberInvitees", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Co_SubscriberInvitees_AspNetUsers_AppUserId",
                        column: x => x.AppUserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Co_SubscriberInvitees_Subscribers_SubscriberId",
                        column: x => x.SubscriberId,
                        principalTable: "Subscribers",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Co_SubscriberInvitees_Vehicles_VehicleId",
                        column: x => x.VehicleId,
                        principalTable: "Vehicles",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Subscriptions",
                columns: table => new
                {
                    Id = table.Column<string>(type: "text", nullable: false),
                    SubscriberId = table.Column<string>(type: "text", nullable: true),
                    DLUrlFront = table.Column<string>(type: "text", nullable: true),
                    DLUrlBack = table.Column<string>(type: "text", nullable: true),
                    VehicleId = table.Column<string>(type: "text", nullable: true),
                    OperatorId = table.Column<string>(type: "text", nullable: true),
                    RequestDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    PickUpDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    TermStart = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    TermEnd = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    Duration = table.Column<string>(type: "text", nullable: true),
                    PickUpLocation = table.Column<string>(type: "text", nullable: true),
                    SoftCredit = table.Column<bool>(type: "boolean", nullable: false),
                    SubscriptionStatus = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Subscriptions", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Subscriptions_Operators_OperatorId",
                        column: x => x.OperatorId,
                        principalTable: "Operators",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Subscriptions_Subscribers_SubscriberId",
                        column: x => x.SubscriberId,
                        principalTable: "Subscribers",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Subscriptions_Vehicles_VehicleId",
                        column: x => x.VehicleId,
                        principalTable: "Vehicles",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Transactions",
                columns: table => new
                {
                    Id = table.Column<string>(type: "text", nullable: false),
                    SubscriberId = table.Column<string>(type: "text", nullable: true),
                    OperatorId = table.Column<string>(type: "text", nullable: true),
                    VehicleId = table.Column<string>(type: "text", nullable: true),
                    StartDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    EndDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    Amount = table.Column<decimal>(type: "numeric", nullable: false),
                    PaymentStatus = table.Column<string>(type: "text", nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    Name = table.Column<string>(type: "text", nullable: true),
                    TrxRef = table.Column<string>(type: "text", nullable: true),
                    Email = table.Column<string>(type: "text", nullable: true),
                    Status = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Transactions", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Transactions_Operators_OperatorId",
                        column: x => x.OperatorId,
                        principalTable: "Operators",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Transactions_Subscribers_SubscriberId",
                        column: x => x.SubscriberId,
                        principalTable: "Subscribers",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Transactions_Vehicles_VehicleId",
                        column: x => x.VehicleId,
                        principalTable: "Vehicles",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "VehicleRequests",
                columns: table => new
                {
                    Id = table.Column<string>(type: "text", nullable: false),
                    VehicleId = table.Column<string>(type: "text", nullable: true),
                    SubscriberId = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_VehicleRequests", x => x.Id);
                    table.ForeignKey(
                        name: "FK_VehicleRequests_Subscribers_SubscriberId",
                        column: x => x.SubscriberId,
                        principalTable: "Subscribers",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_VehicleRequests_Vehicles_VehicleId",
                        column: x => x.VehicleId,
                        principalTable: "Vehicles",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_AspNetRoleClaims_RoleId",
                table: "AspNetRoleClaims",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "RoleNameIndex",
                table: "AspNetRoles",
                column: "NormalizedName",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserClaims_UserId",
                table: "AspNetUserClaims",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserLogins_UserId",
                table: "AspNetUserLogins",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserRoles_RoleId",
                table: "AspNetUserRoles",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "EmailIndex",
                table: "AspNetUsers",
                column: "NormalizedEmail");

            migrationBuilder.CreateIndex(
                name: "UserNameIndex",
                table: "AspNetUsers",
                column: "NormalizedUserName",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Co_SubscriberInvitees_AppUserId",
                table: "Co_SubscriberInvitees",
                column: "AppUserId");

            migrationBuilder.CreateIndex(
                name: "IX_Co_SubscriberInvitees_SubscriberId",
                table: "Co_SubscriberInvitees",
                column: "SubscriberId");

            migrationBuilder.CreateIndex(
                name: "IX_Co_SubscriberInvitees_VehicleId",
                table: "Co_SubscriberInvitees",
                column: "VehicleId");

            migrationBuilder.CreateIndex(
                name: "IX_OperatorMembers_AppUserId",
                table: "OperatorMembers",
                column: "AppUserId");

            migrationBuilder.CreateIndex(
                name: "IX_OperatorMembers_OperatorId",
                table: "OperatorMembers",
                column: "OperatorId");

            migrationBuilder.CreateIndex(
                name: "IX_Operators_AppUserId1",
                table: "Operators",
                column: "AppUserId1");

            migrationBuilder.CreateIndex(
                name: "IX_Subscribers_AppUserId",
                table: "Subscribers",
                column: "AppUserId");

            migrationBuilder.CreateIndex(
                name: "IX_Subscriptions_OperatorId",
                table: "Subscriptions",
                column: "OperatorId");

            migrationBuilder.CreateIndex(
                name: "IX_Subscriptions_SubscriberId",
                table: "Subscriptions",
                column: "SubscriberId");

            migrationBuilder.CreateIndex(
                name: "IX_Subscriptions_VehicleId",
                table: "Subscriptions",
                column: "VehicleId");

            migrationBuilder.CreateIndex(
                name: "IX_Transactions_OperatorId",
                table: "Transactions",
                column: "OperatorId");

            migrationBuilder.CreateIndex(
                name: "IX_Transactions_SubscriberId",
                table: "Transactions",
                column: "SubscriberId");

            migrationBuilder.CreateIndex(
                name: "IX_Transactions_VehicleId",
                table: "Transactions",
                column: "VehicleId");

            migrationBuilder.CreateIndex(
                name: "IX_VehicleRequests_SubscriberId",
                table: "VehicleRequests",
                column: "SubscriberId");

            migrationBuilder.CreateIndex(
                name: "IX_VehicleRequests_VehicleId",
                table: "VehicleRequests",
                column: "VehicleId");

            migrationBuilder.CreateIndex(
                name: "IX_Vehicles_OperatorId",
                table: "Vehicles",
                column: "OperatorId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AspNetRoleClaims");

            migrationBuilder.DropTable(
                name: "AspNetUserClaims");

            migrationBuilder.DropTable(
                name: "AspNetUserLogins");

            migrationBuilder.DropTable(
                name: "AspNetUserRoles");

            migrationBuilder.DropTable(
                name: "AspNetUserTokens");

            migrationBuilder.DropTable(
                name: "Co_SubscriberInvitees");

            migrationBuilder.DropTable(
                name: "OperatorMembers");

            migrationBuilder.DropTable(
                name: "Subscriptions");

            migrationBuilder.DropTable(
                name: "Transactions");

            migrationBuilder.DropTable(
                name: "VehicleRequests");

            migrationBuilder.DropTable(
                name: "AspNetRoles");

            migrationBuilder.DropTable(
                name: "Subscribers");

            migrationBuilder.DropTable(
                name: "Vehicles");

            migrationBuilder.DropTable(
                name: "Operators");

            migrationBuilder.DropTable(
                name: "AspNetUsers");
        }
    }
}
