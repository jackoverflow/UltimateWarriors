import { useState, useEffect } from 'react';

export default function WarriorList() {
    const [warriors, setWarriors] = useState([]);
    const [loading, setLoading] = useState(true);
    const [error, setError] = useState(null);

    useEffect(() => {
        fetchWarriors();
    }, []);

    const fetchWarriors = async () => {
        try {
            const response = await fetch('http://localhost:5108/api/ultimatewarriors/warriors');
            if (!response.ok) {
                throw new Error('Failed to fetch warriors');
            }
            const data = await response.json();
            setWarriors(data);
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
            <h2>Warriors List</h2>
            <table className="table table-striped">
                <thead>
                    <tr>
                        <th>ID</th>
                        <th>Name</th>
                        <th>Description</th>
                    </tr>
                </thead>
                <tbody>
                    {warriors.map(warrior => (
                        <tr key={warrior.id}>
                            <td>{warrior.id}</td>
                            <td>{warrior.name}</td>
                            <td>{warrior.description}</td>
                        </tr>
                    ))}
                </tbody>
            </table>
        </div>
    );
}
