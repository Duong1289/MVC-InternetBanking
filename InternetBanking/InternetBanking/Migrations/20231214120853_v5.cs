using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace InternetBanking.Migrations
{
    /// <inheritdoc />
    public partial class v5 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Accounts_Customer_CustomerPersonalId",
                table: "Accounts");

            migrationBuilder.DropForeignKey(
                name: "FK_Customer_AspNetUsers_PersonalId",
                table: "Customer");

            migrationBuilder.DropForeignKey(
                name: "FK_Employee_AspNetUsers_EmpId",
                table: "Employee");

            migrationBuilder.DropForeignKey(
                name: "FK_FAQ_FAQCategories_FAQCategoryId",
                table: "FAQ");

            migrationBuilder.DropForeignKey(
                name: "FK_HelpRequests_Customer_CustomerId",
                table: "HelpRequests");

            migrationBuilder.DropForeignKey(
                name: "FK_HelpRequests_Employee_EmployeeId",
                table: "HelpRequests");

            migrationBuilder.DropForeignKey(
                name: "FK_Images_Customer_CustomerId",
                table: "Images");

            migrationBuilder.DropForeignKey(
                name: "FK_Images_Employee_EmployeeId",
                table: "Images");

            migrationBuilder.DropForeignKey(
                name: "FK_Loans_Customer_CustomerId",
                table: "Loans");

            migrationBuilder.DropForeignKey(
                name: "FK_Loans_Employee_EmployeeId",
                table: "Loans");

            migrationBuilder.DropForeignKey(
                name: "FK_Services_Customer_CustomerPersonalId",
                table: "Services");

            migrationBuilder.DropForeignKey(
                name: "FK_Services_Employee_EmployeeId",
                table: "Services");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Employee",
                table: "Employee");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Customer",
                table: "Customer");

            migrationBuilder.RenameTable(
                name: "Employee",
                newName: "Employees");

            migrationBuilder.RenameTable(
                name: "Customer",
                newName: "Customers");

            migrationBuilder.AlterColumn<string>(
                name: "Question",
                table: "FAQ",
                type: "nvarchar(300)",
                maxLength: 300,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(300)",
                oldMaxLength: 300);

            migrationBuilder.AlterColumn<int>(
                name: "FAQCategoryId",
                table: "FAQ",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<string>(
                name: "Answer",
                table: "FAQ",
                type: "nvarchar(1000)",
                maxLength: 1000,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(1000)",
                oldMaxLength: 1000);

            migrationBuilder.AddPrimaryKey(
                name: "PK_Employees",
                table: "Employees",
                column: "EmpId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Customers",
                table: "Customers",
                column: "PersonalId");

            migrationBuilder.AddForeignKey(
                name: "FK_Accounts_Customers_CustomerPersonalId",
                table: "Accounts",
                column: "CustomerPersonalId",
                principalTable: "Customers",
                principalColumn: "PersonalId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Customers_AspNetUsers_PersonalId",
                table: "Customers",
                column: "PersonalId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Employees_AspNetUsers_EmpId",
                table: "Employees",
                column: "EmpId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_FAQ_FAQCategories_FAQCategoryId",
                table: "FAQ",
                column: "FAQCategoryId",
                principalTable: "FAQCategories",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_HelpRequests_Customers_CustomerId",
                table: "HelpRequests",
                column: "CustomerId",
                principalTable: "Customers",
                principalColumn: "PersonalId");

            migrationBuilder.AddForeignKey(
                name: "FK_HelpRequests_Employees_EmployeeId",
                table: "HelpRequests",
                column: "EmployeeId",
                principalTable: "Employees",
                principalColumn: "EmpId");

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
                principalColumn: "EmpId");

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
                principalColumn: "EmpId");

            migrationBuilder.AddForeignKey(
                name: "FK_Services_Customers_CustomerPersonalId",
                table: "Services",
                column: "CustomerPersonalId",
                principalTable: "Customers",
                principalColumn: "PersonalId");

            migrationBuilder.AddForeignKey(
                name: "FK_Services_Employees_EmployeeId",
                table: "Services",
                column: "EmployeeId",
                principalTable: "Employees",
                principalColumn: "EmpId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Accounts_Customers_CustomerPersonalId",
                table: "Accounts");

            migrationBuilder.DropForeignKey(
                name: "FK_Customers_AspNetUsers_PersonalId",
                table: "Customers");

            migrationBuilder.DropForeignKey(
                name: "FK_Employees_AspNetUsers_EmpId",
                table: "Employees");

            migrationBuilder.DropForeignKey(
                name: "FK_FAQ_FAQCategories_FAQCategoryId",
                table: "FAQ");

            migrationBuilder.DropForeignKey(
                name: "FK_HelpRequests_Customers_CustomerId",
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

            migrationBuilder.DropPrimaryKey(
                name: "PK_Employees",
                table: "Employees");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Customers",
                table: "Customers");

            migrationBuilder.RenameTable(
                name: "Employees",
                newName: "Employee");

            migrationBuilder.RenameTable(
                name: "Customers",
                newName: "Customer");

            migrationBuilder.AlterColumn<string>(
                name: "Question",
                table: "FAQ",
                type: "nvarchar(300)",
                maxLength: 300,
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(300)",
                oldMaxLength: 300,
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "FAQCategoryId",
                table: "FAQ",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Answer",
                table: "FAQ",
                type: "nvarchar(1000)",
                maxLength: 1000,
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(1000)",
                oldMaxLength: 1000,
                oldNullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_Employee",
                table: "Employee",
                column: "EmpId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Customer",
                table: "Customer",
                column: "PersonalId");

            migrationBuilder.AddForeignKey(
                name: "FK_Accounts_Customer_CustomerPersonalId",
                table: "Accounts",
                column: "CustomerPersonalId",
                principalTable: "Customer",
                principalColumn: "PersonalId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Customer_AspNetUsers_PersonalId",
                table: "Customer",
                column: "PersonalId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Employee_AspNetUsers_EmpId",
                table: "Employee",
                column: "EmpId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_FAQ_FAQCategories_FAQCategoryId",
                table: "FAQ",
                column: "FAQCategoryId",
                principalTable: "FAQCategories",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_HelpRequests_Customer_CustomerId",
                table: "HelpRequests",
                column: "CustomerId",
                principalTable: "Customer",
                principalColumn: "PersonalId");

            migrationBuilder.AddForeignKey(
                name: "FK_HelpRequests_Employee_EmployeeId",
                table: "HelpRequests",
                column: "EmployeeId",
                principalTable: "Employee",
                principalColumn: "EmpId");

            migrationBuilder.AddForeignKey(
                name: "FK_Images_Customer_CustomerId",
                table: "Images",
                column: "CustomerId",
                principalTable: "Customer",
                principalColumn: "PersonalId");

            migrationBuilder.AddForeignKey(
                name: "FK_Images_Employee_EmployeeId",
                table: "Images",
                column: "EmployeeId",
                principalTable: "Employee",
                principalColumn: "EmpId");

            migrationBuilder.AddForeignKey(
                name: "FK_Loans_Customer_CustomerId",
                table: "Loans",
                column: "CustomerId",
                principalTable: "Customer",
                principalColumn: "PersonalId");

            migrationBuilder.AddForeignKey(
                name: "FK_Loans_Employee_EmployeeId",
                table: "Loans",
                column: "EmployeeId",
                principalTable: "Employee",
                principalColumn: "EmpId");

            migrationBuilder.AddForeignKey(
                name: "FK_Services_Customer_CustomerPersonalId",
                table: "Services",
                column: "CustomerPersonalId",
                principalTable: "Customer",
                principalColumn: "PersonalId");

            migrationBuilder.AddForeignKey(
                name: "FK_Services_Employee_EmployeeId",
                table: "Services",
                column: "EmployeeId",
                principalTable: "Employee",
                principalColumn: "EmpId");
        }
    }
}
