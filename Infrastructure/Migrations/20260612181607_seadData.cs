using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql;

#nullable disable

namespace Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class seadData : Migration
    {
        /// <inheritdoc />
        /// <summary>
        /// This method is called when the database is being created or updated.
        /// It contains the migration operations to transform the database schema to a new state.
        /// </summary>
        /// <param name="migrationBuilder">The MigrationBuilder instance used to create database objects.</param>
        protected override void Up(MigrationBuilder migrationBuilder)
        {

            // Seed Data
            // NOTE: Replace the hashes below with actual password hashes from your application logic.
            // Using plain text or static hashes here is for demonstration only.
            var adminId = Guid.Parse("11111111-1111-1111-1111-111111111111");
            var userId = Guid.Parse("22222222-2222-2222-2222-222222222222");

            // Seed data using InsertData instead of raw SQL with parameters
            migrationBuilder.InsertData(
                table: "User",
                columns: new[] { "Id", "Username", "PasswordHash", "Email", "DisplayName", "ProfilePictureUrl", "IsOnline", "LastSeen" },
                values: new object[,]
                {
                // Admin User
                { adminId, "admin", "hashed_password_admin_123", "admin@example.com", "System Administrator", "/img/admin.png", true, DateTime.UtcNow },
                // Standard User
                { userId, "johndoe", "hashed_password_user_123", "john.doe@example.com", "John Doe", "/img/default.png", false, DateTime.UtcNow.AddHours(-1) }
                });
        }


        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {

        }
    }
}
