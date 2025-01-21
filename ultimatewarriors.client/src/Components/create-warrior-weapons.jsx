import React, { useState, useEffect } from 'react';
import { useForm } from 'react-hook-form';
import axios from 'axios';
import Swal from 'sweetalert2';

const CreateWarriorWithWeapons = () => {
    const { register, handleSubmit, reset } = useForm();
    const [weapons, setWeapons] = useState([]);
    const [selectedWeapons, setSelectedWeapons] = useState([]);

    useEffect(() => {
        // Fetch all weapons
        const fetchWeapons = async () => {
            try {
                const response = await axios.get('http://localhost:5108/api/ultimatewarriors/weapons');
                setWeapons(response.data);
            } catch (error) {
                console.error('Error fetching weapons:', error);
            }
        };
        fetchWeapons();
    }, []);

    const onSubmit = async (data) => {
        const payload = {
            Name: data.Name,
            Description: data.Description,
            WeaponIds: selectedWeapons // Array of selected weapon IDs
        };

        try {
            const response = await axios.post('http://localhost:5108/api/ultimatewarriors', payload);
            console.log('Warrior created:', response.data);
            Swal.fire({
                title: 'Success!',
                text: 'Warrior created successfully',
                icon: 'success',
                confirmButtonColor: '#3085d6'
            });
            reset();
            setSelectedWeapons([]); // Reset selected weapons
        } catch (error) {
            console.error('Error creating warrior:', error);
            Swal.fire({
                title: 'Error!',
                text: 'Failed to create warrior. Please try again.',
                icon: 'error',
                confirmButtonColor: '#d33'
            });
        }
    };

    const handleWeaponChange = (event) => {
        const value = Array.from(event.target.selectedOptions, option => option.value);
        setSelectedWeapons(value);
    };

    return (
        <div>
            <h2>Create Warrior with Weapons</h2>
            <form onSubmit={handleSubmit(onSubmit)}>
                <div>
                    <label>Name:</label>
                    <input type="text" {...register('Name', { required: 'Name is required' })} />
                </div>
                <div>
                    <label>Description:</label>
                    <input type="text" {...register('Description', { required: 'Description is required' })} />
                </div>
                <div>
                    <label>Weapons:</label>
                    <select multiple onChange={handleWeaponChange}>
                        {weapons.map(weapon => (
                            <option key={weapon.id} value={weapon.id}>
                                {weapon.name}
                            </option>
                        ))}
                    </select>
                </div>
                <button type="submit">Create Warrior</button>
            </form>
        </div>
    );
};

export default CreateWarriorWithWeapons;