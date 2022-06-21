using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Document.Data.Migrations
{
	public partial class InicialMigrationDocument : Migration
	{
		protected override void Up(MigrationBuilder migrationBuilder)
		{
			migrationBuilder.CreateTable(
				name: "Documents",
				columns: table => new {
					Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
					Number = table.Column<string>(type: "nvarchar(max)", nullable: false),
					Date = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false),
					DocumentType = table.Column<int>(type: "int", nullable: false),
					Operation = table.Column<int>(type: "int", nullable: false),
					Paid = table.Column<bool>(type: "bit", nullable: false),
					PaymentDate = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false),
					Description = table.Column<string>(type: "nvarchar(max)", nullable: false),
					Total = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
					Observation = table.Column<string>(type: "nvarchar(max)", nullable: false)
				},
				constraints: table =>
				{
					table.PrimaryKey("PK_Documents", x => x.Id);
				});
		}

		protected override void Down(MigrationBuilder migrationBuilder)
		{
			migrationBuilder.DropTable(
				name: "Documents");
		}
	}
}
