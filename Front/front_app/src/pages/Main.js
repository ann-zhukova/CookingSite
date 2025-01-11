import { useState, useEffect } from 'react';
import Sidebar from '../components/Sidebar';
import Card from '../components/Card';
import {getRecipes} from "../services/recipesService";

const Main = () => {
    const [cards, setCards] = useState([]);
    const [filters, setFilters] = useState({ types: [], ingredients: [] });
    const [currentPage, setCurrentPage] = useState(1);
    const [totalPages, setTotalPages] = useState(1);
    const [activeFilters, setActiveFilters] = useState({});

    // Получение данных с бэка
    const fetchCards = async (filters = {}, page = 1) => {
        try {
            const response = await getRecipes({ ...filters, page: currentPage });
            console.log(response);
            setCards(response.recipes);
            setTotalPages(response.totalPages);
            setCurrentPage(response.currentPage)
        } catch (error) {
            console.error('Ошибка при загрузке данных:', error);
        }
    };
    
    // Обновление фильтров
    const handleFilterChange = (newFilters) => {
        setActiveFilters(newFilters);
        fetchCards(newFilters, 1);
    };

    // Переход на следующую страницу
    const handleNextPage = () => {
        if (currentPage < totalPages) {
            const nextPage = currentPage + 1;
            setCurrentPage(nextPage);
            fetchCards(activeFilters, nextPage);
        }
    };

    // Переход на предыдущую страницу
    const handlePrevPage = () => {
        if (currentPage > 1) {
            const prevPage = currentPage - 1;
            setCurrentPage(prevPage);
            fetchCards(activeFilters, prevPage);
        }
    };

    // Загрузка фильтров и рецептов при первом рендере
    useEffect(() => {
        fetchCards();
    }, []);

    return (
        <main>
            <Sidebar filters={filters} onFilterChange={handleFilterChange} />
            <section>
                {cards.map((card, index) => (
                    <Card key={index} {...card} />
                ))}
                <div className="pagination">
                    <button onClick={handlePrevPage} disabled={currentPage === 1}>
                        Назад
                    </button>
                    <span>
                    Страница {currentPage} из {totalPages}
                </span>
                    <button onClick={handleNextPage} disabled={currentPage === totalPages}>
                        Вперед
                    </button>
                </div>
            </section>
        </main>
    );
};

export default Main;
