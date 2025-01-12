import { useState, useEffect, useRef } from 'react';
import TimeSlider from './TimeSlider';
import { get } from "../services/requests";
import Select from 'react-select';

const Sidebar = ({ initialFilters, onFilterChange }) => {
    const [filters, setFilters] = useState({
        minTime: 1,
        maxTime: 720,
        types: [], // Список Id типов
        ingredients: [], // Список Id ингредиентов
        page: 1,
        //pageSize: 5,
        sortBy: 'time',
    });

    const [availableTypes, setAvailableTypes] = useState([]); // Типы объектов
    const [availableIngredients, setAvailableIngredients] = useState([]); // Ингредиенты объектов

    // Ссылки на предыдущее состояние фильтров
    const prevFiltersRef = useRef();
    const prevFilters = prevFiltersRef.current;

    // Функция для получения типов блюд
    const fetchDataTypes = async () => {
        const response = await get('types');
        setAvailableTypes(response.types); 
    };

    // Функция для получения ингредиентов
    const fetchDataIngredients = async () => {
        const response = await get('ingredients');
        setAvailableIngredients(response.ingredients); 
    };

    // Загрузка данных при монтировании компонента
    useEffect(() => {
        const fetchData = async () => {
            try {
                await Promise.all([fetchDataTypes(), fetchDataIngredients()]);
            } catch (error) {
                console.error('Error fetching data:', error);
            }
        };

        fetchData();
    }, []);

    // Обработчик изменения фильтров
    const handleFilterChange = (updatedFilters) => {
        setFilters((prev) => {
            const newFilters = { ...prev, ...updatedFilters };
            return newFilters;
        });
    };

    // Используем useEffect для вызова onFilterChange после изменения фильтров
    useEffect(() => {
        if (prevFilters && JSON.stringify(prevFilters) !== JSON.stringify(filters)) {
            // Передаем обновленные фильтры только если они изменились
            onFilterChange(filters);
        }
        prevFiltersRef.current = filters; // Обновляем ссылку на предыдущее состояние
    }, [filters, onFilterChange]); // Фильтры изменяются, и onFilterChange вызывается с новыми значениями

    // Функция для обновления выбранных типов блюд
    const toggleType = (selectedOptions) => {
        const updatedTypes = selectedOptions.map(option => option.value);
        handleFilterChange({ types: updatedTypes });
    };

    // Функция для обновления выбранных ингредиентов
    const toggleIngredient = (selectedOptions) => {
        const updatedIngredients = selectedOptions.map(option => option.value);
        handleFilterChange({ ingredients: updatedIngredients });
    };

    // Обновление диапазона времени
    const updateTimeRange = (min, max) => {
        handleFilterChange({ minTime: min, maxTime: max });
    };

    // Обновление сортировки
    const handleSortChange = (e) => {
        handleFilterChange({ sortBy: e.target.value });
    };

    return (
        <aside>
            <div className="sort">
                Сортировать по
                <select className="selectInput" value={filters.sortBy} onChange={handleSortChange}>
                    <option value="prepareTime">Время приготовления</option>
                    <option value="yourTime">Ваше время</option>
                </select>
            </div>

            <div className="filtration">
                <div>Тип блюда</div>
                <Select
                    isMulti
                    options={availableTypes.map(type => ({ value: type.id, label: type.typeName }))}
                    value={availableTypes.filter(type => filters.types.includes(type.id)).map(type => ({ value: type.id, label: type.typeName }))}
                    onChange={toggleType}
                    placeholder="Выберите типы блюд"
                />

                <div>Ингредиенты</div>
                <Select
                    isMulti
                    options={availableIngredients.map(ingredient => ({ value: ingredient.id, label: ingredient.ingredientName }))}
                    value={availableIngredients.filter(ingredient => filters.ingredients.includes(ingredient.id)).map(ingredient => ({ value: ingredient.id, label: ingredient.ingredientName }))}
                    onChange={toggleIngredient}
                    placeholder="Выберите ингредиенты"
                />

                <TimeSlider
                    minTime={filters.minTime}
                    maxTime={filters.maxTime}
                    onTimeChange={updateTimeRange}
                />
            </div>
        </aside>
    );
};

export default Sidebar;
