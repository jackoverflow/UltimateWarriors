import React from 'react';
import { useForm } from 'react-hook-form';
import axios from 'axios';

const CreateWeapon = () => {
    const { register, handleSubmit, formState: { errors } } = useForm();

    const onSubmit = async (data) => {
        const payload = {
            Name: data.Name,
            Description: data.Description
        };

        // Debugging line to check the API URL
        console.log('API URL:', process.env.REACT_APP_API_URL); 

        try {
            // Ensure no extra '/' in the URL
            const response = await axios.post(`${process.env.REACT_APP_API_URL}api/Ultimatewarriors/weapons`, payload);
            console.log('Weapon created:', response.data);
        } catch (error) {
            console.error('Error creating weapon:', error);
        }
    };

    return (
        <div>
            <h2>Create Weapon</h2>
            <form onSubmit={handleSubmit(onSubmit)}>
                <div>
                    <label>Name:</label>
                    <input
                        type="text"
                        {...register('Name', { required: 'Name is required' })}
                    />
                    {errors.Name && <p>{errors.Name.message}</p>}
                </div>
                <div>
                    <label>Description:</label>
                    <textarea
                        {...register('Description', { required: 'Description is required' })}
                    />
                    {errors.Description && <p>{errors.Description.message}</p>}
                </div>
                <button type="submit">Create Weapon</button>
            </form>
        </div>
    );
};

export default CreateWeapon;