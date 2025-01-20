import { useEffect, useState } from 'react';
import './App.css';
import { Link } from 'react-router-dom';

function App() {
    const [forecasts, setForecasts] = useState();

    useEffect(() => {
        populateWeatherData();
    }, []);

    return (
        <div>
            <h1 id="tableLabel">Weather Forecast</h1>
            <p>This component demonstrates fetching data from the server.</p>
            <p>
                <Link to="/create-weapon">Create a New Weapon</Link>
            </p>
        </div>
    );
    
    async function populateWeatherData() {
        const response = await fetch('weatherforecast');
        if (response.ok) {
            const data = await response.json();
            setForecasts(data);
        }
    }
}

export default App;