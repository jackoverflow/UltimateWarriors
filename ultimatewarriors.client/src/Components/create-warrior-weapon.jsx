import React, { useState, useEffect } from 'react';
import { TextField, Button, Select, MenuItem, FormControl, InputLabel, Box } from '@mui/material';

const CreateWarriorWeapon = () => {
    const [formData, setFormData] = useState({
        name: '',
        description: '',
        weaponIds: []
    });
    const [weapons, setWeapons] = useState([]);
    const [loading, setLoading] = useState(false);

    // Fetch available weapons when component mounts
    useEffect(() => {
        fetchWeapons();
    }, []);

    const fetchWeapons = async () => {
        try {
            const response = await fetch('/api/ultimatewarriors/weapons');
            if (response.ok) {
                const data = await response.json();
                setWeapons(data);
            }
        } catch (error) {
            console.error('Error fetching weapons:', error);
        }
    };

    const handleInputChange = (e) => {
        const { name, value } = e.target;
        setFormData(prev => ({
            ...prev,
            [name]: value
        }));
    };

    const handleWeaponSelect = (event) => {
        setFormData(prev => ({
            ...prev,
            weaponIds: event.target.value
        }));
    };

    const handleSubmit = async (e) => {
        e.preventDefault();
        setLoading(true);

        try {
            const response = await fetch('/api/ultimatewarriors/warrior-with-weapons', {
                method: 'POST',
                headers: {
                    'Content-Type': 'application/json',
                },
                body: JSON.stringify(formData)
            });

            if (response.ok) {
                // Reset form after successful submission
                setFormData({
                    name: '',
                    description: '',
                    weaponIds: []
                });
                alert('Warrior created successfully!');
            } else {
                const error = await response.text();
                alert(`Failed to create warrior: ${error}`);
            }
        } catch (error) {
            console.error('Error creating warrior:', error);
            alert('Error creating warrior');
        } finally {
            setLoading(false);
        }
    };

    return (
        <Box
            component="form"
            onSubmit={handleSubmit}
            sx={{
                display: 'flex',
                flexDirection: 'column',
                gap: 2,
                maxWidth: 400,
                margin: '0 auto',
                padding: 2
            }}
        >
            <TextField
                required
                name="name"
                label="Warrior Name"
                value={formData.name}
                onChange={handleInputChange}
                fullWidth
            />

            <TextField
                name="description"
                label="Description"
                value={formData.description}
                onChange={handleInputChange}
                fullWidth
                multiline
                rows={3}
            />

            <FormControl fullWidth>
                <InputLabel>Select Weapons</InputLabel>
                <Select
                    multiple
                    value={formData.weaponIds}
                    onChange={handleWeaponSelect}
                    label="Select Weapons"
                >
                    {weapons.map((weapon) => (
                        <MenuItem key={weapon.id} value={weapon.id}>
                            {weapon.name}
                        </MenuItem>
                    ))}
                </Select>
            </FormControl>

            <Button
                type="submit"
                variant="contained"
                color="primary"
                disabled={loading}
                sx={{ mt: 2 }}
            >
                {loading ? 'Creating...' : 'Create Warrior'}
            </Button>
        </Box>
    );
};

export default CreateWarriorWeapon;
