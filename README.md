# Restaurant Reservation System  

## Introduction
 This project is a backend system developed to manage restaurant reservations, table availability, customer information, and menu items. The system provides a RESTful API to facilitate efficient management of restaurant resources, including tables, bookings, customers, and menus.


## Features
-  **CRUD Operations**: Enables Create, Read, Update, and Delete operations for managing reservations, tables, customers, and menu items.
-  **Table Availability Checking**: Allows users to check available tables based on specific dates and times to manage reservations effectively.
-  **Code-First Database Creation**:  The database schema is defined and generated directly from the application's code using a code-first approach with Entity Framework Core.
-  **Error Handling**: Implements robust error handling to manage exceptions and provide informative responses for various error scenarios.
-  **Performance Optimization**: Asynchronous methods are implemented to handle multiple simultaneous users and improve responsiveness.

  ## Installation
1. **Clone the repository:**
   ```bash
   git clone https://github.com/ParmisMorshedi/Restaurant.git
   ```

 2.  **Navigate to the project directory**:
   ```bash
    cd Restaurant 
   ```
 3.  **Restore dependencies:**:
   ```bash
    dotnet restore 
   ```
 4.  **Run the application:**:
   ```bash
    dotnet run
   ```
## NuGet Packages

The project uses the following NuGet packages:

* **Microsoft.EntityFrameworkCore**: ORM for database interactions.
* **Microsoft.EntityFrameworkCore.SqlServer**: SQL Server provider for Entity Framework Core.
* **Microsoft.EntityFrameworkCore.Tools**: Tools for Entity Framework Core, used for database migrations and scaffolding.
* **Microsoft.EntityFrameworkCore.Design**: Design-time tools for Entity Framework Core, used for migrations and database updates. 



## Usage  


- API Documentation: Access the Swagger documentation at https://localhost:7071/swagger/index.html.

- API  Endpoints:
### Customer
- **GET /api/Customer**
  - **Response:**
    ```Json
    [
      {
        "customerId": 1,
        "name": "Parmis",
        "email": "Parmis@gmail.com",
        "phoneNumber": "076899999"
      },
      {
        "customerId": 2, 
        "name": "Adolf",
        "email": "Adolf@gmail.com",
        "phoneNumber": "23456778"
      }
    ]
    ```
  - **Status:** `200 OK`


- **POST /api/Customer**
  - **Request Body:**
    ```json
    {
      "customerId": 0,
      "name": "ALdor",
      "email": "Aldor@gmail.com",
      "phoneNumber": "106580087"
    }
    ```
  - **Status:** `201 Created`

- **GET /api/Customer/{id}**
  - **Request Parameter:** `id (required): 1`
  - **Response:**
    ```json
    {
      "customerId": 1,
      "name": "Parmis",
      "email": "Parmis@gmail.com",
      "phoneNumber": "076899999"
    }
    ```
  - **Status:** `200 OK`

- **PUT /api/Customer/{id}**
  - **Request Parameter:** `id (required)`
  - **Status:** `200 OK`

- **DELETE /api/Customer/{id}**
  - **Request Parameter:** `id (required)`
  - **Status:** `200 OK`

### Menu

- **GET /api/Menu**
  - **Response:**
    ```json
    [
      {
        "menuId": 1,
        "dishName": "Margherita Pizza",
        "price": 95,
        "isAvailable": true
      },
      {
        "menuId": 2,
        "dishName": "Carbonara",
        "price": 110,
        "isAvailable": true
      }
    ]
    ```
  - **Status:** `200 OK`

- **POST /api/Menu**
  - **Request Body:**
    ```json
    {
      "menuId": 3,
      "dishName": "Tomato Soup",
      "price": 70,
      "isAvailable": false
    }
    ```
  - **Status:** `201 Created`

- **GET /api/Menu/{id}**
  - **Request Parameter:** `id (required)`
  - **Response:**
    ```json
    {
      "menuId": 3,
      "dishName": "Tomato Soup",
      "price": 70,
      "isAvailable": false
    }
    ```
  - **Status:** `200 OK`

- **PUT /api/Menu/{id}**
  - **Request Parameter:** `id (required): 2`
  - **Request Body:**
    ```json
    {
      "menuId": 2,
      "dishName": "Carbonara",
      "price": 250,
      "isAvailable": true
    }
    ```
  - **Status:** `200 OK`

