using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace InternetBanking.Migrations
{
    /// <inheritdoc />
    public partial class v2 : Migration
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
                name: "FK_HelpRequests_Employees_EmployeeEmpId",
                table: "HelpRequests");

            migrationBuilder.DropForeignKey(
                name: "FK_Images_Customers_CustomerPersonalId",
                table: "Images");

            migrationBuilder.DropForeignKey(
                name: "FK_Images_Employees_EmployeeEmpId",
                table: "Images");

            migrationBuilder.DropForeignKey(
                name: "FK_Loans_Customers_CustomerPersonalId",
                table: "Loans");

            migrationBuilder.DropForeignKey(
                name: "FK_Loans_Employees_EmployeeEmpId",
                table: "Loans");

            migrationBuilder.DropForeignKey(
                name: "FK_Services_Customers_CustomerPersonalId",
                table: "Services");

            migrationBuilder.DropForeignKey(
                name: "FK_Services_Employees_EmployeeEmpId",
                table: "Services");

            migrationBuilder.DropIndex(
                name: "IX_Services_EmployeeEmpId",
                table: "Services");

            migrationBuilder.DropIndex(
                name: "IX_HelpRequests_CustomerPersonalId",
                table: "HelpRequests");

            migrationBuilder.DropIndex(
                name: "IX_HelpRequests_EmployeeEmpId",
                table: "HelpRequests");

            migrationBuilder.DropColumn(
                name: "EmployeeEmpId",
                table: "Services");

            migrationBuilder.DropColumn(
                name: "CustomerPersonalId",
                table: "HelpRequests");

            migrationBuilder.DropColumn(
                name: "EmployeeEmpId",
                table: "HelpRequests");

            migrationBuilder.RenameColumn(
                name: "CustomerPersonalId",
                table: "Services",
                newName: "PersonalId");

            migrationBuilder.RenameIndex(
                name: "IX_Services_CustomerPersonalId",
                table: "Services",
                newName: "IX_Services_PersonalId");

            migrationBuilder.RenameColumn(
                name: "EmployeeEmpId",
                table: "Loans",
                newName: "PersonalId");

            migrationBuilder.RenameColumn(
                name: "CustomerPersonalId",
                table: "Loans",
                newName: "EmpId");

            migrationBuilder.RenameIndex(
                name: "IX_Loans_EmployeeEmpId",
                table: "Loans",
                newName: "IX_Loans_PersonalId");

            migrationBuilder.RenameIndex(
                name: "IX_Loans_CustomerPersonalId",
                table: "Loans",
                newName: "IX_Loans_EmpId");

            migrationBuilder.RenameColumn(
                name: "EmployeeEmpId",
                table: "Images",
                newName: "PersonalId");

            migrationBuilder.RenameColumn(
                name: "CustomerPersonalId",
                table: "Images",
                newName: "EmpId");

            migrationBuilder.RenameIndex(
                name: "IX_Images_EmployeeEmpId",
                table: "Images",
                newName: "IX_Images_PersonalId");

            migrationBuilder.RenameIndex(
                name: "IX_Images_CustomerPersonalId",
                table: "Images",
                newName: "IX_Images_EmpId");

            migrationBuilder.RenameColumn(
                name: "CustomerPersonalId",
                table: "Accounts",
                newName: "PersonalId");

            migrationBuilder.RenameIndex(
                name: "IX_Accounts_CustomerPersonalId",
                table: "Accounts",
                newName: "IX_Accounts_PersonalId");

            migrationBuilder.AlterColumn<string>(
                name: "PersonalId",
                table: "Services",
                type: "nvarchar(450)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(450)",
                oldNullable: true);

            migrationBuilder.AddColumn<string>(
                name: "EmpId",
                table: "Services",
                type: "nvarchar(450)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "EmpId",
                table: "HelpRequests",
                type: "nvarchar(450)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "PersonalId",
                table: "HelpRequests",
                type: "nvarchar(450)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateIndex(
                name: "IX_Services_EmpId",
                table: "Services",
                column: "EmpId");

            migrationBuilder.CreateIndex(
                name: "IX_HelpRequests_EmpId",
                table: "HelpRequests",
                column: "EmpId");

            migrationBuilder.CreateIndex(
                name: "IX_HelpRequests_PersonalId",
                table: "HelpRequests",
                column: "PersonalId");

            migrationBuilder.AddForeignKey(
                name: "FK_Accounts_Customers_PersonalId",
                table: "Accounts",
                column: "PersonalId",
                principalTable: "Customers",
                principalColumn: "PersonalId");

            migrationBuilder.AddForeignKey(
                name: "FK_HelpRequests_Customers_PersonalId",
                table: "HelpRequests",
                column: "PersonalId",
                principalTable: "Customers",
                principalColumn: "PersonalId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_HelpRequests_Employees_EmpId",
                table: "HelpRequests",
                column: "EmpId",
                principalTable: "Employees",
                principalColumn: "EmpId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Images_Customers_PersonalId",
                table: "Images",
                column: "PersonalId",
                principalTable: "Customers",
                principalColumn: "PersonalId");

            migrationBuilder.AddForeignKey(
                name: "FK_Images_Employees_EmpId",
                table: "Images",
                column: "EmpId",
                principalTable: "Employees",
                principalColumn: "EmpId");

            migrationBuilder.AddForeignKey(
                name: "FK_Loans_Customers_PersonalId",
                table: "Loans",
                column: "PersonalId",
                principalTable: "Customers",
                principalColumn: "PersonalId");

            migrationBuilder.AddForeignKey(
                name: "FK_Loans_Employees_EmpId",
                table: "Loans",
                column: "EmpId",
                principalTable: "Employees",
                principalColumn: "EmpId");

            migrationBuilder.AddForeignKey(
                name: "FK_Services_Customers_PersonalId",
                table: "Services",
                column: "PersonalId",
                principalTable: "Customers",
                principalColumn: "PersonalId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Services_Employees_EmpId",
                table: "Services",
                column: "EmpId",
                principalTable: "Employees",
                principalColumn: "EmpId",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Accounts_Customers_PersonalId",
                table: "Accounts");

            migrationBuilder.DropForeignKey(
                name: "FK_HelpRequests_Customers_PersonalId",
                table: "HelpRequests");

            migrationBuilder.DropForeignKey(
                name: "FK_HelpRequests_Employees_EmpId",
                table: "HelpRequests");

            migrationBuilder.DropForeignKey(
                name: "FK_Images_Customers_PersonalId",
                table: "Images");

            migrationBuilder.DropForeignKey(
                name: "FK_Images_Employees_EmpId",
                table: "Images");

            migrationBuilder.DropForeignKey(
                name: "FK_Loans_Customers_PersonalId",
                table: "Loans");

            migrationBuilder.DropForeignKey(
                name: "FK_Loans_Employees_EmpId",
                table: "Loans");

            migrationBuilder.DropForeignKey(
                name: "FK_Services_Customers_PersonalId",
                table: "Services");

            migrationBuilder.DropForeignKey(
                name: "FK_Services_Employees_EmpId",
                table: "Services");

            migrationBuilder.DropIndex(
                name: "IX_Services_EmpId",
                table: "Services");

            migrationBuilder.DropIndex(
                name: "IX_HelpRequests_EmpId",
                table: "HelpRequests");

            migrationBuilder.DropIndex(
                name: "IX_HelpRequests_PersonalId",
                table: "HelpRequests");

            migrationBuilder.DropColumn(
                name: "EmpId",
                table: "Services");

            migrationBuilder.DropColumn(
                name: "EmpId",
                table: "HelpRequests");

            migrationBuilder.DropColumn(
                name: "PersonalId",
                table: "HelpRequests");

            migrationBuilder.RenameColumn(
                name: "PersonalId",
                table: "Services",
                newName: "CustomerPersonalId");

            migrationBuilder.RenameIndex(
                name: "IX_Services_PersonalId",
                table: "Services",
                newName: "IX_Services_CustomerPersonalId");

            migrationBuilder.RenameColumn(
                name: "PersonalId",
                table: "Loans",
                newName: "EmployeeEmpId");

            migrationBuilder.RenameColumn(
                name: "EmpId",
                table: "Loans",
                newName: "CustomerPersonalId");

            migrationBuilder.RenameIndex(
                name: "IX_Loans_PersonalId",
                table: "Loans",
                newName: "IX_Loans_EmployeeEmpId");

            migrationBuilder.RenameIndex(
                name: "IX_Loans_EmpId",
                table: "Loans",
                newName: "IX_Loans_CustomerPersonalId");

            migrationBuilder.RenameColumn(
                name: "PersonalId",
                table: "Images",
                newName: "EmployeeEmpId");

            migrationBuilder.RenameColumn(
                name: "EmpId",
                table: "Images",
                newName: "CustomerPersonalId");

            migrationBuilder.RenameIndex(
                name: "IX_Images_PersonalId",
                table: "Images",
                newName: "IX_Images_EmployeeEmpId");

            migrationBuilder.RenameIndex(
                name: "IX_Images_EmpId",
                table: "Images",
                newName: "IX_Images_CustomerPersonalId");

            migrationBuilder.RenameColumn(
                name: "PersonalId",
                table: "Accounts",
                newName: "CustomerPersonalId");

            migrationBuilder.RenameIndex(
                name: "IX_Accounts_PersonalId",
                table: "Accounts",
                newName: "IX_Accounts_CustomerPersonalId");

            migrationBuilder.AlterColumn<string>(
                name: "CustomerPersonalId",
                table: "Services",
                type: "nvarchar(450)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");

            migrationBuilder.AddColumn<string>(
                name: "EmployeeEmpId",
                table: "Services",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "CustomerPersonalId",
                table: "HelpRequests",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "EmployeeEmpId",
                table: "HelpRequests",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Services_EmployeeEmpId",
                table: "Services",
                column: "EmployeeEmpId");

            migrationBuilder.CreateIndex(
                name: "IX_HelpRequests_CustomerPersonalId",
                table: "HelpRequests",
                column: "CustomerPersonalId");

            migrationBuilder.CreateIndex(
                name: "IX_HelpRequests_EmployeeEmpId",
                table: "HelpRequests",
                column: "EmployeeEmpId");

            migrationBuilder.AddForeignKey(
                name: "FK_Accounts_Customers_CustomerPersonalId",
                table: "Accounts",
                column: "CustomerPersonalId",
                principalTable: "Customers",
                principalColumn: "PersonalId");

            migrationBuilder.AddForeignKey(
                name: "FK_HelpRequests_Customers_CustomerPersonalId",
                table: "HelpRequests",
                column: "CustomerPersonalId",
                principalTable: "Customers",
                principalColumn: "PersonalId");

            migrationBuilder.AddForeignKey(
                name: "FK_HelpRequests_Employees_EmployeeEmpId",
                table: "HelpRequests",
                column: "EmployeeEmpId",
                principalTable: "Employees",
                principalColumn: "EmpId");

            migrationBuilder.AddForeignKey(
                name: "FK_Images_Customers_CustomerPersonalId",
                table: "Images",
                column: "CustomerPersonalId",
                principalTable: "Customers",
                principalColumn: "PersonalId");

            migrationBuilder.AddForeignKey(
                name: "FK_Images_Employees_EmployeeEmpId",
                table: "Images",
                column: "EmployeeEmpId",
                principalTable: "Employees",
                principalColumn: "EmpId");

            migrationBuilder.AddForeignKey(
                name: "FK_Loans_Customers_CustomerPersonalId",
                table: "Loans",
                column: "CustomerPersonalId",
                principalTable: "Customers",
                principalColumn: "PersonalId");

            migrationBuilder.AddForeignKey(
                name: "FK_Loans_Employees_EmployeeEmpId",
                table: "Loans",
                column: "EmployeeEmpId",
                principalTable: "Employees",
                principalColumn: "EmpId");

            migrationBuilder.AddForeignKey(
                name: "FK_Services_Customers_CustomerPersonalId",
                table: "Services",
                column: "CustomerPersonalId",
                principalTable: "Customers",
                principalColumn: "PersonalId");

            migrationBuilder.AddForeignKey(
                name: "FK_Services_Employees_EmployeeEmpId",
                table: "Services",
                column: "EmployeeEmpId",
                principalTable: "Employees",
                principalColumn: "EmpId");
        }
    }
}
