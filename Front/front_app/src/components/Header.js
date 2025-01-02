import React from "react";

const Header = () => (
    <header className="header">
        <img className="logo" src={require("../assets/images/logo.png")} alt="logo" />
        <nav>
            <a href="/" className="header-block">Рецепты</a>
            <a href="/favorite" className="header-block">Избранное</a>
            <a href="/my-recipes" className="header-block">Мои рецепты</a>
        </nav>
        <a href="/account" className="cabinet-link">Анна</a>
    </header>
);

export default Header;