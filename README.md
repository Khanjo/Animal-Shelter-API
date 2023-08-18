# Animal Shelter API
### By _**Joshua Khan**_ 8/18/2023

## Description

API for an animal shelter with full CRUD Functionality with list for cats and dogs at the shelter. Attempted implementing CORS on the and Token based authentication

## Setup/Installation Requirements

  Software Requirements:
  1. Internet browser
  2. A code editor like VSCode or Atom to view or edit the codebase.
  3. MySQL

  Setup:
  1. Click on this [link to the project repository](https://github.com/Khanjo/Animal-Shelter-API.git) on GitHub.   
  2. Click on the "Clone or download" button to copy the project.     
  3. If you know how to use the command line and Github, clone the project with `git clone` then skip to step 5, otherwise use "**Download ZIP**".
  4. Extract the Zip to a folder of your choice and open with a code editor (i.e. vscode)
  5. Use a SQL Manager Database such as mySQL Workbench.
  6. Create an appsettings.json file in **Your Filepath/**`AnimalShelter` and copy/paste this code:  
     {
         "ConnectionStrings": {
             "DefaultConnection": "Server=localhost;Port=3306;database=`Name Your Database`;uid=root;pwd=`Your Password`;"
         }
     }
  7. Navigate to the AnimalShelter directory by entering `cd` **Your Filepath/**`AnimalShelter`. Then enter `dotnet restore`, `dotnet build`, `dotnet ef database update` then `dotnet run` into the terminal.

  ### HTTP Requests

| Request Type  | Route | Parameters |
| :---------- | ---------- | :---------- |
| GET | /api/Cats | name, breed, color, pattern, maxAge |  
| GET | /api/Cats/{id} | |
| POST | /api/Cats | |
| PUT | /api/Cats/{id} | |
| DELETE| /api/Cats/{id} | |
| | | |
| GET | /api/Dogs | name, breed, color, pattern, maxAge |
| GET | /api/Dogs/{id} | |
| POST | /api/Dogs | |
| PUT | /api/Dogs/{id} | |
| DELETE| /api/Dogs/{id} | |

#### Further Exploration:

I attempted to implement CORS for the GET endpoints so people can see what animals are at the shelter from any origin but not on the POST, PUT, or DELETE endpoints to prevent shenanigans with the database.
I Also started working on authentication with tokens, but wasn't sure if it actually works without having an MVC app or how else to check that.

## Support and contact details

_https://github.com/khanjo_

## Technologies Used

* Entity Framework Core
* .NET Core 2.2
* MySQL
* C#
* Visual Studio Code
* GitHub

### License

[MIT License.](https://opensource.org/license/mit/)

Copyright (c) 2023 **_Joshua Khan_**