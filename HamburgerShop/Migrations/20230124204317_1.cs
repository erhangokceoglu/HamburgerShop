using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HamburgerShop.Migrations
{
    public partial class _1 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Boyutlar",
                columns: table => new
                {
                    BoyutId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    BoyutAdi = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    BoyutFiyati = table.Column<decimal>(type: "decimal(18,2)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Boyutlar", x => x.BoyutId);
                });

            migrationBuilder.CreateTable(
                name: "Hamburgerler",
                columns: table => new
                {
                    HamburgerId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    HamburgerAdi = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    HamburgerFiyati = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    HamburgerFotograf = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Hamburgerler", x => x.HamburgerId);
                });

            migrationBuilder.CreateTable(
                name: "Icecekler",
                columns: table => new
                {
                    IcecekId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    IcecekAdi = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    IcecekFiyati = table.Column<decimal>(type: "decimal(18,2)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Icecekler", x => x.IcecekId);
                });

            migrationBuilder.CreateTable(
                name: "Siparisler",
                columns: table => new
                {
                    SiparisId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    SiparisAdet = table.Column<byte>(type: "tinyint", nullable: false),
                    ToplamTutar = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    BoyutId = table.Column<int>(type: "int", nullable: false),
                    HamburgerId = table.Column<int>(type: "int", nullable: false),
                    IcecekId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Siparisler", x => x.SiparisId);
                    table.ForeignKey(
                        name: "FK_Siparisler_Boyutlar_BoyutId",
                        column: x => x.BoyutId,
                        principalTable: "Boyutlar",
                        principalColumn: "BoyutId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Siparisler_Hamburgerler_HamburgerId",
                        column: x => x.HamburgerId,
                        principalTable: "Hamburgerler",
                        principalColumn: "HamburgerId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Siparisler_Icecekler_IcecekId",
                        column: x => x.IcecekId,
                        principalTable: "Icecekler",
                        principalColumn: "IcecekId");
                });

            migrationBuilder.CreateTable(
                name: "EkstraMalzemeler",
                columns: table => new
                {
                    EkstraMalzemeId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    EkstraMalzemeAdi = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    EkstraMalzemeFiyati = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    SiparisId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EkstraMalzemeler", x => x.EkstraMalzemeId);
                    table.ForeignKey(
                        name: "FK_EkstraMalzemeler_Siparisler_SiparisId",
                        column: x => x.SiparisId,
                        principalTable: "Siparisler",
                        principalColumn: "SiparisId");
                });

            migrationBuilder.InsertData(
                table: "Boyutlar",
                columns: new[] { "BoyutId", "BoyutAdi", "BoyutFiyati" },
                values: new object[,]
                {
                    { 1, "100gr", 0m },
                    { 2, "150gr", 24m },
                    { 3, "200gr", 48m }
                });

            migrationBuilder.InsertData(
                table: "EkstraMalzemeler",
                columns: new[] { "EkstraMalzemeId", "EkstraMalzemeAdi", "EkstraMalzemeFiyati", "SiparisId" },
                values: new object[,]
                {
                    { 1, "Hardal Sos", 5m, null },
                    { 2, "Acı Sos", 6m, null },
                    { 3, "Mayonez", 4m, null },
                    { 4, "Ketcap", 4m, null },
                    { 5, "Ranch Sos", 7m, null }
                });

            migrationBuilder.InsertData(
                table: "Hamburgerler",
                columns: new[] { "HamburgerId", "HamburgerAdi", "HamburgerFiyati", "HamburgerFotograf" },
                values: new object[,]
                {
                    { 1, "Cheese Burger", 60m, "cheeseburger.jpg" },
                    { 2, "BigKing Burger", 90m, "big-king.jpg" },
                    { 3, "KingBeef Burger", 110m, "kingbeefburger.jpg" },
                    { 4, "Kofte Burger", 85m, "kofteburger.jpg" },
                    { 5, "Tavuk Burger", 50m, "tavukburger.jpg" }
                });

            migrationBuilder.InsertData(
                table: "Icecekler",
                columns: new[] { "IcecekId", "IcecekAdi", "IcecekFiyati" },
                values: new object[,]
                {
                    { 1, "Kola", 25m },
                    { 2, "Fanta", 25m },
                    { 3, "Ayran", 20m },
                    { 4, "Su", 15m },
                    { 5, "Soda", 18m }
                });

            migrationBuilder.CreateIndex(
                name: "IX_EkstraMalzemeler_SiparisId",
                table: "EkstraMalzemeler",
                column: "SiparisId");

            migrationBuilder.CreateIndex(
                name: "IX_Siparisler_BoyutId",
                table: "Siparisler",
                column: "BoyutId");

            migrationBuilder.CreateIndex(
                name: "IX_Siparisler_HamburgerId",
                table: "Siparisler",
                column: "HamburgerId");

            migrationBuilder.CreateIndex(
                name: "IX_Siparisler_IcecekId",
                table: "Siparisler",
                column: "IcecekId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "EkstraMalzemeler");

            migrationBuilder.DropTable(
                name: "Siparisler");

            migrationBuilder.DropTable(
                name: "Boyutlar");

            migrationBuilder.DropTable(
                name: "Hamburgerler");

            migrationBuilder.DropTable(
                name: "Icecekler");
        }
    }
}
