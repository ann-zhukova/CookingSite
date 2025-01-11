using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class InitDbData : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Image",
                table: "Recipes",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<int>(
                name: "YourTime",
                table: "Recipes",
                type: "integer",
                nullable: false,
                defaultValue: 0);
            // Добавление типов блюд
            migrationBuilder.InsertData(
                table: "Types",
                columns: new[] { "Id", "TypeName" },
                values: new object[,]
                {
                    { Guid.NewGuid(), "Первое" },
                    { Guid.NewGuid(), "Второе" },
                    {Guid.NewGuid(), "Мясное"},
                    {Guid.NewGuid(), "Суп"},
                    {Guid.NewGuid(), "Крем-суп"},
                    {Guid.NewGuid(), "Соус"},
                    {Guid.NewGuid(), "Паста"},
                    { Guid.NewGuid(), "Десерт" },
                    { Guid.NewGuid(), "Пирог" }
                }
            );

            // Добавление ингредиентов
            migrationBuilder.InsertData(
                table: "Ingredients",
                columns: new[] { "Id", "IngredientName" },
                values: new object[,]
                {
                    { Guid.NewGuid(), "Мясо" },
                    { Guid.NewGuid(), "Курица" },
                    { Guid.NewGuid(), "Ягоды" },
                    { Guid.NewGuid(), "Томаты" },
                    { Guid.NewGuid(), "Яблоки" },
                    { Guid.NewGuid(), "Сметана" },
                    { Guid.NewGuid(), "Картофель" },
                    { Guid.NewGuid(), "Сыр" },
                    { Guid.NewGuid(), "Чеснок" },
                    { Guid.NewGuid(), "Лук" },
                    { Guid.NewGuid(), "Молоко" },
                    { Guid.NewGuid(), "Мука" },
                    { Guid.NewGuid(), "Творог" },
                    { Guid.NewGuid(), "Масло" },
                    { Guid.NewGuid(), "Грибы" },
                    { Guid.NewGuid(), "Болгарский перец" },
                    { Guid.NewGuid(), "Шоколад" },
                    { Guid.NewGuid(), "Ваниль" },
                    { Guid.NewGuid(), "Сливки" },
                    { Guid.NewGuid(), "Яйца" }
                    
                }
            );
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Image",
                table: "Recipes");

            migrationBuilder.DropColumn(
                name: "YourTime",
                table: "Recipes");
            // Удаление всех записей из таблицы TypeEntities
            migrationBuilder.Sql("DELETE FROM Types");

            // Удаление всех записей из таблицы IngredientEntities
            migrationBuilder.Sql("DELETE FROM Ingredients");
        }
        
    }
}
