using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Viamatica.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class MoveAttentionTypeToTurn : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Attentions_AttentionTypes_AttentionTypeAttentionTypeId",
                table: "Attentions");

            migrationBuilder.DropIndex(
                name: "IX_Attentions_AttentionTypeAttentionTypeId",
                table: "Attentions");

            migrationBuilder.DropColumn(
                name: "AttentionTypeAttentionTypeId",
                table: "Attentions");

            migrationBuilder.AlterColumn<string>(
                name: "Description",
                table: "Turns",
                type: "nvarchar(6)",
                maxLength: 6,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AddColumn<int>(
                name: "AttentionId",
                table: "Turns",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "AttentionTypeId",
                table: "Turns",
                type: "nvarchar(450)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateIndex(
                name: "IX_Turns_AttentionTypeId",
                table: "Turns",
                column: "AttentionTypeId");

            migrationBuilder.AddForeignKey(
                name: "FK_Turns_AttentionTypes_AttentionTypeId",
                table: "Turns",
                column: "AttentionTypeId",
                principalTable: "AttentionTypes",
                principalColumn: "AttentionTypeId",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Turns_AttentionTypes_AttentionTypeId",
                table: "Turns");

            migrationBuilder.DropIndex(
                name: "IX_Turns_AttentionTypeId",
                table: "Turns");

            migrationBuilder.DropColumn(
                name: "AttentionId",
                table: "Turns");

            migrationBuilder.DropColumn(
                name: "AttentionTypeId",
                table: "Turns");

            migrationBuilder.AlterColumn<string>(
                name: "Description",
                table: "Turns",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(6)",
                oldMaxLength: 6);

            migrationBuilder.AddColumn<string>(
                name: "AttentionTypeAttentionTypeId",
                table: "Attentions",
                type: "nvarchar(450)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateIndex(
                name: "IX_Attentions_AttentionTypeAttentionTypeId",
                table: "Attentions",
                column: "AttentionTypeAttentionTypeId");

            migrationBuilder.AddForeignKey(
                name: "FK_Attentions_AttentionTypes_AttentionTypeAttentionTypeId",
                table: "Attentions",
                column: "AttentionTypeAttentionTypeId",
                principalTable: "AttentionTypes",
                principalColumn: "AttentionTypeId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
