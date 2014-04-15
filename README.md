MVC5-tutorial
=============
This project is based on Microsoft&trade; tutorial <a href="http://www.asp.net/mvc/tutorials/mvc-5/introduction/getting-started" >See here »</a>
It is created in MVC 5.1 with EF 6.1 (.NET 4.5)

It uses 2 Database Context : one for Authentication (Users, Roles, etc), second for Application Data( Movies etc). 
Both have migrations, and set Datatabase Initialization to latest migration(creates 3 roles, 1 user "admin" and 4 movies)



Project have useful examples of :
-Authentication (standard, by Google, facebooc etc)
-Authorization by roles (Controlers, Actions, render view parts if user is in role)
-Razor Html extension method
-Linq extension method
-EF in use
-Database migrations
-localizable Resources and show how to use them in MVC
-Ajax example
