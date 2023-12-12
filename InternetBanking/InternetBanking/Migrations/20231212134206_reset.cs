using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace InternetBanking.Migrations
{
    /// <inheritdoc />
    public partial class reset : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Accounts_Customers_CustomerPersonalId",
                table: "Accounts");

            migrationBuilder.DropForeignKey(
                name: "FK_HelpRequests_Customers_CustomerPersonalId",
                table: "HelpRequests");

            migrationBuilder.DropForeignKey(
                name: "FK_HelpRequests_Employees_EmployeeId",
                table: "HelpRequests");

            migrationBuilder.DropForeignKey(
                name: "FK_Images_Customers_CustomerId",
                table: "Images");

            migrationBuilder.DropForeignKey(
                name: "FK_Images_Employees_EmployeeId",
                table: "Images");

            migrationBuilder.DropForeignKey(
                name: "FK_Loans_Customers_CustomerId",
                table: "Loans");

            migrationBuilder.DropForeignKey(
                name: "FK_Loans_Employees_EmployeeId",
                table: "Loans");

            migrationBuilder.DropForeignKey(
                name: "FK_Services_Customers_CustomerPersonalId",
                table: "Services");

            migrationBuilder.DropForeignKey(
                name: "FK_Services_Employees_EmployeeId",
                table: "Services");

            migrationBuilder.DropTable(
                name: "Customers");

            migrationBuilder.DropTable(
                name: "Employees");

            migrationBuilder.DropIndex(
                name: "IX_Services_CustomerPersonalId",
                table: "Services");

            migrationBuilder.DropIndex(
                name: "IX_Services_EmployeeId",
                table: "Services");

            migrationBuilder.DropIndex(
                name: "IX_Loans_CustomerId",
                table: "Loans");

            migrationBuilder.DropIndex(
                name: "IX_Loans_EmployeeId",
                table: "Loans");

            migrationBuilder.DropIndex(
                name: "IX_Images_CustomerId",
                table: "Images");

            migrationBuilder.DropIndex(
                name: "IX_Images_EmployeeId",
                table: "Images");

            migrationBuilder.DropIndex(
                name: "IX_HelpRequests_CustomerPersonalId",
                table: "HelpRequests");

            migrationBuilder.DropIndex(
                name: "IX_HelpRequests_EmployeeId",
                table: "HelpRequests");

            migrationBuilder.DropIndex(
                name: "IX_Accounts_CustomerPersonalId",
                table: "Accounts");

            migrationBuilder.DropColumn(
                name: "LoanName",
                table: "LoanTypes");

            migrationBuilder.DropColumn(
                name: "FineInterest",
                table: "Loans");

            migrationBuilder.DropColumn(
                name: "Period",
                table: "Loans");

            migrationBuilder.AlterColumn<string>(
                name: "EmployeeId",
                table: "Services",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");

            migrationBuilder.AlterColumn<string>(
                name: "CustomerPersonalId",
                table: "Services",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(20)");

            migrationBuilder.AlterColumn<string>(
                name: "EmployeeId",
                table: "Loans",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "EmployeeId",
                table: "Images",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "EmployeeId",
                table: "HelpRequests",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)",
                oldNullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Answer",
                table: "HelpRequests",
                type: "nvarchar(1000)",
                maxLength: 1000,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "AspNetUserTokens",
                type: "nvarchar(128)",
                maxLength: 128,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");

            migrationBuilder.AlterColumn<string>(
                name: "LoginProvider",
                table: "AspNetUserTokens",
                type: "nvarchar(128)",
                maxLength: 128,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");

            migrationBuilder.AlterColumn<string>(
                name: "ProviderKey",
                table: "AspNetUserLogins",
                type: "nvarchar(128)",
                maxLength: 128,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");

            migrationBuilder.AlterColumn<string>(
                name: "LoginProvider",
                table: "AspNetUserLogins",
                type: "nvarchar(128)",
                maxLength: 128,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Answer",
                table: "HelpRequests");

            migrationBuilder.AlterColumn<string>(
                name: "EmployeeId",
                table: "Services",
                type: "nvarchar(450)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<string>(
                name: "CustomerPersonalId",
                table: "Services",
                type: "nvarchar(20)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AddColumn<string>(
                name: "LoanName",
                table: "LoanTypes",
                type: "nvarchar(200)",
                maxLength: 200,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AlterColumn<string>(
                name: "EmployeeId",
                table: "Loans",
                type: "nvarchar(450)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AddColumn<double>(
                name: "FineInterest",
                table: "Loans",
                type: "float",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<int>(
                name: "Period",
                table: "Loans",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AlterColumn<string>(
                name: "EmployeeId",
                table: "Images",
                type: "nvarchar(450)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "EmployeeId",
                table: "HelpRequests",
                type: "nvarchar(450)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "AspNetUserTokens",
                type: "nvarchar(450)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(128)",
                oldMaxLength: 128);

            migrationBuilder.AlterColumn<string>(
                name: "LoginProvider",
                table: "AspNetUserTokens",
                type: "nvarchar(450)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(128)",
                oldMaxLength: 128);

            migrationBuilder.AlterColumn<string>(
                name: "ProviderKey",
                table: "AspNetUserLogins",
                type: "nvarchar(450)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(128)",
                oldMaxLength: 128);

            migrationBuilder.AlterColumn<string>(
                name: "LoginProvider",
                table: "AspNetUserLogins",
                type: "nvarchar(450)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(128)",
                oldMaxLength: 128);

            migrationBuilder.CreateTable(
                name: "Customers",
                columns: table => new
                {
                    PersonalId = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    Address = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: false),
                    BankId = table.Column<int>(type: "int", nullable: true),
                    BranchId = table.Column<int>(type: "int", nullable: true),
                    Email = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    FirstName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    LastName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Locked = table.Column<bool>(type: "bit", nullable: false),
                    OpenDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Status = table.Column<bool>(type: "bit", nullable: false),
                    Username = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Customers", x => x.PersonalId);
                    table.ForeignKey(
                        name: "FK_Customers_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Customers_Banks_BankId",
                        column: x => x.BankId,
                        principalTable: "Banks",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Employees",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    BankId = table.Column<int>(type: "int", nullable: true),
                    BranchId = table.Column<int>(type: "int", nullable: false),
                    FirstName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    LastName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    ManageBy = table.Column<string>(type: "nvarchar(5)", maxLength: 5, nullable: true),
                    Password = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PersonalId = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    Phone = table.Column<string>(type: "nvarchar(15)", maxLength: 15, nullable: false),
                    RoleId = table.Column<string>(type: "nvarchar(5)", maxLength: 5, nullable: false),
                    Status = table.Column<bool>(type: "bit", nullable: false),
                    Username = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Employees", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Employees_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Employees_Banks_BankId",
                        column: x => x.BankId,
                        principalTable: "Banks",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Employees_Branches_BranchId",
                        column: x => x.BranchId,
                        principalTable: "Branches",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Services_CustomerPersonalId",
                table: "Services",
                column: "CustomerPersonalId");

            migrationBuilder.CreateIndex(
                name: "IX_Services_EmployeeId",
                table: "Services",
                column: "EmployeeId");

            migrationBuilder.CreateIndex(
                name: "IX_Loans_CustomerId",
                table: "Loans",
                column: "CustomerId");

            migrationBuilder.CreateIndex(
                name: "IX_Loans_EmployeeId",
                table: "Loans",
                column: "EmployeeId");

            migrationBuilder.CreateIndex(
                name: "IX_Images_CustomerId",
                table: "Images",
                column: "CustomerId");

            migrationBuilder.CreateIndex(
                name: "IX_Images_EmployeeId",
                table: "Images",
                column: "EmployeeId");

            migrationBuilder.CreateIndex(
                name: "IX_HelpRequests_CustomerPersonalId",
                table: "HelpRequests",
                column: "CustomerPersonalId");

            migrationBuilder.CreateIndex(
                name: "IX_HelpRequests_EmployeeId",
                table: "HelpRequests",
                column: "EmployeeId");

            migrationBuilder.CreateIndex(
                name: "IX_Accounts_CustomerPersonalId",
                table: "Accounts",
                column: "CustomerPersonalId");

            migrationBuilder.CreateIndex(
                name: "IX_Customers_BankId",
                table: "Customers",
                column: "BankId");

            migrationBuilder.CreateIndex(
                name: "IX_Customers_UserId",
                table: "Customers",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_Employees_BankId",
                table: "Employees",
                column: "BankId");

            migrationBuilder.CreateIndex(
                name: "IX_Employees_BranchId",
                table: "Employees",
                column: "BranchId");

            migrationBuilder.CreateIndex(
                name: "IX_Employees_UserId",
                table: "Employees",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_Accounts_Customers_CustomerPersonalId",
                table: "Accounts",
                column: "CustomerPersonalId",
                principalTable: "Customers",
                principalColumn: "PersonalId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_HelpRequests_Customers_CustomerPersonalId",
                table: "HelpRequests",
                column: "CustomerPersonalId",
                principalTable: "Customers",
                principalColumn: "PersonalId");

            migrationBuilder.AddForeignKey(
                name: "FK_HelpRequests_Employees_EmployeeId",
                table: "HelpRequests",
                column: "EmployeeId",
                principalTable: "Employees",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Images_Customers_CustomerId",
                table: "Images",
                column: "CustomerId",
                principalTable: "Customers",
                principalColumn: "PersonalId");

            migrationBuilder.AddForeignKey(
                name: "FK_Images_Employees_EmployeeId",
                table: "Images",
                column: "EmployeeId",
                principalTable: "Employees",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Loans_Customers_CustomerId",
                table: "Loans",
                column: "CustomerId",
                principalTable: "Customers",
                principalColumn: "PersonalId");

            migrationBuilder.AddForeignKey(
                name: "FK_Loans_Employees_EmployeeId",
                table: "Loans",
                column: "EmployeeId",
                principalTable: "Employees",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Services_Customers_CustomerPersonalId",
                table: "Services",
                column: "CustomerPersonalId",
                principalTable: "Customers",
                principalColumn: "PersonalId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Services_Employees_EmployeeId",
                table: "Services",
                column: "EmployeeId",
                principalTable: "Employees",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
