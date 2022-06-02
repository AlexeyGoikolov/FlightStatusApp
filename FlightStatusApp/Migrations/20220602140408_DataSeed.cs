using FlightStatusApp.Services;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FlightStatusApp.Migrations
{
    public partial class DataSeed : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                "Roles",
                new[] { "Id", "Code" },
                new object[] { 1, "Moderator" });
            
            migrationBuilder.InsertData(
                "Roles",
                new[] { "Id", "Code" },
                new object[] { 2, "User" });
            
            migrationBuilder.InsertData(
                "Users",
                new[] { "Username", "Password", "RoleId" },
                new object[] { "administrator", PasswordHashGenerator.HashPassword("Aa123456"), 1 });

            migrationBuilder.InsertData(
                "Users",
                new[] { "Username", "Password", "RoleId" },
                new object[] { "basicUser", PasswordHashGenerator.HashPassword("Aa123456"), 2 });
            
            migrationBuilder.InsertData(
                "Statuses",
                new[] { "Origin", "Destination", "Departure", "Arrival", "Status" },
                new object[] { "Asia/Almaty", "Asia/Aqtau",  DateTimeOffset.Parse("2022-06-02 07:10:25 +6:00"), DateTimeOffset.Parse("2022-06-02 08:40:25 +5:00"), 0});
            
            migrationBuilder.InsertData(
                "Statuses",
                new[] { "Origin", "Destination", "Departure", "Arrival", "Status" },
                new object[] { "Asia/Almaty", "Europe/Moscow", DateTimeOffset.Parse("2022-06-03 07:10:25 +6:00"), DateTimeOffset.Parse("2022-06-03 11:35:25 +3:00"), 1});
        }
        
        

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Roles",
                new[] { "Code" },
                new object[] { "Moderator" });
            
            migrationBuilder.DeleteData(
                "Roles",
                new[] { "Code" },
                new object[] { "User" });
            
            migrationBuilder.DeleteData(
                "Users",
                new[] { "Username", "Password", "RoleId" },
                new object[] { "administrator", PasswordHashGenerator.HashPassword("Aa123456"), 1 });

            migrationBuilder.DeleteData(
                "Users",
                new[] { "Username", "Password", "RoleId" },
                new object[] { "basicUser", PasswordHashGenerator.HashPassword("Aa123456"), 2 });
            
            migrationBuilder.DeleteData(
                "Statuses",
                new[] { "Origin", "Destination", "Departure", "Arrival", "Status" },
                new object[] { "Asia/Almaty", "Asia/Aqtau",  new DateTimeOffset(2022, 06, 02, 19, 10, 25, TimeSpan.FromHours(+6)), new DateTimeOffset(2022, 06, 02, 19, 40, 25, TimeSpan.FromHours(+5)), 0});
            
            migrationBuilder.DeleteData(
                "Statuses",
                new[] { "Origin", "Destination", "Departure", "Arrival", "Status" },
                new object[] { "Asia/Almaty", "Europe/Moscow", new DateTimeOffset(2022, 06, 03, 13, 10, 25, TimeSpan.FromHours(+6)), new DateTimeOffset(2022, 06, 03, 17, 35, 25, TimeSpan.FromHours(+3)), 1});
        }
    }
}
