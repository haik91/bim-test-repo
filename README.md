# WineMaker API

## Overview

This project is an ASP.NET Core Web API for managing a wine collection.


## Setup & Start 

1. Clone the repository:
   ``` git clone https://github.com/haik91/bim-test-repo.git ```


2.  Configure `appsettings.json` to match your local environment settings. This includes adding your username and password.

3.  Execute the following commands to add and apply migrations:

    Add a new migration 
   ``` dotnet ef migrations add InitialCreate  ``` 

    Apply the migration to the database
   ```  dotnet ef database update ``` 