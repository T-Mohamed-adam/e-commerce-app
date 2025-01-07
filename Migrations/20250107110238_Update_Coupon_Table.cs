using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TagerProject.Migrations
{
    /// <inheritdoc />
    public partial class Update_Coupon_Table : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "NameEr",
                table: "ExpenseItems",
                newName: "NameAr");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "NameAr",
                table: "ExpenseItems",
                newName: "NameEr");
        }
    }
}
