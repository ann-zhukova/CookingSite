import React, { useEffect, useState } from 'react';
import { getRecipe } from '../services/recipesService'; // Предполагается, что этот метод уже реализован
import '../assets/css/recipe.css'
import {useParams} from "react-router-dom";
const Recipe = () => {
    const [recipe, setRecipe] = useState(null);
    const { recipeId } = useParams();
    // Получение рецепта при монтировании компонента
    useEffect(() => {
        const fetchRecipe = async () => {
            try {
                const data = await getRecipe(recipeId);
                setRecipe(data.recipe);
            } catch (error) {
                console.error('Ошибка при загрузке рецепта:', error);
            }
        };

        fetchRecipe();
    }, [recipeId]);

    if (!recipe) {
        return <p>Загрузка рецепта...</p>;
    }

    return (
        <section>
            <div className="recipe-name">
                <h1>{recipe.name}</h1>
                <button className="recipe-favorite" title="Добавить в избранное">
                    &#9734;
                </button>
            </div>
            <div className="recipe-head">
                <img src={recipe.image} alt={recipe.name} className="recipe-img" />
                <div className="recipe-info">
                    <div className="recipe-ingredients">
                        <h2>Типы блюда</h2>
                        <div className="recipe-types">
                            <ul>
                                {recipe.types.map((type, index) => (
                                    <li key={index}>{type.typeName}</li>
                                ))}
                            </ul>
                        </div>
                        <h2>Ингредиенты</h2>
                        <ul>
                            {recipe.ingredients.map((ingredient, index) => (
                                <li key={index}>
                                    {ingredient.ingredientName}
                                </li>
                            ))}
                        </ul>
                    </div>
                </div>
            </div>
            <h2>Приготовление </h2>
            <h3>Время приготовления</h3>
            <p>
                <strong>Время на приготовление:</strong> {recipe.prepareTime} минут <br />
                <strong>Ваше время:</strong> {recipe.yourTime} минут
            </p>
            <h2>Шаги приготовления</h2>
            <div className="recipe-steps">
                {recipe.steps.length > 0 && recipe.steps.map((step, index) => (
                    <div key={index} className="recipe-step">
                        <div className="recipe-step-head">Шаг {index + 1}</div>
                        <p>{step.stepDescription}</p>
                    </div>
                ))}
            </div>
        </section>
    );
};

export default Recipe;
