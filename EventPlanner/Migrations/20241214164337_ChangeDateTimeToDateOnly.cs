using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EventPlanner.Migrations
{
    /// <inheritdoc />
    public partial class ChangeDateTimeToDateOnly : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Gatherings_Categories_GatheringId",
                table: "Gatherings");

            migrationBuilder.AlterColumn<DateOnly>(
                name: "GatheringStart",
                table: "Gatherings",
                type: "date",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "datetime2");

            migrationBuilder.AlterColumn<DateOnly>(
                name: "GatheringEnd",
                table: "Gatherings",
                type: "date",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "datetime2");

            migrationBuilder.DropColumn(
                name: "GatheringId",
                table: "Gatherings"
                );

            migrationBuilder.AddColumn<int>(
                name: "GatheringId",
                table: "Gatherings",
                type: "int",
                nullable: false,
                defaultValue : 0)
                .Annotation("SqlServer:Identity", "1, 1");

            migrationBuilder.AddColumn<int>(
                name: "CategoryId",
                table: "Gatherings",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Gatherings_CategoryId",
                table: "Gatherings",
                column: "CategoryId");

            migrationBuilder.AddForeignKey(
                name: "FK_Gatherings_Categories_CategoryId",
                table: "Gatherings",
                column: "CategoryId",
                principalTable: "Categories",
                principalColumn: "CategoryId",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Gatherings_Categories_CategoryId",
                table: "Gatherings");

            migrationBuilder.DropIndex(
                name: "IX_Gatherings_CategoryId",
                table: "Gatherings");

            migrationBuilder.DropColumn(
                name: "CategoryId",
                table: "Gatherings");

            migrationBuilder.AlterColumn<DateTime>(
                name: "GatheringStart",
                table: "Gatherings",
                type: "datetime2",
                nullable: false,
                oldClrType: typeof(DateOnly),
                oldType: "date");

            migrationBuilder.AlterColumn<DateTime>(
                name: "GatheringEnd",
                table: "Gatherings",
                type: "datetime2",
                nullable: false,
                oldClrType: typeof(DateOnly),
                oldType: "date");

            migrationBuilder.AlterColumn<int>(
                name: "GatheringId",
                table: "Gatherings",
                type: "int",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int")
                .OldAnnotation("SqlServer:Identity", "1, 1");

            migrationBuilder.AddForeignKey(
                name: "FK_Gatherings_Categories_GatheringId",
                table: "Gatherings",
                column: "GatheringId",
                principalTable: "Categories",
                principalColumn: "CategoryId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
