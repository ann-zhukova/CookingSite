import React, { useState, useRef, useEffect } from "react";
import {get} from "../services/requests"
import Card from "../components/Card";
import "../assets/css/favorite.css"; 

const Favorites = () => {
    const [recipes, setRecipes] = useState([]);
    useEffect(() => {
        const fetchRecipes = async () => {
            try {
                const response = await get('users/recipes/favorites');
                console.log(response);
                setRecipes(response.recipes);
            } catch (error) {
                alert('Ошибка при загрузке избранных рецептов.');
                console.error(error);
            }
        };

        fetchRecipes();
    }, []);
    
    // Прокрутка по горизонтали
    const scrollContainerRef = useRef();

    const scrollLeft = () => {
        scrollContainerRef.current.scrollBy({ left: -300, behavior: "smooth" });
    };

    const scrollRight = () => {
        scrollContainerRef.current.scrollBy({ left: 300, behavior: "smooth" });
    };

    return (
        <section>
            <h1>Избранное</h1>
            <div className="scrollContainer" ref={scrollContainerRef}>
                {recipes.map((recipe, index) => (
                    <div className="scrollable-item" key={index}>
                        <Card
                            image={recipe.image}
                            name={recipe.name}
                            tags={recipe.tags}
                            description={recipe.description}
                            prepareTime={recipe.prepareTime}
                            yourTime={recipe.yourTime}
                        />
                    </div>
                ))}
            </div>
            <div className="scrollMenu">
                <button id="scrollLeftButton" onClick={scrollLeft}>
                    &larr;
                </button>
                <button id="scrollRightButton" onClick={scrollRight}>
                    &rarr;
                </button>
            </div>
        </section>
    );
};

export default Favorites;
