import React, { useState, useEffect } from 'react';
import axios from 'axios';
import '../assets/css/form_recipe.css'
import {get} from "../services/requests";
import Select from "react-select";
const AddRecipe = () => {
    const [recipeName, setRecipeName] = useState('');
    const [ingredients, setIngredients] = useState([]);
    const [selectedIngredients, setSelectedIngredients] = useState([]);
    const [prepareTime, setPrepareTime] = useState(0);
    const [yourTime, setYourTime] = useState(0);
    const [dishTypes, setDishTypes] = useState([]);
    const [selectedDishTypes, setSelectedDishTypes] = useState([]);
    const [imageUrl, setImageUrl] = useState('');
    const [steps, setSteps] = useState(['']);
    const [id, setId] = useState('');

    // Загрузка ингредиентов и типов блюда из базы данных
    useEffect(() => {
        const fetchData = async () => {
            try {
                const [ingredientsResponse, dishTypesResponse] = await Promise.all([
                    get('ingredients'),
                    get('types')
                ]);
                console.log(dishTypesResponse);
                setIngredients(ingredientsResponse.ingredients);
                setDishTypes(dishTypesResponse.types);
            } catch (error) {
                alert('Ошибка при загрузке данных с сервера.');
                console.error(error);
            }
        };

        fetchData();
    }, []);

    // Обработчик добавления нового шага
    const addStep = () => {
        setSteps([...steps, '']);
    };

    // Обработчик изменения шага
    const handleStepChange = (index, value) => {
        const updatedSteps = [...steps];
        updatedSteps[index] = value;
        setSteps(updatedSteps);
    };

    // Обработчик отправки формы
    const handleSubmit = async (e) => {
        e.preventDefault();

        if (!recipeName || !prepareTime || selectedDishTypes.length === 0 || !imageUrl || steps.some(step => !step.trim()) || selectedIngredients.length === 0) {
            alert('Пожалуйста, заполните все поля!');
            return;
        }

        const recipe = {
            name: recipeName,
            prepareTime: parseInt(prepareTime, 10),
            yourTime: yourTime,
            types: selectedDishTypes,
            image: imageUrl,
            steps: steps.map((s, index) => ({stepNumber: index+1, stepDescription: s })),
            ingredients: selectedIngredients,
        };

        try {
            const response = await axios.post('/recipes', recipe);
            alert('Рецепт успешно добавлен!');
            console.log(response.data);
            resetForm();
        } catch (error) {
            alert('Ошибка при добавлении рецепта.');
            console.error(error);
        }
    };

    // Сброс формы
    const resetForm = () => {
        setRecipeName('');
        setPrepareTime(0);
        setSelectedDishTypes([]);
        setImageUrl('');
        setSteps(['']);
        setYourTime(0);
        setId('');
        setSelectedIngredients([]);
    };
    // Форматируем данные для react-select
    const ingredientOptions = ingredients.map((ingredient) => ({
        value: ingredient.id,
        label: ingredient.ingredientName,
    }));

    const dishTypeOptions = dishTypes.map((type) => ({
        value: type.id,
        label: type.typeName,
    }));
    return (
        <main>
            <section>
                <h1>Добавьте свой рецепт</h1>
                <form onSubmit={handleSubmit}>
                    {/* Название рецепта */}
                    <label htmlFor="recipeName">Название рецепта:</label>
                    <input
                        type="text"
                        id="recipeName"
                        value={recipeName}
                        onChange={(e) => setRecipeName(e.target.value)}
                        placeholder="Введите название рецепта"
                        required
                    />
                    
                    {/* Ссылка на изображение */}
                    <label htmlFor="imageUrl">Ссылка на изображение:</label>
                    <input
                        type="url"
                        id="imageUrl"
                        value={imageUrl}
                        onChange={(e) => setImageUrl(e.target.value)}
                        placeholder="Введите ссылку на изображение"
                        required
                    />
                    {/* Время приготовления */}
                    <label htmlFor="prepareTime">Время приготовления (минуты):</label>
                    <input
                        type="number"
                        id="prepareTime"
                        value={prepareTime}
                        onChange={(e) => setPrepareTime(e.target.value)}
                        min="1"
                        required
                    />

                    {/* Время приготовления */}
                    <label htmlFor="prepareTime">Ваше время (минуты):</label>
                    <input
                        type="number"
                        id="prepareTime"
                        value={yourTime}
                        onChange={(e) => setYourTime(e.target.value)}
                        min="1"
                        required
                    />

                    {/* Ингредиенты */}
                    <label htmlFor="ingredients">Выберите ингредиенты:</label>
                    <Select
                        id="ingredients"
                        options={ingredientOptions}
                        isMulti
                        value={selectedIngredients.map((id) => ingredientOptions.find((opt) => opt.value === id))}
                        onChange={(selectedOptions) =>
                            setSelectedIngredients(selectedOptions.map((option) => option.value))
                        }
                    />

                    {/* Типы блюд */}
                    <label htmlFor="dishTypes">Выберите типы блюд:</label>
                    <Select
                        id="dishTypes"
                        options={dishTypeOptions}
                        isMulti
                        value={selectedDishTypes.map((id) => dishTypeOptions.find((opt) => opt.value === id))}
                        onChange={(selectedOptions) =>
                            setSelectedDishTypes(selectedOptions.map((option) => option.value))
                        }
                    />
                    

                    {/* Шаги приготовления */}
                    <label>Шаги приготовления:</label>
                    {steps.map((step, index) => (
                        <div key={index} className="step-container">
                            <textarea
                                value={step}
                                onChange={(e) => handleStepChange(index, e.target.value)}
                                placeholder={`Введите шаг ${index + 1}`}
                                rows="2"
                                required
                            />
                            {index === steps.length - 1 && (
                                <button type="button" className="button-add" onClick={addStep}>
                                    Добавить шаг
                                </button>
                            )}
                        </div>
                    ))}

                    {/* Кнопка отправки */}
                    <button type="submit">Добавить рецепт</button>
                </form>
            </section>
        </main>
    );
};

export default AddRecipe;

