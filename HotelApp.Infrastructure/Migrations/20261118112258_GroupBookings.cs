using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HotelApp.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class GroupBookings : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Bookings",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    GuestName = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    GuestEmail = table.Column<string>(type: "nvarchar(320)", maxLength: 320, nullable: false),
                    GuestPhone = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    TimeFrame_StartDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    TimeFrame_EndDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    TotalGuests = table.Column<int>(type: "int", nullable: false),
                    IsConfirmed = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Bookings", x => x.Id);
                });

            migrationBuilder.AddColumn<int>(
                name: "BookingId",
                table: "Reservations",
                type: "int",
                nullable: true);

            migrationBuilder.Sql(
                """
                SET IDENTITY_INSERT [Bookings] ON;

                INSERT INTO [Bookings]
                    ([Id], [GuestName], [GuestEmail], [GuestPhone],
                     [TimeFrame_StartDate], [TimeFrame_EndDate], [TotalGuests], [IsConfirmed])
                SELECT [Id], N'Legacy booking', N'legacy@invalid.local', NULL,
                       [TimeFrame_StartDate], [TimeFrame_EndDate], 1, [IsConfirmed]
                FROM [Reservations];

                SET IDENTITY_INSERT [Bookings] OFF;

                UPDATE [Reservations]
                SET [BookingId] = [Id];
                """);

            migrationBuilder.AlterColumn<int>(
                name: "BookingId",
                table: "Reservations",
                type: "int",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.DropColumn(
                name: "IsConfirmed",
                table: "Reservations");

            migrationBuilder.DropColumn(
                name: "TimeFrame_EndDate",
                table: "Reservations");

            migrationBuilder.DropColumn(
                name: "TimeFrame_StartDate",
                table: "Reservations");

            migrationBuilder.CreateIndex(
                name: "IX_Reservations_BookingId",
                table: "Reservations",
                column: "BookingId");

            migrationBuilder.AddForeignKey(
                name: "FK_Reservations_Bookings_BookingId",
                table: "Reservations",
                column: "BookingId",
                principalTable: "Bookings",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Reservations_Bookings_BookingId",
                table: "Reservations");

            migrationBuilder.DropIndex(
                name: "IX_Reservations_BookingId",
                table: "Reservations");

            migrationBuilder.AddColumn<DateTime>(
                name: "TimeFrame_StartDate",
                table: "Reservations",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "TimeFrame_EndDate",
                table: "Reservations",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<bool>(
                name: "IsConfirmed",
                table: "Reservations",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.Sql(
                """
                UPDATE [Reservations]
                SET [TimeFrame_StartDate] = [Bookings].[TimeFrame_StartDate],
                    [TimeFrame_EndDate] = [Bookings].[TimeFrame_EndDate],
                    [IsConfirmed] = [Bookings].[IsConfirmed]
                FROM [Reservations]
                INNER JOIN [Bookings] ON [Bookings].[Id] = [Reservations].[BookingId];
                """);

            migrationBuilder.DropColumn(
                name: "BookingId",
                table: "Reservations");

            migrationBuilder.DropTable(
                name: "Bookings");
        }
    }
}
