using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Persistence.Migrations
{
    public partial class CreateDB : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "BasePrivateEntity",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    DateCreated = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    CreatedBy = table.Column<string>(type: "text", nullable: true),
                    LastModifiedDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    LastModifiedBy = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BasePrivateEntity", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "DoctorVisits",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    DateTime = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    Address = table.Column<string>(type: "text", nullable: true),
                    Notes = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DoctorVisits", x => x.Id);
                    table.ForeignKey(
                        name: "FK_DoctorVisits_BasePrivateEntity_Id",
                        column: x => x.Id,
                        principalTable: "BasePrivateEntity",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "HealthRecords",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    StartDateTime = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    EndDateTime = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    Notes = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_HealthRecords", x => x.Id);
                    table.ForeignKey(
                        name: "FK_HealthRecords_BasePrivateEntity_Id",
                        column: x => x.Id,
                        principalTable: "BasePrivateEntity",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Medications",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Name = table.Column<string>(type: "text", nullable: true),
                    Dose = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Medications", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Medications_BasePrivateEntity_Id",
                        column: x => x.Id,
                        principalTable: "BasePrivateEntity",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "SleepRecords",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    StartDateTime = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    EndDateTime = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    Notes = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SleepRecords", x => x.Id);
                    table.ForeignKey(
                        name: "FK_SleepRecords_BasePrivateEntity_Id",
                        column: x => x.Id,
                        principalTable: "BasePrivateEntity",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Symptoms",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Name = table.Column<string>(type: "text", nullable: true),
                    Level = table.Column<int>(type: "integer", nullable: false),
                    Description = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Symptoms", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Symptoms_BasePrivateEntity_Id",
                        column: x => x.Id,
                        principalTable: "BasePrivateEntity",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Treatments",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Name = table.Column<string>(type: "text", nullable: true),
                    StartDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    RepeatOffset = table.Column<int>(type: "integer", nullable: false),
                    RepeatOccurencies = table.Column<int>(type: "integer", nullable: false),
                    Notes = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Treatments", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Treatments_BasePrivateEntity_Id",
                        column: x => x.Id,
                        principalTable: "BasePrivateEntity",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Medications_HealthRecords",
                columns: table => new
                {
                    HealthRecordId = table.Column<Guid>(type: "uuid", nullable: false),
                    MedicationId = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Medications_HealthRecords", x => new { x.HealthRecordId, x.MedicationId });
                    table.ForeignKey(
                        name: "FK_Medications_HealthRecords_HealthRecords_HealthRecordId",
                        column: x => x.HealthRecordId,
                        principalTable: "HealthRecords",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Medications_HealthRecords_Medications_MedicationId",
                        column: x => x.MedicationId,
                        principalTable: "Medications",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Symtpoms_HealthRecords",
                columns: table => new
                {
                    HealthRecordId = table.Column<Guid>(type: "uuid", nullable: false),
                    SymtpomId = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Symtpoms_HealthRecords", x => new { x.HealthRecordId, x.SymtpomId });
                    table.ForeignKey(
                        name: "FK_Symtpoms_HealthRecords_HealthRecords_HealthRecordId",
                        column: x => x.HealthRecordId,
                        principalTable: "HealthRecords",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Symtpoms_HealthRecords_Symptoms_SymtpomId",
                        column: x => x.SymtpomId,
                        principalTable: "Symptoms",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Medications_HealthRecords_MedicationId",
                table: "Medications_HealthRecords",
                column: "MedicationId");

            migrationBuilder.CreateIndex(
                name: "IX_Symtpoms_HealthRecords_SymtpomId",
                table: "Symtpoms_HealthRecords",
                column: "SymtpomId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "DoctorVisits");

            migrationBuilder.DropTable(
                name: "Medications_HealthRecords");

            migrationBuilder.DropTable(
                name: "SleepRecords");

            migrationBuilder.DropTable(
                name: "Symtpoms_HealthRecords");

            migrationBuilder.DropTable(
                name: "Treatments");

            migrationBuilder.DropTable(
                name: "Medications");

            migrationBuilder.DropTable(
                name: "HealthRecords");

            migrationBuilder.DropTable(
                name: "Symptoms");

            migrationBuilder.DropTable(
                name: "BasePrivateEntity");
        }
    }
}
