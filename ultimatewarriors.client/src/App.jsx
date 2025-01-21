import { useEffect, useState } from 'react';
import './App.css';
import { Link, Routes, Route, BrowserRouter } from 'react-router-dom';
import React from 'react';
import CreateWeapon from './Components/create-weapon.jsx';
import WeaponList from './Components/weapon-list.jsx';
import CreateWarrior from './Components/create-warrior.jsx';
import WarriorList from './Components/warrior-list.jsx';
import CreateWarriorWithWeapons from './Components/create-warrior-weapons.jsx';

class ErrorBoundary extends React.Component {
    constructor(props) {
        super(props);
        this.state = { hasError: false };
    }

    static getDerivedStateFromError(error) {
        return { hasError: true };
    }

    componentDidCatch(error, errorInfo) {
        console.error("Error caught in ErrorBoundary: ", error, errorInfo);
    }

    render() {
        if (this.state.hasError) {
            return <h1>Something went wrong.</h1>;
        }

        return this.props.children; 
    }
}

function App() {
    const [forecasts, setForecasts] = useState();
    const apiUrl = process.env.REACT_APP_API_URL;

    useEffect(() => {
        populateWeatherData();
    }, []);

    return (
        <div>
            <h1>Ultimate Warriors</h1>
            <nav>
                <p><Link to="/create-weapon">Create a New Weapon</Link></p>
                <p><Link to="/weapons">View All Weapons</Link></p>
                <p><Link to="/create-warrior">Create a New Warrior</Link></p>
                <p><Link to="/warriors">View All Warriors</Link></p>
                <p><Link to="/create-warrior-with-weapons">Create Warrior with Weapons</Link></p>
            </nav>
            <Routes>
                <Route path="/" element={<h1>Welcome to the Ultimate Warriors!</h1>} />
                <Route path="/create-weapon" element={<CreateWeapon />} />
                <Route path="/weapons" element={<WeaponList />} />
                <Route path="/create-warrior" element={<CreateWarrior />} />
                <Route path="/warriors" element={<WarriorList />} />
                <Route path="/create-warrior-with-weapons" element={<CreateWarriorWithWeapons />} />
            </Routes>
        </div>
    );
    
    async function populateWeatherData() {
        const response = await fetch(`${apiUrl}/weatherforecast`);
        if (response.ok) {
            const data = await response.json();
            setForecasts(data);
        }
    }
}

export default function AppWrapper() {
    return (
        <BrowserRouter>
            <ErrorBoundary>
                <App />
            </ErrorBoundary>
        </BrowserRouter>
    );
}