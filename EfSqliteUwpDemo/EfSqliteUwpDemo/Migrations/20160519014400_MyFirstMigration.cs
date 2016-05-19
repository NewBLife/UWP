using System;
using System.Collections.Generic;
using Microsoft.Data.Entity.Migrations;

namespace EfSqliteUwpDemo.Migrations
{
    public partial class MyFirstMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Sensor",
                columns: table => new
                {
                    SensorId = table.Column<Guid>(nullable: false),
                    Location = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Sensor", x => x.SensorId);
                });
            migrationBuilder.CreateTable(
                name: "Ambience",
                columns: table => new
                {
                    AmbienceId = table.Column<int>(nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Humidity = table.Column<int>(nullable: false),
                    SensorSensorId = table.Column<Guid>(nullable: true),
                    Temp = table.Column<int>(nullable: false),
                    TimeStamp = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Ambience", x => x.AmbienceId);
                    table.ForeignKey(
                        name: "FK_Ambience_Sensor_SensorSensorId",
                        column: x => x.SensorSensorId,
                        principalTable: "Sensor",
                        principalColumn: "SensorId",
                        onDelete: ReferentialAction.Restrict);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable("Ambience");
            migrationBuilder.DropTable("Sensor");
        }
    }
}
