import React from "react";
import {Link} from "react-router-dom";

const Card = ({ id, image, name, prepareTime, yourTime }) => (
    <article className="card">
        <img src={image} className="card-img" alt={name} />
        <div className="card-body">
            <div className="card-header">
                <Link to={`recipe/${id}`}>{name}</Link>
            </div>
            {/*<div className="card-tags">{tags.join(", ")}</div>*/}
            <div className="card-description">Время приготовления: {prepareTime} минут </div>
            <div className="card-description">Ваше время: {yourTime} минут</div>
        </div>
    </article>
);

export default Card;