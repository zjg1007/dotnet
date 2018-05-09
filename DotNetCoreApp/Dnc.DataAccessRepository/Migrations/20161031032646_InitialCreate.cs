using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Dnc.DataAccessRepository.Migrations
{
    public partial class InitialCreate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "ApplicationGroups",
                columns: table => new
                {
                    ID = table.Column<Guid>(nullable: false),
                    Decription = table.Column<string>(maxLength: 500, nullable: true),
                    Name = table.Column<string>(maxLength: 20, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ApplicationGroups", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "ArticleTypes",
                columns: table => new
                {
                    ID = table.Column<Guid>(nullable: false),
                    Decription = table.Column<string>(maxLength: 500, nullable: true),
                    Name = table.Column<string>(maxLength: 20, nullable: true),
                    SortCode = table.Column<string>(maxLength: 50, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ArticleTypes", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "Persons",
                columns: table => new
                {
                    ID = table.Column<Guid>(nullable: false),
                    BirthDay = table.Column<DateTime>(nullable: false),
                    Description = table.Column<string>(maxLength: 500, nullable: true),
                    Name = table.Column<string>(maxLength: 20, nullable: true),
                    Sex = table.Column<bool>(nullable: false),
                    SortCode = table.Column<string>(maxLength: 50, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Persons", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "ApplicationUsers",
                columns: table => new
                {
                    ID = table.Column<Guid>(nullable: false),
                    ApplicationGroupID = table.Column<Guid>(nullable: false),
                    Name = table.Column<string>(maxLength: 20, nullable: true),
                    Password = table.Column<string>(maxLength: 20, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ApplicationUsers", x => x.ID);
                    table.ForeignKey(
                        name: "FK_ApplicationUsers_ApplicationGroups_ApplicationGroupID",
                        column: x => x.ApplicationGroupID,
                        principalTable: "ApplicationGroups",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Articles",
                columns: table => new
                {
                    ID = table.Column<Guid>(nullable: false),
                    ArticleTypeID = table.Column<Guid>(nullable: true),
                    Content = table.Column<string>(nullable: true),
                    Name = table.Column<string>(maxLength: 50, nullable: true),
                    PublishDateTime = table.Column<DateTime>(nullable: false),
                    SubName = table.Column<string>(maxLength: 100, nullable: true),
                    TitleImage = table.Column<string>(maxLength: 255, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Articles", x => x.ID);
                    table.ForeignKey(
                        name: "FK_Articles_ArticleTypes_ArticleTypeID",
                        column: x => x.ArticleTypeID,
                        principalTable: "ArticleTypes",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ApplicationUsers_ApplicationGroupID",
                table: "ApplicationUsers",
                column: "ApplicationGroupID");

            migrationBuilder.CreateIndex(
                name: "IX_Articles_ArticleTypeID",
                table: "Articles",
                column: "ArticleTypeID");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ApplicationUsers");

            migrationBuilder.DropTable(
                name: "Articles");

            migrationBuilder.DropTable(
                name: "Persons");

            migrationBuilder.DropTable(
                name: "ApplicationGroups");

            migrationBuilder.DropTable(
                name: "ArticleTypes");
        }
    }
}
