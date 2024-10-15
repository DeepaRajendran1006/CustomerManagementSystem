# Customer Management System
This is a self-contained Customer Management System built using **ASP.NET MVC** as frontend and **ASP.NET Core Web API** for the backend. It uses **Entity Framework** to perform CRUD operations (Create, Read, Update, Delete) on customer data.

The backend uses a **SQL Server** database for storing customer information. The solution also implements proper **API validation** and **separation** between the UI and data layers.

## Features

### Customer Management
- **CRUD Operations**: Add, update, delete, and view customer information.

### API Features
- **RESTful API**: Built with ASP.NET Web API to manage customer data.
- **Search and Filter**: Search and filter customers by name, email, and other attributes in Web API.
- **Pagination**: Filter the customer records based on number of records in each page.
- **Sorting**: Can be sorted on Name by Ascending/Descending

### UI and UX
- **Responsive Design**: Uses Bootstrap for a mobile-friendly layout.
- **Dynamic Razor Views**: Renders HTML dynamically with Razor for a better user experience.

### Backend & Architecture
- **Entity Framework**: Code-first approach for database management.
- **Domain-Driven Design**: Ensures a clean separation between business logic and application logic.

### Additional Features
- **Pagination and Sorting**: Allows sorting and paginating customer lists in Web API.
- **Error Handling**: Comprehensive error handling for smoother user experience.

## Technologies Used

- **ASP.NET MVC**: Utilized for the front-end web application, following the Model-View-Controller pattern.
- **Razor Views**: Implemented to dynamically render HTML content on the front-end.
- **ASP.NET Web API**: Provides RESTful endpoints for handling backend operations, allowing communication between the front-end and the database.
- **Entity Framework**: Used for data access, enabling CRUD operations on the database with a code-first approach.
- **Domain-Driven Design (DDD)**: Applied to structure the business logic, ensuring separation of concerns between the domain layer and other components.

## Prerequisites

Before you begin, ensure you have met the following requirements:

- Visual Studio 2019/2022 or later
- .NET Framework 4.7.2 or later
- SQL Server Express
- Chrome / Microsoft Edge browser

## Setup Instructions

### 1. Clone the Repository

Clone this repository to your local machine using:

git clone https://github.com/DeepaRajendran1006/CustomerManagementSystem.git

### 2. Open the solution

Open the solution in Visual Studio

### 3. Database Setup

- Update the connection string in the Web.config of the Web API project to connect to the SQL Server instance:

 <connectionStrings>
    <add name="DefaultConnection" connectionString="Server=<server-name>;Initial Catalog=CustomerDB;Integrated Security=True" providerName="System.Data.SqlClient"/>
  </connectionStrings>

### 4. Run the solution

- Press F5 to run the solution.
- Both the API and WebForms are set as startup projects.
- Browser opens both the API and Customer Information forms
- API can be verified by navigating to https://localhost:7269/api/Customer?pageNumber=1&pageSize=1000
- WebForms loaded the page with Customer Information page to add, edit, or delete customer records

### 5. Testing the API

To manually test the API endpoints, here are the available routes:

- **GET** `api/Customer?pageNumber=1&pageSize=1000`: Retrieves a list of all customers. Can be filtered, sorted and paging.
- **GET** `/api/Customer/{id}`: Retrieves a customer by ID.
- **POST** `/api/Customer`: Adds a new customer.
- **PUT** `/api/Customer/{id}`: Updates an existing customer.
- **DELETE** `/api/Customer/{id}`: Deletes a customer by ID.

### 6. API Validation and Separation

- **Frontend Validation**: Fronend validation for fields like Name, Email and Phone Number has been done using Data Annotations.
- **API Validation**: Server-side validation for customer data in the **Web API** project to ensure valid email format, phone number length, and required fields.

## How to Use the Application

1. Launches the `Customer Management System` page.
2. A table will be displayed with the list of customers.
3. Use the form to **Add**, **Edit**, or **Delete** customers.
4. The changes are reflected in the SQL Server database (if connected).

## Future Improvements

- Can add user authentication for securing the application.
- Can implement pagination in UI in the customer list.
- Can add search functionality for filtering customers in the UI.
- Can enhance the frontend UI with more advanced CSS or JavaScript frameworks.
