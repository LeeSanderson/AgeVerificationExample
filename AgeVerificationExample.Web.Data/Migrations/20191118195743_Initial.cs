using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace AgeVerificationExample.Web.Data.Migrations
{
    public partial class Initial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "ApplicationUserIdentityRoles",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    Name = table.Column<string>(maxLength: 256, nullable: true),
                    NormalizedName = table.Column<string>(maxLength: 256, nullable: true),
                    ConcurrencyStamp = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ApplicationUserIdentityRoles", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ApplicationUsers",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    UserName = table.Column<string>(maxLength: 256, nullable: true),
                    NormalizedUserName = table.Column<string>(maxLength: 256, nullable: true),
                    Email = table.Column<string>(maxLength: 256, nullable: true),
                    NormalizedEmail = table.Column<string>(maxLength: 256, nullable: true),
                    EmailConfirmed = table.Column<bool>(nullable: false),
                    PasswordHash = table.Column<string>(nullable: true),
                    SecurityStamp = table.Column<string>(nullable: true),
                    ConcurrencyStamp = table.Column<string>(nullable: true),
                    PhoneNumber = table.Column<string>(nullable: true),
                    PhoneNumberConfirmed = table.Column<bool>(nullable: false),
                    TwoFactorEnabled = table.Column<bool>(nullable: false),
                    LockoutEnd = table.Column<DateTimeOffset>(nullable: true),
                    LockoutEnabled = table.Column<bool>(nullable: false),
                    AccessFailedCount = table.Column<int>(nullable: false),
                    Name = table.Column<string>(maxLength: 256, nullable: false),
                    DateOfBirth = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ApplicationUsers", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "RegistrationAttempts",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    Name = table.Column<string>(maxLength: 256, nullable: false),
                    Email = table.Column<string>(maxLength: 256, nullable: false),
                    LastAttempt = table.Column<DateTime>(nullable: false),
                    Failures = table.Column<int>(nullable: false),
                    LockedOutDate = table.Column<DateTime>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RegistrationAttempts", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ApplicationUserRoleClaims",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RoleId = table.Column<Guid>(nullable: false),
                    ClaimType = table.Column<string>(nullable: true),
                    ClaimValue = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ApplicationUserRoleClaims", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ApplicationUserRoleClaims_ApplicationUserIdentityRoles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "ApplicationUserIdentityRoles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ApplicationUserClaims",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<Guid>(nullable: false),
                    ClaimType = table.Column<string>(nullable: true),
                    ClaimValue = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ApplicationUserClaims", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ApplicationUserClaims_ApplicationUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "ApplicationUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ApplicationUserIdentityUserLogins",
                columns: table => new
                {
                    LoginProvider = table.Column<string>(nullable: false),
                    ProviderKey = table.Column<string>(nullable: false),
                    ProviderDisplayName = table.Column<string>(nullable: true),
                    UserId = table.Column<Guid>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ApplicationUserIdentityUserLogins", x => new { x.LoginProvider, x.ProviderKey });
                    table.ForeignKey(
                        name: "FK_ApplicationUserIdentityUserLogins_ApplicationUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "ApplicationUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ApplicationUserIdentityUserRoles",
                columns: table => new
                {
                    UserId = table.Column<Guid>(nullable: false),
                    RoleId = table.Column<Guid>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ApplicationUserIdentityUserRoles", x => new { x.UserId, x.RoleId });
                    table.ForeignKey(
                        name: "FK_ApplicationUserIdentityUserRoles_ApplicationUserIdentityRoles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "ApplicationUserIdentityRoles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ApplicationUserIdentityUserRoles_ApplicationUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "ApplicationUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ApplicationUserIdentityUserTokens",
                columns: table => new
                {
                    UserId = table.Column<Guid>(nullable: false),
                    LoginProvider = table.Column<string>(nullable: false),
                    Name = table.Column<string>(nullable: false),
                    Value = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ApplicationUserIdentityUserTokens", x => new { x.UserId, x.LoginProvider, x.Name });
                    table.ForeignKey(
                        name: "FK_ApplicationUserIdentityUserTokens_ApplicationUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "ApplicationUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "LoginAttempts",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    UserId = table.Column<Guid>(nullable: false),
                    AttemptDate = table.Column<DateTime>(nullable: false),
                    Status = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LoginAttempts", x => x.Id);
                    table.ForeignKey(
                        name: "FK_LoginAttempts_UserId",
                        column: x => x.UserId,
                        principalTable: "ApplicationUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "ApplicationUserIdentityClaims_UserId",
                table: "ApplicationUserClaims",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "ApplicationUserIdentityRole_NormalizedName",
                table: "ApplicationUserIdentityRoles",
                column: "NormalizedName",
                unique: true,
                filter: "[NormalizedName] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "ApplicationUserIdentityUserLogin_UserId",
                table: "ApplicationUserIdentityUserLogins",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "ApplicationUserIdentityUserRole_RoleId",
                table: "ApplicationUserIdentityUserRoles",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "ApplicationUserIdentityRoleClaims_RoleId",
                table: "ApplicationUserRoleClaims",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "ApplicationUsers_EmaiIndex",
                table: "ApplicationUsers",
                column: "NormalizedEmail");

            migrationBuilder.CreateIndex(
                name: "ApplicationUsers_UserNameIndex",
                table: "ApplicationUsers",
                column: "NormalizedUserName",
                unique: true,
                filter: "[NormalizedUserName] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_LoginAttempts_UserId",
                table: "LoginAttempts",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "RegistrationAttempts_EmailNameIndex",
                table: "RegistrationAttempts",
                columns: new[] { "Email", "Name" },
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ApplicationUserClaims");

            migrationBuilder.DropTable(
                name: "ApplicationUserIdentityUserLogins");

            migrationBuilder.DropTable(
                name: "ApplicationUserIdentityUserRoles");

            migrationBuilder.DropTable(
                name: "ApplicationUserIdentityUserTokens");

            migrationBuilder.DropTable(
                name: "ApplicationUserRoleClaims");

            migrationBuilder.DropTable(
                name: "LoginAttempts");

            migrationBuilder.DropTable(
                name: "RegistrationAttempts");

            migrationBuilder.DropTable(
                name: "ApplicationUserIdentityRoles");

            migrationBuilder.DropTable(
                name: "ApplicationUsers");
        }
    }
}
