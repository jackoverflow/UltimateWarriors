# Create Weapon Component

## Overview

The `CreateWeapon` component is a React functional component that allows users to create a new weapon by submitting a form. It utilizes the `react-hook-form` library for form handling and `axios` for making HTTP requests to the backend API.

## Features

- **Form Handling**: The component uses `react-hook-form` to manage form state and validation.
- **Error Handling**: Displays validation messages for required fields.
- **API Integration**: Sends a POST request to the `/api/weapons` endpoint to create a new weapon.

## Usage

**JavaScript**
```javascript
import CreateWeapon from './CreateWeapon';

function App() {
  return (
    <div>
      <h1>Create a New Weapon</h1>
      <CreateWeapon />
    </div>
  );
}
```

To use the `CreateWeapon` component, simply import it into your desired file and include it in your JSX.

## Props

The `CreateWeapon` component does not accept any props.

## Notes

- Ensure that the backend API is set up to handle the POST request at `/api/weapons`.
- You may want to implement additional features such as form reset or success/error notifications based on your application's requirements.
