import React, { useState, useEffect } from 'react';
import axios from 'axios';
import '../assets/css/form_recipe.css'
const AddRecipe = () => {
    const [recipeName, setRecipeName] = useState('');
    const [ingredients, setIngredients] = useState([]);
    const [selectedIngredients, setSelectedIngredients] = useState([]);
    const [prepareTime, setPrepareTime] = useState('');
    const [dishTypes, setDishTypes] = useState([]);
    const [selectedDishTypes, setSelectedDishTypes] = useState([]);
    const [imageUrl, setImageUrl] = useState('');
    const [steps, setSteps] = useState(['']);

    // Загрузка ингредиентов и типов блюда из базы данных
    useEffect(() => {
        const fetchData = async () => {
            try {
                const ingredientsResponse = await axios.get('/api/ingredients');
                const dishTypesResponse = await axios.get('/api/dishTypes');

                setIngredients(ingredientsResponse.data);
                setDishTypes(dishTypesResponse.data);
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
            dishTypes: selectedDishTypes,
            imageUrl,
            steps,
            ingredients: selectedIngredients,
        };

        try {
            const response = await axios.post('/api/recipes', recipe);
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
        setPrepareTime('');
        setSelectedDishTypes([]);
        setImageUrl('');
        setSteps(['']);
        setSelectedIngredients([]);
    };

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

                    {/* Ингредиенты */}
                    <label htmlFor="ingredients">Выберите ингредиенты:</label>
                    <select
                        id="ingredients"
                        multiple
                        value={selectedIngredients}
                        onChange={(e) =>
                            setSelectedIngredients([...e.target.selectedOptions].map(option => option.value))
                        }
                    >
                        {ingredients.map((ingredient) => (
                            <option key={ingredient.id} value={ingredient.name}>
                                {ingredient.name}
                            </option>
                        ))}
                    </select>

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

                    {/* Тип блюда */}
                    <label htmlFor="dishTypes">Выберите типы блюда:</label>
                    <select
                        id="dishTypes"
                        multiple
                        value={selectedDishTypes}
                        onChange={(e) =>
                            setSelectedDishTypes([...e.target.selectedOptions].map(option => option.value))
                        }
                    >
                        {dishTypes.map((type) => (
                            <option key={type.id} value={type.name}>
                                {type.name}
                            </option>
                        ))}
                    </select>

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
                                <button type="button" onClick={addStep}>
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

