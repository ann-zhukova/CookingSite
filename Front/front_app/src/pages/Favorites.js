import React, { useState, useRef, useEffect } from "react";
import {get} from "../services/requests"
import Card from "../components/Card";
import "../assets/css/favorite.css"; 

const Favorites = () => {

    useEffect(() => {
        const fetchRecipes = async () => {
            try {
                const response = await get('users/recipes/favorites');
                setRecipes(response.recipes);
            } catch (error) {
                alert('Ошибка при загрузке избранных рецептов.');
                console.error(error);
            }
        };

        fetchRecipes();
    }, []);

    // Данные рецептов
    const [recipes, setRecipes] = useState([
        {
            imgSrc: "../img/pi.jpg",
            title: "Цветаевский пирог",
            tags: ["десерты", "пироги"],
            description: "ягоды, сметана, сахар, мука, крахмал, яйца",
            link: "/recipe/1",
        },
        {
            imgSrc: "../img/sup.jpg",
            title: "Грибной крем-суп",
            tags: ["первое", "крем-суп"],
            description: "шампиньоны, картофель, сливки, лук, масло",
            link: "/recipe/2",
        },
        {
            imgSrc: "../img/lasagna.jpg",
            title: "Лазанья",
            tags: ["второе", "мясное"],
            description: "макаронные листы, мясо, сыр, молоко, томатная паста, сельдерей",
            link: "/recipe/3",
        },
        {
            imgSrc: "../img/chicken.JPG",
            title: "Курица в томатном соусе",
            tags: ["второе", "мясное"],
            description: "курица, томаты, болгарский перец, томатная паста, чеснок",
            link: "/recipe/4",
        },
    ]);

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
                            image={recipe.imgSrc}
                            name={recipe.title}
                            tags={recipe.tags}
                            description={recipe.description}
                            link={recipe.link}
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
