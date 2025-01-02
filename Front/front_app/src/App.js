import React from 'react';
import './assets/css/index.css';
import TimeSlider from "./components/TimeSlider";
import Footer from "./components/Footer";
import Header from "./components/Header";
import {Route, Routes} from "react-router-dom";
import AboutUs from "./pages/AboutUs";

const Sidebar = () => (
    <aside>
        <div className="sort">
            Сортировать по
            <select className="selectInput">
                <option>время приготовления</option>
            </select>
        </div>
        <div className="filtration">
           <TimeSlider/>
            <div>Тип блюда</div>
            <select className="selectInput">
                <option>Выберете тип</option>
                <option>Первое</option>
                <option>Второе</option>
                <option>Десерты</option>
                <option>Пироги</option>
            </select>
            <div>Ингредиенты</div>
            <select className="selectInput">
                <option>Выберете ингредиенты</option>
                <option>Мясо</option>
                <option>Курица</option>
                <option>Ягоды</option>
                <option>Томаты</option>
            </select>
        </div>
    </aside>
);

const Card = ({ imgSrc, title, tags, description, link }) => (
    <article className="card">
        <img src={imgSrc} className="card-img" alt={title} />
        <div className="card-body">
            <div className="card-header">
                {link ? <a href={link}>{title}</a> : title}
            </div>
            <div className="card-tags">{tags.join(", ")}</div>
            <div className="card-description">{description}</div>
        </div>
    </article>
);

const Main = () => {
    const cards = [
        {
            imgSrc: require("./assets/images/pi.jpg"),
            title: 'Цветаевский пирог',
            tags: ['десерты', 'пироги'],
            description: 'ягоды, сметана, сахар, мука, крахмал, яица',
            link: '/recipe/tsvetaevsky-pie'
        },
        {
            imgSrc: require('./assets/images/sup.jpg'),
            title: 'Грибной крем суп',
            tags: ['первое', 'крем-суп'],
            description: 'шампиньоны, картофель, сливки, лук, масло'
        },
        {
            imgSrc: require('./assets/images/lasagna.jpg'),
            title: 'Лазанья',
            tags: ['второе', 'мясное'],
            description: 'макаронные листы, мясо, сыр, молоко, томатная паста, сельдерей'
        },
        {
            imgSrc: require('./assets/images/chicken.JPG'),
            title: 'Курица в томатном соусе',
            tags: ['второе', 'мясное'],
            description: 'курица, томаты, болгарский перец, томатная паста, чеснок'
        },
        {
            imgSrc: require('./assets/images/bbq.jpg'),
            title: 'Свинные ребра в соусе BBQ',
            tags: ['второе', 'мясное'],
            description: 'свинные ребра, чеснок, томатная паста, горчица'
        },
        {
            imgSrc: require('./assets/images/applePi.jpg'),
            title: 'Яблочный пирог',
            tags: ['десерты', 'пироги'],
            description: 'яблоки, масло, сахар, яица, мука'
        }
    ];

    return (
        <main>
            <Sidebar />
            <section>
                {cards.map((card, index) => (
                    <Card key={index} {...card} />
                ))}
            </section>
        </main>
    );
};

const App = () => (
    <div>
        <Header />
        <Routes>
            <Route path="/" element={<Main />} />
            <Route path="/about-us" element={<AboutUs />} />
        </Routes>
        <Footer />
    </div>
);

export default App;