- **DELETE /api/Menu/{id}**
  - **Request Parameter:** `id (required)`
  - **Status:** `200 OK`

### Reservation

- **GET /api/Reservation**
  - **Status:** `200 OK`

- **GET /api/Reservation/{id}**
  - **Request Parameter:** `id (required): 4`
  - **Response:**
    ```json
    {
      "reservationId": 4,
      "tableId": 2,
      "customerId": 1,
      "time": "12:00:00",
      "date": "2024-09-01T00:00:00",
      "numberOfGuests": 2
    }
    ```
  - **Status:** `200 OK`

- **PUT /api/Reservation/{id}**
  - **Request Parameter:** `id (required)`
  - **Status:** `200 OK`

- **DELETE /api/Reservation/{id}**
  - **Request Parameter:** `id (required)`
  - **Status:** `200 OK`

- **POST /api/Reservation/AddReservation**
  - **Status:** `201 Created`

- **GET /api/Reservation/by-date**
  - **Request Parameters:** `date: 2024-09-01, time: 12:00:00`
  - **Response:**
    ```json
    [
      {
        "reservationId": 4,
        "tableId": 2,
        "customerId": 1,
        "time": "12:00:00",
        "date": "2024-09-01T00:00:00",
        "numberOfGuests": 2
      }
    ]
    ```
  - **Status:** `200 OK`

### Table

- **GET /api/Table**
  - **Response:**
    ```json
    [
      {
        "tableId": 2,
        "number": 2,
        "seats": 3
      },
      {
        "tableId": 3,
        "number": 2,
        "seats": 3
      },
      {
        "tableId": 4,
        "number": 4,
        "seats": 5
      },
      {
        "tableId": 5,
        "number": 3,
        "seats": 4
      },
      {
        "tableId": 20,
        "number": 1,
        "seats": 2
      },
      {
        "tableId": 21,
        "number": 1,
        "seats": 2
      }
    ]
    ```
  - **Status:** `200 OK`

- **POST /api/Table**
  - **Example of Invalid Input:**
    ```json
    {
      "tableId": 0,
      "number": 15,
      "seats": 200
    }
    ```
  - **Response:** 
    ```json
    {
      "error": "Seats must be between 1 and 10."
    }
    ```
  - **Status:** `400 Bad Request`

- **GET /api/Table/{id}**
  - **Request Parameter:** `id (required)`
  - **Status:** `200 OK`

- **PUT /api/Table/{id}**
  - **Request Parameter:** `id (required)`
  - **Status:** `200 OK`

- **DELETE /api/Table/{id}**
  - **Request Parameter:** `id (required)`
  - **Status:** `200 OK`

- **GET /api/Table/available**
  - **Request Parameters:** `date: 2024-09-01, time: 12:00:00`
  - **Response:**
    ```json
    [
      {
        "tableId": 3,
        "number": 2,
        "seats": 3
      },
      {
        "tableId": 4,
        "number": 4,
        "seats": 5
      },
      {
        "tableId": 5,
        "number": 3,
        "seats": 4
      },
      {
        "tableId": 20,
        "number": 1,
        "seats": 2
      },
      {
        "tableId": 21,
        "number": 1,
        "seats": 2
      }
    ]
    ```
  - **Status:** `200 OK`
 
## Architecture

-  **Controllers**: Manage HTTP requests and responses, routing requests to the appropriate services.
-  **Services**: Contain the business logic for managing tables, customers, reservations, and menus.
-  **Repositories**: Handle database interactions, utilizing Entity Framework Core to perform CRUD operations.
-  **Error Handling**: Implements robust error handling to manage exceptions and provide informative responses for various error scenarios.
-  **Database**: A code-first approach is used to define the database schema, with migrations applied to create the necessary tables and relationships.

##  Technologies Used
- ASP.NET Core 8.0
- Entity Framework Core
- SQL Server
- Swagger for API documentation
- Insomnia for API testing

  
##  Testing
- The API was tested using Insomnia and Swagger ensuring each endpoint functions as expected. 
-  API responses were verified against expected outcomes, with error handling tested by simulating invalid inputs.

  
