﻿using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TagerProject.Migrations
{
    /// <inheritdoc />
    public partial class Update_Owners_Table : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ConfirmPassword",
                table: "Owners");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "ConfirmPassword",
                table: "Owners",
                type: "nvarchar(max)",
                nullable: true);
        }
    }
}
