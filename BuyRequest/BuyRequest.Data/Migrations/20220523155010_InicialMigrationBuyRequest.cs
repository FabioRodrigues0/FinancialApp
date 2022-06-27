using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BuyRequest.Data.Migrations
{
	public partial class InicialMigrationBuyRequest : Migration
	{
		protected override void Up(MigrationBuilder migrationBuilder)
		{
			migrationBuilder.CreateTable(
				name: "BuyRequests",
				columns: table => new {
					Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
					Code = table.Column<long>(type: "bigint", nullable: false),
					Date = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false),
					DeliveryDate = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false),
					Client = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
					ClientDescription = table.Column<string>(type: "nvarchar(max)", nullable: false),
					ClientEmail = table.Column<string>(type: "nvarchar(max)", nullable: false),
					ClientPhone = table.Column<string>(type: "nvarchar(max)", nullable: false),
					Status = table.Column<int>(type: "int", nullable: false),
					Street = table.Column<string>(type: "nvarchar(max)", nullable: false),
					Number = table.Column<string>(type: "nvarchar(max)", nullable: false),
					Sector = table.Column<string>(type: "nvarchar(max)", nullable: false),
					Complement = table.Column<string>(type: "nvarchar(max)", nullable: false),
					City = table.Column<string>(type: "nvarchar(max)", nullable: false),
					State = table.Column<string>(type: "nvarchar(max)", nullable: false),
					Discount = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
					ProductValor = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
					Cost = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
					TotalValor = table.Column<decimal>(type: "decimal(18,2)", nullable: false)
				},
				constraints: table =>
				{
					table.PrimaryKey("PK_BuyRequests", x => x.Id);
				});

			migrationBuilder.CreateTable(
				name: "BuyRequestProducts",
				columns: table => new {
					Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
					Total = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
					BuyRequestId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
					ProductId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
					ProductDescription = table.Column<string>(type: "nvarchar(max)", nullable: false),
					ProductCategory = table.Column<int>(type: "int", nullable: false),
					Quantity = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
					Valor = table.Column<decimal>(type: "decimal(18,2)", nullable: false)
				},
				constraints: table =>
				{
					table.PrimaryKey("PK_BuyRequestProducts", x => x.Id);
					table.ForeignKey(
						name: "FK_BuyRequestProducts_BuyRequestId_BuyRequest_Id",
						column: x => x.BuyRequestId,
						principalTable: "BuyRequests",
						principalColumn: "Id",
						onDelete: ReferentialAction.Cascade);
				});

			migrationBuilder.CreateIndex(
				name: "IX_BuyRequestProducts_BuyRequestId",
				table: "BuyRequestProducts",
				column: "BuyRequestId");
		}

		protected override void Down(MigrationBuilder migrationBuilder)
		{
			migrationBuilder.DropTable(
				name: "BuyRequestProducts");

			migrationBuilder.DropTable(
				name: "BuyRequests");
		}
	}
}
