using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace classwork.Migrations
{
    public partial class firstMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "UploadedBy",
                table: "FileOnFileSystemModels",
                type: "longtext",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "longtext")
                .Annotation("MySql:CharSet", "utf8mb4")
                .OldAnnotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AlterColumn<string>(
                name: "UploadedBy",
                table: "FileOnDatabaseModels",
                type: "longtext",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "longtext")
                .Annotation("MySql:CharSet", "utf8mb4")
                .OldAnnotation("MySql:CharSet", "utf8mb4");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "FileOnFileSystemModels",
                keyColumn: "UploadedBy",
                keyValue: null,
                column: "UploadedBy",
                value: "");

            migrationBuilder.AlterColumn<string>(
                name: "UploadedBy",
                table: "FileOnFileSystemModels",
                type: "longtext",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "longtext",
                oldNullable: true)
                .Annotation("MySql:CharSet", "utf8mb4")
                .OldAnnotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.UpdateData(
                table: "FileOnDatabaseModels",
                keyColumn: "UploadedBy",
                keyValue: null,
                column: "UploadedBy",
                value: "");

            migrationBuilder.AlterColumn<string>(
                name: "UploadedBy",
                table: "FileOnDatabaseModels",
                type: "longtext",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "longtext",
                oldNullable: true)
                .Annotation("MySql:CharSet", "utf8mb4")
                .OldAnnotation("MySql:CharSet", "utf8mb4");
        }
    }
}
