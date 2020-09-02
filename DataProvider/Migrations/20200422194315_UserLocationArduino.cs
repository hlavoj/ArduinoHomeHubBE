using Microsoft.EntityFrameworkCore.Migrations;

namespace DataProvider.Migrations
{
    public partial class UserLocationArduino : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Temperatures_IdentityUsers_UserId",
                table: "Temperatures");

            migrationBuilder.DropIndex(
                name: "IX_Temperatures_UserId",
                table: "Temperatures");

            migrationBuilder.DropColumn(
                name: "UserId",
                table: "Temperatures");

            migrationBuilder.AddColumn<int>(
                name: "ArduinoId",
                table: "Temperatures",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "Location",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Location", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Arduino",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(nullable: true),
                    Info = table.Column<string>(nullable: true),
                    AuthorizationHash = table.Column<string>(nullable: true),
                    LocationId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Arduino", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Arduino_Location_LocationId",
                        column: x => x.LocationId,
                        principalTable: "Location",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "UserLocation",
                columns: table => new
                {
                    UserId = table.Column<int>(nullable: false),
                    LocationId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserLocation", x => new { x.UserId, x.LocationId });
                    table.ForeignKey(
                        name: "FK_UserLocation_Location_LocationId",
                        column: x => x.LocationId,
                        principalTable: "Location",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_UserLocation_IdentityUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "IdentityUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Temperatures_ArduinoId",
                table: "Temperatures",
                column: "ArduinoId");

            migrationBuilder.CreateIndex(
                name: "IX_Arduino_LocationId",
                table: "Arduino",
                column: "LocationId");

            migrationBuilder.CreateIndex(
                name: "IX_UserLocation_LocationId",
                table: "UserLocation",
                column: "LocationId");

            migrationBuilder.AddForeignKey(
                name: "FK_Temperatures_Arduino_ArduinoId",
                table: "Temperatures",
                column: "ArduinoId",
                principalTable: "Arduino",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Temperatures_Arduino_ArduinoId",
                table: "Temperatures");

            migrationBuilder.DropTable(
                name: "Arduino");

            migrationBuilder.DropTable(
                name: "UserLocation");

            migrationBuilder.DropTable(
                name: "Location");

            migrationBuilder.DropIndex(
                name: "IX_Temperatures_ArduinoId",
                table: "Temperatures");

            migrationBuilder.DropColumn(
                name: "ArduinoId",
                table: "Temperatures");

            migrationBuilder.AddColumn<int>(
                name: "UserId",
                table: "Temperatures",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Temperatures_UserId",
                table: "Temperatures",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_Temperatures_IdentityUsers_UserId",
                table: "Temperatures",
                column: "UserId",
                principalTable: "IdentityUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
