# RealEase
RealEase is a comprehensive real estate management platform that simplifies the listing of properties, scheduling of visits, rental contract administration, and rent payment processing.

## Features

### Property Listings: Allows real estate agents to list properties with detailed descriptions and images.

### Visit Scheduling: Efficiently manages property visit appointments.

### Contract Management: Generates and stores rental contracts securely.

### Rent Payments: Manages rental payments and keeps track of all transactions.

## Tech Stack

### Language: C#

### Frontend: HTML, CSS, JavaScript

### Backend: ASP.NET Core

### Database: SQL Server

## Installation and Usage

1- Clone the repository:
  git clone https://github.com/1MarioSR/RealEase.git
2- Open the solution in Visual Studio.  
3 - Restore the NuGet packages.
4 - Configure the database connection string in appsettings.json.
5 - dotnet ef database update
6 - dotnet run

## API Documentation (Preview)

The API follows RESTful principles and uses DTOs (Data Transfer Objects) for communication.

## Endpoints

GET /api/properties
Returns a list of all properties.

GET /api/properties/{id}
Returns the details of a specific property.

POST /api/properties
Creates a new property.

PUT /api/properties/{id}
Updates a property listing.

DELETE /api/properties/{id}
Deletes a property.

## Contributing

Contributions are welcome! If you’d like to help improve RealEase, please:

1 - Fork the repo.

2 - Create a new branch.

3 - Make your changes and test them.

4 - Open a pull request with a clear explanation.

## Screenshot

![image](https://github.com/user-attachments/assets/b6cca112-6692-4791-bd51-164ddba78f77)

![image](https://github.com/user-attachments/assets/20a6efb6-548b-49b1-aed7-4c9b2a3ae867)

![image](https://github.com/user-attachments/assets/bb7b9e32-8792-46c0-871a-eb4c64d0a2ce)

![image](https://github.com/user-attachments/assets/eb578e4e-3081-4cb1-a679-39932758f2c4)

![image](https://github.com/user-attachments/assets/d525c54e-533b-4979-8790-03c48733694f)

![image](https://github.com/user-attachments/assets/3f5e3129-2018-40d2-992d-a5df205018ca)


## License
This project is licensed under the MIT License. See the LICENSE file for details.

## Updates may be made in the future.
