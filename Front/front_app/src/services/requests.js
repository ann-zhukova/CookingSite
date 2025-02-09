import axios from 'axios';
import qs from 'qs';

// Общая конфигурация для запросов
const config = {
    headers: {
        'Content-Type': 'application/json',
    },
    withCredentials: true,
};

// Функция для получения данных по маршруту
const get = async (route) => {
    try {
        const response = await axios.get(`/${route}/`, config);
        return response.data;
    } catch (error) {
        console.error("Error fetching:", error);
        throw error;
    }
};

// Функция для получения фильтрованных данных
const getFiltered = async (route, queryParams) => {
    try {
        console.log(queryParams);
        //const response = await axios.get(`/${route}`, { params: queryParams, ...config });
        const response = await axios.get(`/${route}`, {
            params: queryParams,
            paramsSerializer: (params) => qs.stringify(params, { arrayFormat: 'repeat' }),
            ...config
        });
        return response.data;
    } catch (error) {
        console.error("Error fetching filtered data:", error);
        throw error;
    }
};

// Функция для получения данных по ID
const getById = async (route, id) => {
    try {
        const response = await axios.get(`/${route}/${id}/`, config);
        return response.data;
    } catch (error) {
        console.error(`Error fetching object with ID ${id}:`, error);
        throw error;
    }
};

// Функция для создания нового объекта
const create = async (route, request) => {
    try {
        const response = await axios.post(`/${route}/`, request, config);
        console.log('Response from API:', response.data);
        return response.data.id;
    } catch (error) {
        console.error("Error creating object:", error);
        throw error;
    }
};

// Функция для обновления объекта
const update = async (route, id, request) => {
    try {
        await axios.put(`/${route}/${id}/`, request, config);
        console.log("Object updated successfully");
    } catch (error) {
        console.error(`Error updating object with ID ${id}:`, error);
        throw error;
    }
};

// Функция для удаления объекта по ID
const deleteById = async (route, id) => {
    try {
        await axios.delete(`/${route}/${id}/`, config);
        console.log("Object deleted successfully");
    } catch (error) {
        console.error(`Error deleting object with ID ${id}:`, error);
        throw error;
    }
};

const AddToFavorites = async (id) => {
    try {
        console.log(`Добавлено в избранное: ${id}`);
        const response = await axios.post(`/recipes/favorite/${id}`, config);
    } catch (error) {
        console.error('Ошибка при добавлении в избранное:', error);
    }
};

// Экспорт всех функций
export { get, getFiltered, getById, create, update, deleteById, AddToFavorites};
