import React from 'react';
import { useForm } from 'react-hook-form';
import axios from 'axios';
import Swal from 'sweetalert2';

const CreateWarrior = () => {
    const { register, handleSubmit, formState: { errors }, reset } = useForm();

    const onSubmit = async (data) => {
        const payload = {
            Name: data.Name,
            Description: data.Description
        };

        // Debugging line to check the API URL
        console.log('API URL:', process.env.REACT_APP_API_URL); 

        try {
            const response = await axios.post('http://localhost:5108/api/ultimatewarriors', payload);
            console.log('Warrior created:', response.data);
            
            // Success message
            Swal.fire({
                title: 'Success!',
                text: 'Warrior created successfully',
                icon: 'success',
                confirmButtonColor: '#3085d6'
            });
            
            // Reset form after successful submission
            reset();
            
        } catch (error) {
            console.error('Error creating warrior:', error);
            
            // Error message
            Swal.fire({
                title: 'Error!',
                text: 'Failed to create warrior. Please try again.',
                icon: 'error',
                confirmButtonColor: '#d33'
            });
        }
    };

    return (
        <div>
            <h2>Create Warrior</h2>
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
                <button type="submit">Create Warrior</button>
            </form>
        </div>
    );
};

export default CreateWarrior;