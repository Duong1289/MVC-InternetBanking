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
                name: "FK_Accounts_AspNetUsers_InternetBankingUserId",
                table: "Accounts");

            migrationBuilder.DropForeignKey(
                name: "FK_Deposits_Customers_CustomerId",
                table: "Deposits");

            migrationBuilder.DropForeignKey(
                name: "FK_Deposits_Employees_EmployeeId",
                table: "Deposits");

            migrationBuilder.DropForeignKey(
                name: "FK_Withdraws_Customers_CustomerId",
                table: "Withdraws");

            migrationBuilder.DropForeignKey(
                name: "FK_Withdraws_Employees_EmployeeId",
                table: "Withdraws");

            migrationBuilder.DropIndex(
                name: "IX_Accounts_InternetBankingUserId",
                table: "Accounts");

            migrationBuilder.DropColumn(
                name: "InternetBankingUserId",
                table: "Accounts");

            migrationBuilder.AlterColumn<string>(
                name: "EmployeeId",
                table: "Withdraws",
                type: "nvarchar(450)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(450)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "CustomerId",
                table: "Withdraws",
                type: "nvarchar(450)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(450)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "EmployeeId",
                table: "Deposits",
                type: "nvarchar(450)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(450)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "CustomerId",
                table: "Deposits",
                type: "nvarchar(450)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(450)",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Deposits_Customers_CustomerId",
                table: "Deposits",
                column: "CustomerId",
                principalTable: "Customers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Deposits_Employees_EmployeeId",
                table: "Deposits",
                column: "EmployeeId",
                principalTable: "Employees",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Withdraws_Customers_CustomerId",
                table: "Withdraws",
                column: "CustomerId",
                principalTable: "Customers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Withdraws_Employees_EmployeeId",
                table: "Withdraws",
                column: "EmployeeId",
                principalTable: "Employees",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Deposits_Customers_CustomerId",
                table: "Deposits");

            migrationBuilder.DropForeignKey(
                name: "FK_Deposits_Employees_EmployeeId",
                table: "Deposits");

            migrationBuilder.DropForeignKey(
                name: "FK_Withdraws_Customers_CustomerId",
                table: "Withdraws");

            migrationBuilder.DropForeignKey(
                name: "FK_Withdraws_Employees_EmployeeId",
                table: "Withdraws");

            migrationBuilder.AlterColumn<string>(
                name: "EmployeeId",
                table: "Withdraws",
                type: "nvarchar(450)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");

            migrationBuilder.AlterColumn<string>(
                name: "CustomerId",
                table: "Withdraws",
                type: "nvarchar(450)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");

            migrationBuilder.AlterColumn<string>(
                name: "EmployeeId",
                table: "Deposits",
                type: "nvarchar(450)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");

            migrationBuilder.AlterColumn<string>(
                name: "CustomerId",
                table: "Deposits",
                type: "nvarchar(450)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");

            migrationBuilder.AddColumn<string>(
                name: "InternetBankingUserId",
                table: "Accounts",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Accounts_InternetBankingUserId",
                table: "Accounts",
                column: "InternetBankingUserId");

            migrationBuilder.AddForeignKey(
                name: "FK_Accounts_AspNetUsers_InternetBankingUserId",
                table: "Accounts",
                column: "InternetBankingUserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Deposits_Customers_CustomerId",
                table: "Deposits",
                column: "CustomerId",
                principalTable: "Customers",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Deposits_Employees_EmployeeId",
                table: "Deposits",
                column: "EmployeeId",
                principalTable: "Employees",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Withdraws_Customers_CustomerId",
                table: "Withdraws",
                column: "CustomerId",
                principalTable: "Customers",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Withdraws_Employees_EmployeeId",
                table: "Withdraws",
                column: "EmployeeId",
                principalTable: "Employees",
                principalColumn: "Id");
        }
    }
}
