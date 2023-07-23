using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MoviezLand.Web.Data.Migrations
{
    /// <inheritdoc />
    public partial class InsertManagerUser : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql("INSERT INTO [dbo].[AspNetUsers] ([Id], [UserName], [NormalizedUserName], [Email], [NormalizedEmail], [EmailConfirmed], [PasswordHash], [SecurityStamp], [ConcurrencyStamp], [PhoneNumber], [PhoneNumberConfirmed], [TwoFactorEnabled], [LockoutEnd], [LockoutEnabled], [AccessFailedCount], [FirstName], [LastName], [ProfilePicture], [BirthDate]) VALUES (N'51d02bc4-4e95-4fca-ae32-5f65aa384ccc', N'Manager', N'MANAGER', N'Manager@manager.com', N'MANAGER@MANAGER.COM', 0, N'AQAAAAIAAYagAAAAEAl4+/KlFPTawFKnYRbPR8qQcMYb0caaY+cn7GkwAUPtKwS454cidhUY96NinUao2g==', N'ESDBYFWUMCFS46WWNABBCIUJPLWCRJEH', N'e64a5ac3-d8f0-4598-aaa8-941004ac7ec0', N'11111111111', 0, 0, NULL, 1, 0, N'Adel', N'Wageeh', NULL, N'2002-04-17 00:00:00')\r\n");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql("Delete from [dbo].[AspNetUsers] where Id=N'51d02bc4-4e95-4fca-ae32-5f65aa384ccc'");
        }
    }
}
