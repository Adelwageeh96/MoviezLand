using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MoviezLand.Web.Data.Migrations
{
    /// <inheritdoc />
    public partial class AddManagerToAllRoles : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql("Insert into AspNetUserRoles (UserId,RoleId) select N'51d02bc4-4e95-4fca-ae32-5f65aa384ccc' , id from AspNetRoles");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql("Delete from AspNetUserRoles where UserId=N'51d02bc4-4e95-4fca-ae32-5f65aa384ccc'");
        }
    }
}
