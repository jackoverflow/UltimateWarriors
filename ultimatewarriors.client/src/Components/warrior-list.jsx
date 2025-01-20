import React, { useState, useEffect } from 'react';
import axios from 'axios';
import Swal from 'sweetalert2';

const WarriorList = () => {
    const [warriors, setWarriors] = useState([]);

    useEffect(() => {
        fetchWarriors();
    }, []);

    const fetchWarriors = async () => {
        try {
            const response = await axios.get('http://localhost:5108/api/Ultimatewarriors/warriors');
            setWarriors(response.data);
        } catch (error) {
            console.error('Error fetching warriors:', error);
            Swal.fire({
                title: 'Error!',
                text: 'Failed to load warriors. Please try again.',
                icon: 'error',
                confirmButtonColor: '#d33'
            });
        }
    };

    const handleDelete = async (id) => {
        try {
            await axios.delete(`http://localhost:5108/api/Ultimatewarriors/warriors/${id}`);
            Swal.fire({
                title: 'Success!',
                text: 'Warrior deleted successfully',
                icon: 'success',
                confirmButtonColor: '#3085d6'
            });
            fetchWarriors(); // Refresh the list
        } catch (error) {
            console.error('Error deleting warrior:', error);
            Swal.fire({
                title: 'Error!',
                text: 'Failed to delete warrior. Please try again.',
                icon: 'error',
                confirmButtonColor: '#d33'
            });
        }
    };

    return (
        <div>
            <h2>Warriors List</h2>
            <table>
                <thead>
                    <tr>
                        <th>ID</th>
                        <th>Name</th>
                        <th>Description</th>
                        <th>Actions</th>
                    </tr>
                </thead>
                <tbody>
                    {warriors.map(warrior => (
                        <tr key={warrior.id}>
                            <td>{warrior.id}</td>
                            <td>{warrior.name}</td>
                            <td>{warrior.description}</td>
                            <td>
                                <button 
                                    onClick={() => handleDelete(warrior.id)}
                                    style={{ backgroundColor: '#ff4444', color: 'white', border: 'none', padding: '5px 10px', cursor: 'pointer' }}
                                >
                                    Delete
                                </button>
                            </td>
                        </tr>
                    ))}
                </tbody>
            </table>
        </div>
    );
};

export default WarriorList;
