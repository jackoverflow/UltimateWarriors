# Ultimate Warriors Controller API Documentation

## Overview
The `UltimatewarriorsController` provides endpoints for managing warriors and weapons in the Ultimate Warriors application. It allows clients to perform CRUD operations on warriors and weapons.

## Base URL
```
http://localhost:5108/api/ultimatewarriors
```

## Endpoints

### 1. Get All Warriors
- **Endpoint**: `/warriors`
- **Method**: `GET`
- **Description**: Retrieves a list of all warriors.
- **Response**:
  - **Status Code**: `200 OK`
  - **Body**: An array of warrior objects.
    ```json
    [
        {
            "id": 1,
            "name": "Warrior Name",
            "description": "Warrior Description"
        },
        ...
    ]
    ```

### 2. Get All Weapons
- **Endpoint**: `/weapons`
- **Method**: `GET`
- **Description**: Retrieves a list of all weapons.
- **Response**:
  - **Status Code**: `200 OK`
  - **Body**: An array of weapon objects.
    ```json
    [
        {
            "id": 1,
            "name": "Weapon Name",
            "description": "Weapon Description"
        },
        ...
    ]
    ```

### 3. Create a Warrior
- **Endpoint**: `/`
- **Method**: `POST`
- **Description**: Creates a new warrior.
- **Request Body**:
  - **Content-Type**: `application/json`
  - **Body**:
    ```json
    {
        "name": "Warrior Name",
        "description": "Warrior Description"
    }
    ```
- **Response**:
  - **Status Code**: `201 Created`
  - **Body**: The created warrior object.
    ```json
    {
        "id": 1,
        "name": "Warrior Name",
        "description": "Warrior Description"
    }
    ```

### 4. Create a Weapon
- **Endpoint**: `/weapons`
- **Method**: `POST`
- **Description**: Creates a new weapon.
- **Request Body**:
  - **Content-Type**: `application/json`
  - **Body**:
    ```json
    {
        "name": "Weapon Name",
        "description": "Weapon Description"
    }
    ```
- **Response**:
  - **Status Code**: `201 Created`
  - **Body**: The created weapon object.
    ```json
    {
        "id": 1,
        "name": "Weapon Name",
        "description": "Weapon Description"
    }
    ```

### 5. Associate Warrior with Weapon
- **Endpoint**: `/warrior-weapon`
- **Method**: `POST`
- **Description**: Associates a warrior with a weapon.
- **Request Body**:
  - **Content-Type**: `application/json`
  - **Body**:
    ```json
    {
        "warriorId": 1,
        "weaponId": 1
    }
    ```
- **Response**:
  - **Status Code**: `200 OK`
  - **Body**: Success message.
    ```json
    {
        "message": "Warrior associated with weapon successfully."
    }
    ```

### 6. Get Warrior by ID
- **Endpoint**: `/warriors/{id}`
- **Method**: `GET`
- **Description**: Retrieves a warrior by their ID.
- **Response**:
  - **Status Code**: `200 OK` (if found) or `404 Not Found` (if not found)
  - **Body** (if found):
    ```json
    {
        "id": 1,
        "name": "Warrior Name",
        "description": "Warrior Description"
    }
    ```

### 7. Delete Warrior
- **Endpoint**: `/warriors/{id}`
- **Method**: `DELETE`
- **Description**: Deletes a warrior by their ID.
- **Response**:
  - **Status Code**: `204 No Content` (if successful)

## Error Handling
- **400 Bad Request**: Returned when the request data is invalid.
- **404 Not Found**: Returned when a requested resource cannot be found.

## Notes
- Ensure that the server is running and accessible at the specified base URL.
- Use appropriate headers for content type when making requests.
