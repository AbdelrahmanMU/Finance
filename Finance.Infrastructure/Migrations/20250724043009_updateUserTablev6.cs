using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Finance.Infrastructure.Migrations
{
    public partial class updateUserTablev6 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "FinanceRequests",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RequestNumber = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PaymentAmount = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    PaymentPeriod = table.Column<int>(type: "int", nullable: false),
                    TotalProfit = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    RequestStatus = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FinanceRequests", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "FinanceRequests");
        }
    }
}
