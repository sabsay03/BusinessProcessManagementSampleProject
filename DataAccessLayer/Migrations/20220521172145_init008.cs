using Microsoft.EntityFrameworkCore.Migrations;

namespace DataAccessLayer.Migrations
{
    public partial class init008 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Comments_AspNetUsers_MemberId",
                table: "Comments");

            migrationBuilder.DropIndex(
                name: "IX_Comments_MemberId",
                table: "Comments");

            migrationBuilder.RenameColumn(
                name: "MemberId",
                table: "Comments",
                newName: "commentType");

            migrationBuilder.AlterColumn<int>(
                name: "MissionId",
                table: "Comments",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddColumn<int>(
                name: "ProjectId",
                table: "Comments",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Comments_ProjectId",
                table: "Comments",
                column: "ProjectId");

            migrationBuilder.AddForeignKey(
                name: "FK_Comments_Projects_ProjectId",
                table: "Comments",
                column: "ProjectId",
                principalTable: "Projects",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Comments_Projects_ProjectId",
                table: "Comments");

            migrationBuilder.DropIndex(
                name: "IX_Comments_ProjectId",
                table: "Comments");

            migrationBuilder.DropColumn(
                name: "ProjectId",
                table: "Comments");

            migrationBuilder.RenameColumn(
                name: "commentType",
                table: "Comments",
                newName: "MemberId");

            migrationBuilder.AlterColumn<int>(
                name: "MissionId",
                table: "Comments",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Comments_MemberId",
                table: "Comments",
                column: "MemberId");

            migrationBuilder.AddForeignKey(
                name: "FK_Comments_AspNetUsers_MemberId",
                table: "Comments",
                column: "MemberId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
