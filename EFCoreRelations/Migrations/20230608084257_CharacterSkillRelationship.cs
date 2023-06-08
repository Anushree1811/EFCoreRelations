using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EFCoreRelations.Migrations
{
    /// <inheritdoc />
    public partial class CharacterSkillRelationship : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Character_Skill_SkillId",
                table: "Character");

            migrationBuilder.DropIndex(
                name: "IX_Character_SkillId",
                table: "Character");

            migrationBuilder.DropColumn(
                name: "SkillId",
                table: "Character");

            migrationBuilder.CreateTable(
                name: "CharacterSkill",
                columns: table => new
                {
                    CharacterId = table.Column<int>(type: "int", nullable: false),
                    SkillId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CharacterSkill", x => new { x.CharacterId, x.SkillId });
                    table.ForeignKey(
                        name: "FK_CharacterSkill_Character_CharacterId",
                        column: x => x.CharacterId,
                        principalTable: "Character",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_CharacterSkill_Skill_SkillId",
                        column: x => x.SkillId,
                        principalTable: "Skill",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_CharacterSkill_SkillId",
                table: "CharacterSkill",
                column: "SkillId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CharacterSkill");

            migrationBuilder.AddColumn<int>(
                name: "SkillId",
                table: "Character",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Character_SkillId",
                table: "Character",
                column: "SkillId");

            migrationBuilder.AddForeignKey(
                name: "FK_Character_Skill_SkillId",
                table: "Character",
                column: "SkillId",
                principalTable: "Skill",
                principalColumn: "Id");
        }
    }
}
