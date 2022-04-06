# 🧾 Project Description 
**Library Managment** is a web application that serves as online library where you can browse across multiple books, authors and publish house as well as book a loan for an available book.
The project contains both API and MVC project 

## Homepage
![Homepage](Library-Mangagment/tree/main/img/homepage.png)

## Installation

1. Clone the repository locally;

2. Open the source code using Library.sln through Visual Studio;

3. For API: Open StartUp.cs in Library.API project and configure your local SQL Server connection string;

4. For MVC: Open StartUp.cs in Library.Web project and configure your local SQL Server connection string;

5. Go to Tools -> NuGet Package Manager -> Package Manager Console;

6. Add Initial migration(if not available already) with the following command:
```bash
  Add-Migration Initial
```

7. Update the database with the following command:
```bash
  Update-Database
```

8. Run the project locally (CTRL + F5) and enjoy using it on [the following address](https://localhost:5001/). :)    
## Database relations

![Database-relations](img/db.jpg)

```
## Main functionality
Public part:
* Login/Register
* Contact form
* Browse available books
* About us
Private part:
* Personal loans on books with their status
* Profile with picture and options to change your personal data
* Books details
* Loan request
Administrative part:
* List of users
* User details
* Ban/Unban user
* List of books
* Create/Update/Delete/View Book
* Create/Update/Delete/View Author
* Create/Update/Delete/View PublishHouse
* Upload pictures on books/authors/publish houses using Cloudinary

## Built with:
* ASP.NET Core
* MsSQL Server
* Entity Framework
* Swagger
* Mail Service
* Auto unban background service
* JWT for API / Cookies for WEB
* Sensitive data BCrypt
* ImageKitAPI
* Cloudinary
* Moq testing
* Javascript/jQuery
* HTML5/CSS/Bootstrap/
