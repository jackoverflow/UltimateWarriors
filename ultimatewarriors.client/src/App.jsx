import { useEffect, useState } from 'react';
import './App.css';
import { Link, Routes, Route, BrowserRouter } from 'react-router-dom';
import React from 'react';
import CreateWeapon from './components/create-weapon.jsx';

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
            <h1 id="tableLabel">Weather Forecast</h1>
            <p>This component demonstrates fetching data from the server.</p>
            <p>
                <Link to="/create-weapon">Create a New Weapon</Link>
            </p>
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
                <Routes>
                    <Route path="/" element={<h1>Welcome to the Weapon Creator!</h1>} />
                    <Route path="/create-weapon" element={<CreateWeapon />} />
                </Routes>
            </ErrorBoundary>
        </BrowserRouter>
    );
}