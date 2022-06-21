using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CashBook.Data.Migrations
{
	public partial class InicialMigrationCashBook : Migration
	{
		protected override void Up(MigrationBuilder migrationBuilder)
		{
			migrationBuilder.CreateTable(
				name: "CashBooks",
				columns: table => new {
					Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
					Origin = table.Column<int>(type: "int", nullable: false),
					OriginId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
					Description = table.Column<string>(type: "nvarchar(max)", nullable: false),
					Type = table.Column<int>(type: "int", nullable: false),
					Valor = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
					IsEdited = table.Column<bool>(type: "bit", nullable: false)
				},
				constraints: table =>
				{
					table.PrimaryKey("PK_CashBooks", x => x.Id);
				});
		}

		protected override void Down(MigrationBuilder migrationBuilder)
		{
			migrationBuilder.DropTable(
				name: "CashBooks");
		}
	}
}
