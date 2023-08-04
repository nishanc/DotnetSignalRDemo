using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ServerApp.Migrations
{
    /// <inheritdoc />
    public partial class SeedUserGroups : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql(@"
                INSERT INTO UserGroups (GroupName) VALUES ('Administrator'), ('Normal User');
            ");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {

        }
    }
}
