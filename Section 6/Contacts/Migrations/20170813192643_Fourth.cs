using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace Contacts.Migrations
{
    public partial class Fourth : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            // commented out because it was created manually through a test
            //migrationBuilder.InsertData("PersonTypes", new[] { "PersonTypeId",  "PersonTypeName" },
            //    new object[] { 1, "Friend" }, "Contacts");
            migrationBuilder.InsertData("PersonTypes", new[] { "PersonTypeId", "PersonTypeName" },
               new object[] { 2, "Cowrorker" }, "Contacts");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            //migrationBuilder.DeleteData("PersonTypes", "PersonTypeId", 1, "Contacts");
            migrationBuilder.DeleteData("PersonTypes", "PersonTypeId", 2, "Contacts");
        }
    }
}
