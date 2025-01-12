import {getFiltered, getById} from "./requests";
const getRecipes = (filter) => {
    // Создаем объект queryParams, фильтруя undefined и пустые значения
    const queryParams = {};

    Object.entries(filter)
        .filter(([_, value]) => value !== undefined && value !== '') // фильтруем пустые значения
        .forEach(([key, value]) => {
            // Если значение является массивом (например, жанры), добавляем как массив
            if (Array.isArray(value)) {
                queryParams[key] = value;
            } else {
                queryParams[key] = String(value); // Если это не массив, преобразуем в строку
            }
        });

    // Передаем queryParams в функцию get
    return getFiltered('recipes', queryParams);
};

const getRecipe = (id)=>{
    return getById('recipes', id);
};

export {getRecipes, getRecipe}
