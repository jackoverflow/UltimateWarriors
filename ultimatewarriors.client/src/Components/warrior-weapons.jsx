import React, { useEffect, useState } from 'react';
import axios from 'axios';

const WarriorWeapons = () => {
    const [warriorWeapons, setWarriorWeapons] = useState([]);

    useEffect(() => {
        const fetchWarriorWeapons = async () => {
            try {
                const response = await axios.get('http://localhost:5108/api/Ultimatewarriors/warrior-weapon');
                // Assuming the response format is different, adjust accordingly
                setWarriorWeapons(response.data); // Adjust this line based on the actual response structure
            } catch (error) {
                console.error('Error fetching warrior weapons:', error);
            }
        };

        fetchWarriorWeapons();
    }, []);

    return (
        <div>
            <h2>Warrior Weapons</h2>
            <ul>
                {warriorWeapons.map((item) => (
                    <li key={item.WarriorId}>
                        Warrior ID: {item.WarriorId}, Weapon ID: {item.WeaponId}
                    </li>
                ))}
            </ul>
        </div>
    );
};

export default WarriorWeapons;
