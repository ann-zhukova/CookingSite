using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class AddEmail : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Email",
                table: "Users",
                type: "character varying(64)",
                maxLength: 64,
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateTable(
                name: "Ingredients",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    IngredientName = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Ingredients", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Recipes",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Name = table.Column<string>(type: "text", nullable: false),
                    PrepareTime = table.Column<int>(type: "integer", nullable: false),
                    UserId = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Recipes", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Recipes_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Types",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    TypeName = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Types", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "IngredientEntityRecipeEntity",
                columns: table => new
                {
                    IngredientsId = table.Column<Guid>(type: "uuid", nullable: false),
                    RecipesId = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_IngredientEntityRecipeEntity", x => new { x.IngredientsId, x.RecipesId });
                    table.ForeignKey(
                        name: "FK_IngredientEntityRecipeEntity_Ingredients_IngredientsId",
                        column: x => x.IngredientsId,
                        principalTable: "Ingredients",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_IngredientEntityRecipeEntity_Recipes_RecipesId",
                        column: x => x.RecipesId,
                        principalTable: "Recipes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "RecipeEntityUserEntity",
                columns: table => new
                {
                    FavoriteRecipesId = table.Column<Guid>(type: "uuid", nullable: false),
                    UserFavoritesId = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RecipeEntityUserEntity", x => new { x.FavoriteRecipesId, x.UserFavoritesId });
                    table.ForeignKey(
                        name: "FK_RecipeEntityUserEntity_Recipes_FavoriteRecipesId",
                        column: x => x.FavoriteRecipesId,
                        principalTable: "Recipes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_RecipeEntityUserEntity_Users_UserFavoritesId",
                        column: x => x.UserFavoritesId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Steps",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    StepNumber = table.Column<int>(type: "integer", nullable: false),
                    StepDescription = table.Column<string>(type: "text", nullable: false),
                    RecipetId = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Steps", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Steps_Recipes_RecipetId",
                        column: x => x.RecipetId,
                        principalTable: "Recipes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "RecipeEntityTypeEntity",
                columns: table => new
                {
                    RecipesId = table.Column<Guid>(type: "uuid", nullable: false),
                    TypesId = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RecipeEntityTypeEntity", x => new { x.RecipesId, x.TypesId });
                    table.ForeignKey(
                        name: "FK_RecipeEntityTypeEntity_Recipes_RecipesId",
                        column: x => x.RecipesId,
                        principalTable: "Recipes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_RecipeEntityTypeEntity_Types_TypesId",
                        column: x => x.TypesId,
                        principalTable: "Types",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_IngredientEntityRecipeEntity_RecipesId",
                table: "IngredientEntityRecipeEntity",
                column: "RecipesId");

            migrationBuilder.CreateIndex(
                name: "IX_Ingredients_IngredientName",
                table: "Ingredients",
                column: "IngredientName",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_RecipeEntityTypeEntity_TypesId",
                table: "RecipeEntityTypeEntity",
                column: "TypesId");

            migrationBuilder.CreateIndex(
                name: "IX_RecipeEntityUserEntity_UserFavoritesId",
                table: "RecipeEntityUserEntity",
                column: "UserFavoritesId");

            migrationBuilder.CreateIndex(
                name: "IX_Recipes_PrepareTime",
                table: "Recipes",
                column: "PrepareTime");

            migrationBuilder.CreateIndex(
                name: "IX_Recipes_UserId",
                table: "Recipes",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_Steps_RecipetId_StepNumber",
                table: "Steps",
                columns: new[] { "RecipetId", "StepNumber" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Types_TypeName",
                table: "Types",
                column: "TypeName",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "IngredientEntityRecipeEntity");

            migrationBuilder.DropTable(
                name: "RecipeEntityTypeEntity");

            migrationBuilder.DropTable(
                name: "RecipeEntityUserEntity");

            migrationBuilder.DropTable(
                name: "Steps");

            migrationBuilder.DropTable(
                name: "Ingredients");

            migrationBuilder.DropTable(
                name: "Types");

            migrationBuilder.DropTable(
                name: "Recipes");

            migrationBuilder.DropColumn(
                name: "Email",
                table: "Users");
        }
    }
}
