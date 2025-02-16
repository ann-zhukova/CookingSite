import React, { useState, useEffect } from 'react';
import axios from 'axios';
import Cookies from 'cookie';
import '../assets/css/index.css';
import '../assets/css/auth.css';
import {Link} from "react-router-dom";
import { useNavigate } from 'react-router-dom';

// Функция для получения значения cookie
const getCookie = (name) => {
    if (!document.cookie || document.cookie === '') {
        return null;
    }
    const cookies = document.cookie.split(';');
    for (let i = 0; i < cookies.length; i++) {
        let cookie = cookies[i].trim();
        if (cookie.startsWith(name + '=')) {
            return decodeURIComponent(cookie.substring(name.length + 1));
        }
    }
    return null;
};

const Login = () => {
    const navigate = useNavigate();

    const [userName, setUserName] = useState('');
    const [password, setPassword] = useState('');
    const [loading, setLoading] = useState(false); // Для индикации загрузки

    // Проверяем наличие токена и редиректим, если он уже существует
    useEffect(() => {
        const token = getCookie('auth_token');
        if (token) {
            history.push('/account');
        }
    }, [history]);

    const validatePassword = (password) => {
        if (password.length < 6) {
            alert('Пароль должен содержать не менее 6 символов');
            return false;
        }
        return true;
    };
    const handleSubmit = async (e) => {
        e.preventDefault();
        if (!validatePassword(password)) return;
        try {
            setLoading(true);
            const response = await axios.post('/users/login', {
                username: userName,
                password: password,
            }, {
                headers: {
                    'Content-Type': 'application/json',
                    'X-CSRFToken': getCookie('csrftoken'),
                },
                withCredentials: true,
            });
            window.location.replace('/account')
        } catch (error) {
            console.error('Ошибка при аутентификации:', error);
            alert('Неверный логин или пароль');
            setLoading(false); // Скрываем индикатор загрузки
        }

    };

    return (
        <div>
            <main>
                <div className="registration-form">
                    <h2>Вход</h2>
                    <form onSubmit={handleSubmit}>
                        <div className="form-group">
                            <label htmlFor="email">Имя пользователя</label>
                            <input
                                id="email"
                                name="email"
                                value={userName}
                                onChange={(e) => setUserName(e.target.value)}
                                required
                            />
                        </div>
                        <div className="form-group">
                            <label htmlFor="password">Пароль</label>
                            <input
                                type="password"
                                id="password"
                                name="password"
                                value={password}
                                onChange={(e) => setPassword(e.target.value)}
                                required
                            />
                        </div>
                        <div className="form-group">
                            <button type="submit" disabled={loading}>
                                {loading ? 'Вход...' : 'Войти'}
                            </button>
                        </div>
                        <Link to="/register">Нет аккаунта? Зарегистрируйтесь</Link>
                    </form>
                </div>
            </main>
        </div>
    );
};

export default Login;
