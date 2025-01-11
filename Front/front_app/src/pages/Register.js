import React, { useState } from 'react';
import axios from 'axios';
import '../assets/css/index.css';
import '../assets/css/auth.css';
import {Link} from "react-router-dom";

const Register = () => {
    const [username, setUsername] = useState('');
    const [email, setEmail] = useState('');
    const [password, setPassword] = useState('');
    const [loading, setLoading] = useState(false); // Для индикации загрузки

    const validatePassword = (password) => {
        if (password.length < 6) {
            alert('Пароль должен содержать не менее 6 символов');
            return false;
        }
        return true;
    };

    const handleSubmit = async (e) => {
        e.preventDefault();

        if (!validatePassword(password)) return; // Если пароль не проходит валидацию, не отправляем запрос

        setLoading(true); // Показываем индикатор загрузки

        try {
            const response = await axios.post('users/register', {
                username,
                email,
                password
            });

            // Если сервер вернул успешный ответ
            if (response.status === 200) {
                alert('Вы успешно зарегистрировались!');
            }
        } catch (error) {
            console.error('Ошибка при регистрации:', error);
            alert('Ошибка при регистрации. Попробуйте снова.');
        } finally {
            setLoading(false); // Скрываем индикатор загрузки
        }
    };

    return (
        <div>
            <main>
                <div className="registration-form">
                    <h2>Регистрация</h2>
                    <form onSubmit={handleSubmit}>
                        <div className="form-group">
                            <label htmlFor="username">Имя пользователя</label>
                            <input
                                type="text"
                                id="username"
                                name="username"
                                value={username}
                                onChange={(e) => setUsername(e.target.value)}
                                required
                            />
                        </div>
                        <div className="form-group">
                            <label htmlFor="email">Электронная почта</label>
                            <input
                                type="email"
                                id="email"
                                name="email"
                                value={email}
                                onChange={(e) => setEmail(e.target.value)}
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
                                {loading ? 'Загрузка...' : 'Зарегистрироваться'}
                            </button>
                        </div>
                        <Link to="/login">Есть аккаунт? Войдите</Link>
                    </form>
                </div>
            </main>
        </div>
    );
};

export default Register;
