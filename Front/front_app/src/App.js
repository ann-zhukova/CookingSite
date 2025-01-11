import React from 'react';
import './assets/css/index.css';
import TimeSlider from "./components/TimeSlider";
import Footer from "./components/Footer";
import Header from "./components/Header";
import {Route, Routes} from "react-router-dom";
import AboutUs from "./pages/AboutUs";
import Login from "./pages/Login";
import Register from "./pages/Register";
import Account from "./pages/Account";
import AddRecipe from "./pages/AddRecipe";
import MyRecipes from "./pages/MyRecipies";
import Favorites from "./pages/Favorites";
import Main from "./pages/Main";

const App = () => (
    <div>
        <Header />
        <Routes>
            <Route path="/" element={<Main />} />
            <Route path="/about-us" element={<AboutUs />} />
            <Route path="/account" element={<Account/>}/>
            <Route path="/login" element={<Login/>}/>
            <Route path="/register" element={<Register/>}/>
            <Route path="/add_recipe" element={<AddRecipe/>}/>
            <Route path="/my-recipes" element={<MyRecipes/>}/>
            <Route path="/favorites" element={<Favorites/>}/>
        </Routes>
        <Footer />
    </div>
);

export default App;
