[Slava / GitHub Skig1]
Junior C# / ASP.NET Core Developer
This project is based on the **[Editorial]** template from the [HTML5UP](https://html5up.net/) website.

## Original Template License
- **Author:** ["HTML5UP"]
- **License:** Creative Commons Attribution 3.0 Unported (CC BY 3.0)
- **Template Link:** https://html5up.net/Editorial
- **License Terms:** https://creativecommons.org/licenses/by/3.0/

## Changes
- Completely redesigned the HTML structure.
- Added new components: Admin Panel, About Us Panel, Recommendation List, Food List and their individual views, and Shopping Cart.
- Style changes: Colors and fonts, as well as new ones for new features.
- New JavaScript logic has been implemented: for changing the number of dishes in the basket without redemption, as well as calculating the number of dishes and drinks per person and selecting recommended dishes in the admin panel.
SiteCatering
A tutorial ASP.NET Core MVC project for a catering website.
The project implements basic online catering functionality: viewing dishes, adding them to the cart, placing an order, and managing the menu.

Main features:
1) Home page
Display a list of recommended dishes
Company information

2) Dishes
View all dishes
Filter dishes by menu category
View detailed dish information

3) Cart
Add dishes to cart
Change the quantity of dishes
Remove dishes from cart
Empty cart
Calculate the total cost
Download cart contents in DOCX format

4) Authorization
User login/logout
Used ASP.NET Identity

5) Administrative panel
(available only to users with the admin role)
View the list of dishes
Add, edit, and delete dishes
Upload dish images
Manage recommended dishes

Technologies used:

1) C#
2) ASP.NET Core MVC
3) Entity Framework Core
4) ASP.NET Identity
5) PostgreSQL
6) Razor Views
7) Bootstrap (for basic layout)
8) Session (for working with the shopping cart)

Project Architecture

The project is divided into several logical layers:

Controllers — handling HTTP requests
Models — models and DTOs
Domain — entities and repository interfaces
Infrastructure — helper classes (e.g., DTO mapping)
Views — Razor views
The Repository pattern and the DataManager class are used for working with data.

Clone the repository:
git clone https://github.com/Skig1/SiteCatering.git
Open the project in Visual Studio
Restore dependencies:
dotnet restore
Run the application:
dotnet run

specify the database connection string in appsettings.Development.json
admin panel .../admin, the default login and password are "admin".
List of drink keywords in the file quality.js, 85.Const drinkKeywords=[.....] (needed to calculate the volume of drinks per person)
