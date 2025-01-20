import { useState, useEffect } from 'react';

export default function WeaponList() {
    const [weapons, setWeapons] = useState([]);
    const [loading, setLoading] = useState(true);
    const [error, setError] = useState(null);

    useEffect(() => {
        fetchWeapons();
    }, []);

    const fetchWeapons = async () => {
        try {
            const response = await fetch('api/ultimatewarriors/weapons');
            if (!response.ok) {
                throw new Error('Failed to fetch weapons');
            }
            const data = await response.json();
            setWeapons(data);
            setLoading(false);
        } catch (err) {
            setError(err.message);
            setLoading(false);
        }
    };

    if (loading) return <div>Loading weapons...</div>;
    if (error) return <div>Error: {error}</div>;

    return (
        <div className="container mt-4">
            <h2>Weapons List</h2>
            <table className="table table-striped">
                <thead>
                    <tr>
                        <th>ID</th>
                        <th>Name</th>
                        <th>Description</th>
                    </tr>
                </thead>
                <tbody>
                    {weapons.map(weapon => (
                        <tr key={weapon.id}>
                            <td>{weapon.id}</td>
                            <td>{weapon.name}</td>
                            <td>{weapon.description}</td>
                        </tr>
                    ))}
                </tbody>
            </table>
        </div>
    );
}
