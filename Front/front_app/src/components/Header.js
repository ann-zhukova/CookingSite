import React, {useEffect, useState} from "react";
import {Link} from "react-router-dom";
import { get } from "../services/requests";

const Header = () => {
    const [userName, setUserName] = useState('');

    // Проверка аутентификации
    useEffect(() => {
        // Здесь предполагается, что ваш сервер отправляет имя пользователя на endpoint /account, если он авторизован
        const fetchUserName = async () => {
            try {
                const response = await get('users/account');
                if (response && response.userName) {
                    setUserName(response.userName);
                }
            } catch (error) {
                console.error('Error fetching user name:', error);
                setUserName(null);
            }
        };

        fetchUserName();
    }, []);
    return(
    <header className="header">
        <img className="logo" src={require("../assets/images/logo.png")} alt="logo" />
        <nav>
            <Link to="/" className="header-block">Рецепты</Link>
            <Link to="/favorites" className="header-block">Избранное</Link>
            <Link to="/my-recipes" className="header-block">Мои рецепты</Link>
        </nav>
        {userName ? (
            // Если пользователь авторизован, показываем его имя и ссылку на кабинет
            <Link to="/account" className="cabinet-link">{userName}</Link>
        ) : (
            // Если пользователь не авторизован, показываем ссылку на логин
            <Link to="/login" className="cabinet-link">Войти</Link>
        )}
    </header>
)};

export default Header;