using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MoviezLand.Web.Data.Migrations
{
    /// <inheritdoc />
    public partial class AddMangerUserToAllRoles : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql("Insert into AspNetUserRoles (UserId,RoleId) select '36fba988-f1c3-44f5-83e6-2836b890d5a4' , id from AspNetRoles");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql("Delete from AspNetUserRoles where UserId='36fba988-f1c3-44f5-83e6-2836b890d5a4'");
        }
    }
}
