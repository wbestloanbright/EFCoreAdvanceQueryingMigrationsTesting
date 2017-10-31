using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace Contacts.Migrations
{
    public partial class Initial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            /*
            migrationBuilder.EnsureSchema(
                name: "Contacts");

            migrationBuilder.CreateTable(
                name: "Companies",
                schema: "Contacts",
                columns: table => new
                {
                    CompanyId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    CompanyName = table.Column<string>(type: "varchar(max)", unicode: false, nullable: true),
                    DateAdded = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Companies", x => x.CompanyId);
                });

            migrationBuilder.CreateTable(
                name: "PersonTypes",
                schema: "Contacts",
                columns: table => new
                {
                    PersonTypeId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    PersonTypeName = table.Column<string>(type: "varchar(max)", unicode: false, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PersonTypes", x => x.PersonTypeId);
                });

            migrationBuilder.CreateTable(
                name: "PersonView",
                schema: "Contacts",
                columns: table => new
                {
                    PersonId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    FirstName = table.Column<string>(type: "varchar(max)", unicode: false, nullable: true),
                    LastName = table.Column<string>(type: "varchar(max)", unicode: false, nullable: true),
                    PersonTypeName = table.Column<string>(type: "varchar(max)", unicode: false, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PersonView", x => x.PersonId);
                });

            migrationBuilder.CreateTable(
                name: "People",
                schema: "Contacts",
                columns: table => new
                {
                    PersonId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    BirthDate = table.Column<DateTime>(type: "date", nullable: true, defaultValueSql: "getdate()"),
                    Discriminator = table.Column<string>(type: "varchar(max)", unicode: false, nullable: false),
                    FirstName = table.Column<string>(type: "varchar(40)", unicode: false, maxLength: 40, nullable: false),
                    Height = table.Column<decimal>(type: "decimal(6,2)", nullable: false, defaultValue: 0m),
                    IsActive = table.Column<bool>(type: "bit", nullable: false, defaultValue: true),
                    LastName = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: false),
                    PersonTypeId = table.Column<int>(type: "int", nullable: true),
                    RowVersion = table.Column<byte[]>(type: "rowversion", rowVersion: true, nullable: true),
                    College = table.Column<string>(type: "varchar(max)", unicode: false, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_People", x => x.PersonId);
                    table.ForeignKey(
                        name: "FK_PERSON_PERSONTYPE",
                        column: x => x.PersonTypeId,
                        principalSchema: "Contacts",
                        principalTable: "PersonTypes",
                        principalColumn: "PersonTypeId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "CompanyPersons",
                schema: "Contacts",
                columns: table => new
                {
                    PersonId = table.Column<int>(type: "int", nullable: false),
                    CompanyId = table.Column<int>(type: "int", nullable: false),
                    StartDate = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CompanyPersons", x => new { x.PersonId, x.CompanyId });
                    table.ForeignKey(
                        name: "FK_COMPANYPERSON_COMPANY",
                        column: x => x.CompanyId,
                        principalSchema: "Contacts",
                        principalTable: "Companies",
                        principalColumn: "CompanyId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_COMPANYPERSON_PERSON",
                        column: x => x.PersonId,
                        principalSchema: "Contacts",
                        principalTable: "People",
                        principalColumn: "PersonId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "PersonPhones",
                schema: "Contacts",
                columns: table => new
                {
                    PersonPhoneId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    PersonId = table.Column<int>(type: "int", nullable: false),
                    PhoneNumber = table.Column<string>(type: "varchar(30)", unicode: false, maxLength: 30, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PersonPhones", x => x.PersonPhoneId);
                    table.ForeignKey(
                        name: "FK_PHONE_PERSON",
                        column: x => x.PersonId,
                        principalSchema: "Contacts",
                        principalTable: "People",
                        principalColumn: "PersonId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "PersonResumes",
                schema: "Contacts",
                columns: table => new
                {
                    PersonId = table.Column<int>(type: "int", nullable: false),
                    ResumeText = table.Column<string>(type: "varchar(max)", unicode: false, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PersonResumes", x => x.PersonId);
                    table.ForeignKey(
                        name: "FK_RESUME_PERSON",
                        column: x => x.PersonId,
                        principalSchema: "Contacts",
                        principalTable: "People",
                        principalColumn: "PersonId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_CompanyPersons_CompanyId",
                schema: "Contacts",
                table: "CompanyPersons",
                column: "CompanyId");

            migrationBuilder.CreateIndex(
                name: "IX_LNAME",
                schema: "Contacts",
                table: "People",
                column: "LastName");

            migrationBuilder.CreateIndex(
                name: "IX_People_PersonTypeId",
                schema: "Contacts",
                table: "People",
                column: "PersonTypeId");

            migrationBuilder.CreateIndex(
                name: "UQ_LNAME",
                schema: "Contacts",
                table: "People",
                columns: new[] { "LastName", "FirstName" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_PersonPhones_PersonId",
                schema: "Contacts",
                table: "PersonPhones",
                column: "PersonId");
                */
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            /*
            migrationBuilder.DropTable(
                name: "CompanyPersons",
                schema: "Contacts");

            migrationBuilder.DropTable(
                name: "PersonPhones",
                schema: "Contacts");

            migrationBuilder.DropTable(
                name: "PersonResumes",
                schema: "Contacts");

            migrationBuilder.DropTable(
                name: "PersonView",
                schema: "Contacts");

            migrationBuilder.DropTable(
                name: "Companies",
                schema: "Contacts");

            migrationBuilder.DropTable(
                name: "People",
                schema: "Contacts");

            migrationBuilder.DropTable(
                name: "PersonTypes",
                schema: "Contacts");
                */
        }
    }
}
