import { useState, useEffect } from 'react';
import axios from 'axios';

const WarriorWeaponsList = () => {
    const [warriors, setWarriors] = useState([]);
    const [loading, setLoading] = useState(true);
    const [error, setError] = useState(null);

    useEffect(() => {
        fetchWarriorsWithWeapons();
    }, []);

    const fetchWarriorsWithWeapons = async () => {
        try {
            const response = await axios.get('http://localhost:5108/api/ultimatewarriors/warriors-with-weapons');
            setWarriors(response.data);
            setLoading(false);
        } catch (err) {
            setError(err.message);
            setLoading(false);
        }
    };

    if (loading) return <div>Loading warriors...</div>;
    if (error) return <div>Error: {error}</div>;

    return (
        <div className="container mt-4">
            <h2>Warriors and Their Weapons</h2>
            <table className="table table-striped">
                <thead>
                    <tr>
                        <th>ID</th>
                        <th>Name</th>
                        <th>Description</th>
                        <th>Weapons</th>
                    </tr>
                </thead>
                <tbody>
                    {warriors.map(warrior => (
                        <tr key={warrior.id}>
                            <td>{warrior.id}</td>
                            <td>{warrior.name}</td>
                            <td>{warrior.description}</td>
                            <td>
                                {warrior.weapons.length > 0 ? (
                                    <ul>
                                        {warrior.weapons.map(weapon => (
                                            <li key={weapon.id}>{weapon.name}</li>
                                        ))}
                                    </ul>
                                ) : (
                                    <span>No weapons assigned</span>
                                )}
                            </td>
                        </tr>
                    ))}
                </tbody>
            </table>
        </div>
    );
};

export default WarriorWeaponsList;
