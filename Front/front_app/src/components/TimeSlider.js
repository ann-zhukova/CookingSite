import React, { useState, useEffect } from 'react';

const TimeSlider = ({ minTime = 1, maxTime = 720, onTimeChange }) => {
    const [minValue, setMinValue] = useState(minTime);
    const [maxValue, setMaxValue] = useState(maxTime);

    // Обновляем значения времени при изменении слайдера
    useEffect(() => {
        onTimeChange(minValue, maxValue);
    }, [minValue, maxValue]);

    return (
        <div>
            <div className="slider-label">Время приготовления (минуты)</div>
            <div className="slider">
                <label className="label-min-value">Мин. время {minValue}</label>
                <label className="label-max-value">Макс. время {maxValue}</label>
            </div>
            <div className="rangeslider">
                <input
                    className="min input-ranges"
                    type="range"
                    min="1"
                    max="720"
                    value={minValue}
                    onChange={(e) => setMinValue(Number(e.target.value))}
                />
                <input
                    className="max input-ranges"
                    type="range"
                    min="1"
                    max="720"
                    value={maxValue}
                    onChange={(e) => setMaxValue(Number(e.target.value))}
                />
            </div>
        </div>
    );
};

export default TimeSlider;
