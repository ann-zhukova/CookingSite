import React, { useEffect, useState, useRef  } from 'react';
import { get } from "../services/requests";
//import '../assets/css/account.css'
import {Link} from "react-router-dom";

const Cabinet = () => {
    const [userName, setUserName] = useState('');
    // Проверка аутентификации
    useEffect(() => {
        const fetchUserName = async () => {
            try {
                const response = await get('users/account');
                if (response && response.userName) {
                    setUserName(response.userName);
                }
                if (response && response.serials) {
                    setSerials(response.serials);
                }
                console.log(response);
            } catch (error) {
                console.error('Error fetching user name:', error);
                setUserName(null);
            }
        };

        fetchUserName();
    }, []);
    
    return (
        <main className="main-cabinet">
            {/* Заголовок с именем пользователя и кнопками */}
            <div className="cabinet-header">
                <div className="buttons-container">
                    <h1>{userName}</h1>
                    <button className="logout-button">Выход</button>
                </div>
            </div>
        </main>
    );
};

export default Cabinet;
