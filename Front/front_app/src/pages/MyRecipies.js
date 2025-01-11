import React, { useEffect, useState } from 'react';
import axios from 'axios';
import {get} from '../services/requests'
import { Link } from 'react-router-dom';
import '../assets/css/my_recipe.css'
const MyRecipes = () => {
    const [recipes, setRecipes] = useState([]);
    const [loading, setLoading] = useState(true);

    // Получение данных с сервера
    useEffect(() => {
        const fetchRecipes = async () => {
            try {
                const response = await get('users/recipes');
                console.log('error my recipe'+ response);
                setRecipes(response.recipes);
                setLoading(false);
            } catch (error) {
                alert('Ошибка при загрузке рецептов.');
                console.error(error);
            }
        };

        fetchRecipes();
    }, []);

    if (loading) {
        return <p>Загрузка рецептов...</p>;
    }

    return (
        <section>
            <h1>Мои рецепты</h1>
            <div className="buttons-container">
                <Link to="/add_recipe" className="add-serial-button">Добавить рецепт</Link>
            </div>
            {recipes.length > 0 ? (
                <table>
                    <thead>
                    <tr>
                        <th>Рецепт</th>
                        <th>Время приготовления</th>
                    </tr>
                    </thead>
                    <tbody>
                    {recipes.map((recipe) => (
                        <tr key={recipe.id}>
                            <td>{recipe.name}</td>
                            <td>{recipe.prepareTime} минут</td>
                        </tr>
                    ))}
                    </tbody>
                </table>
            ) : (
                <p>Рецепты отсутствуют. Добавьте новый рецепт!</p>
            )}
        </section>
    );
};

export default MyRecipes;
