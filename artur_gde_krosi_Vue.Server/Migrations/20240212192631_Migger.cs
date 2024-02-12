using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace artur_gde_krosi_Vue.Server.Migrations
{
    /// <inheritdoc />
    public partial class Migger : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Brends",
                columns: table => new
                {
                    BrendId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    name = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Brends", x => x.BrendId);
                });

            migrationBuilder.CreateTable(
                name: "ModelKrosovocks",
                columns: table => new
                {
                    ModelKrosovockId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    BrendID = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ModelKrosovocks", x => x.ModelKrosovockId);
                    table.ForeignKey(
                        name: "FK_ModelKrosovocks_Brends_BrendID",
                        column: x => x.BrendID,
                        principalTable: "Brends",
                        principalColumn: "BrendId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Products",
                columns: table => new
                {
                    ProductId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    prise = table.Column<double>(type: "float", nullable: false),
                    ModelKrosovockId = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Products", x => x.ProductId);
                    table.ForeignKey(
                        name: "FK_Products_ModelKrosovocks_ModelKrosovockId",
                        column: x => x.ModelKrosovockId,
                        principalTable: "ModelKrosovocks",
                        principalColumn: "ModelKrosovockId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Images",
                columns: table => new
                {
                    ImageId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Index = table.Column<int>(type: "int", nullable: false),
                    ImageSrc = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ProductId = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Images", x => x.ImageId);
                    table.ForeignKey(
                        name: "FK_Images_Products_ProductId",
                        column: x => x.ProductId,
                        principalTable: "Products",
                        principalColumn: "ProductId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Variants",
                columns: table => new
                {
                    VariantId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    externalCode = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    quantityInStock = table.Column<int>(type: "int", nullable: false),
                    shoeSize = table.Column<double>(type: "float", nullable: false),
                    prise = table.Column<double>(type: "float", nullable: false),
                    ProductId = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Variants", x => x.VariantId);
                    table.ForeignKey(
                        name: "FK_Variants_Products_ProductId",
                        column: x => x.ProductId,
                        principalTable: "Products",
                        principalColumn: "ProductId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ImageVariants",
                columns: table => new
                {
                    ImageVariantId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ImageData = table.Column<byte[]>(type: "varbinary(max)", nullable: false),
                    VariantId = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ImageVariants", x => x.ImageVariantId);
                    table.ForeignKey(
                        name: "FK_ImageVariants_Variants_VariantId",
                        column: x => x.VariantId,
                        principalTable: "Variants",
                        principalColumn: "VariantId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Images_ProductId",
                table: "Images",
                column: "ProductId");

            migrationBuilder.CreateIndex(
                name: "IX_ImageVariants_VariantId",
                table: "ImageVariants",
                column: "VariantId");

            migrationBuilder.CreateIndex(
                name: "IX_ModelKrosovocks_BrendID",
                table: "ModelKrosovocks",
                column: "BrendID");

            migrationBuilder.CreateIndex(
                name: "IX_Products_ModelKrosovockId",
                table: "Products",
                column: "ModelKrosovockId");

            migrationBuilder.CreateIndex(
                name: "IX_Variants_ProductId",
                table: "Variants",
                column: "ProductId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Images");

            migrationBuilder.DropTable(
                name: "ImageVariants");

            migrationBuilder.DropTable(
                name: "Variants");

            migrationBuilder.DropTable(
                name: "Products");

            migrationBuilder.DropTable(
                name: "ModelKrosovocks");

            migrationBuilder.DropTable(
                name: "Brends");
        }
    }
}
