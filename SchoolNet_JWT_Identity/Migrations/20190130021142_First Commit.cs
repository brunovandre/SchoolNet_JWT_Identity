using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace SchoolNet_JWT_Identity.Migrations
{
    public partial class FirstCommit : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Course",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Name = table.Column<string>(nullable: true),
                    WorkLoad = table.Column<int>(nullable: false),
                    Price = table.Column<decimal>(nullable: false),
                    Online = table.Column<bool>(nullable: false),
                    Audit_CreationUserId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Audit_CreationDate = table.Column<DateTime>(type: "datetime", nullable: false),
                    Audit_LastModifierUserId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Audit_LastModificationDate = table.Column<DateTime>(type: "datetime", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Course", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Student",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    FullName = table.Column<string>(nullable: true),
                    Age = table.Column<int>(nullable: false),
                    Audit_CreationUserId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Audit_CreationDate = table.Column<DateTime>(type: "datetime", nullable: false),
                    Audit_LastModifierUserId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Audit_LastModificationDate = table.Column<DateTime>(type: "datetime", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Student", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Teacher",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    FirstName = table.Column<string>(nullable: true),
                    LastName = table.Column<string>(nullable: true),
                    Audit_CreationUserId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Audit_CreationDate = table.Column<DateTime>(type: "datetime", nullable: false),
                    Audit_LastModifierUserId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Audit_LastModificationDate = table.Column<DateTime>(type: "datetime", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Teacher", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    Name = table.Column<string>(nullable: true),
                    Email = table.Column<string>(nullable: true),
                    Password = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "StudentClass",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    CourseId = table.Column<int>(nullable: false),
                    TeacherId = table.Column<int>(nullable: false),
                    StudentId = table.Column<int>(nullable: false),
                    Audit_CreationUserId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Audit_CreationDate = table.Column<DateTime>(type: "datetime", nullable: false),
                    Audit_LastModifierUserId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Audit_LastModificationDate = table.Column<DateTime>(type: "datetime", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_StudentClass", x => x.Id);
                    table.ForeignKey(
                        name: "FK_StudentClass_Course_CourseId",
                        column: x => x.CourseId,
                        principalTable: "Course",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_StudentClass_Student_StudentId",
                        column: x => x.StudentId,
                        principalTable: "Student",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_StudentClass_Teacher_TeacherId",
                        column: x => x.TeacherId,
                        principalTable: "Teacher",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_StudentClass_CourseId",
                table: "StudentClass",
                column: "CourseId");

            migrationBuilder.CreateIndex(
                name: "IX_StudentClass_StudentId",
                table: "StudentClass",
                column: "StudentId");

            migrationBuilder.CreateIndex(
                name: "IX_StudentClass_TeacherId",
                table: "StudentClass",
                column: "TeacherId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "StudentClass");

            migrationBuilder.DropTable(
                name: "Users");

            migrationBuilder.DropTable(
                name: "Course");

            migrationBuilder.DropTable(
                name: "Student");

            migrationBuilder.DropTable(
                name: "Teacher");
        }
    }
}
