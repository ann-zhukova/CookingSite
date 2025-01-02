import React, { useState, useEffect } from 'react';

const TimeSlider = () => {
    // State to hold the min and max values
    const [minValue, setMinValue] = useState(15);
    const [maxValue, setMaxValue] = useState(60);

    // Effect to update the displayed values when the component mounts
    useEffect(() => {
        updateValues();
    }, []);

    // Function to update the values
    const updateValues = () => {
        // This function can be used if you need to perform any side effects
        // when the values change, but in this case, it's not necessary.
    };

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