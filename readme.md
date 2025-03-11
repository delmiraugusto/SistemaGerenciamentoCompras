# The Alfa Laval Code Challenge Base

## Basic CRUD
This software follows Basic Crud structure, designed to maintain consistency with similar applications but with their owns particularities.

### API
1. **`/Controllers`** - This layer defines all mapped endpoints, accessible via Swagger:
   - Local Debug: [https://localhost:7000/swagger/](https://localhost:7000/swagger/)
   - Server Side: `{applicationUrl}/api/swagger/`
2. **`/Core`** - Contains core functionalities such as **DB** operations, **Encryption**, **HTTP Services**, and **Global Constants**.
3. **`/Models`** - Data Transfer Objects (DTOs) for each database table.
4. **`/Repositories`** - Handles database communication and operations.
5. **`/Services`** - Provides additional logic where endpoints may access multiple repositories or perform specific actions, returning unified results.

### Database
- **`CodeChallenge.sqlite`** - A small database created for this challenge.
- **`create.sql`** - Script for creating the complete database structure required for the application.
- **`insert&Update.sql`** - Script for insert and update some random lines in the database.

## Database Modeling

```mermaid 
classDiagram
    class User {
        <<Entity>>
        - ID: PK Integer
        - EMAIL: Text
        - PASSWORD: Text
        - NAME: Text
        - ROLEID: Integer
    }

    class Role {
        <<Enum>>
        - ID: PK Integer
        - EMAIL: Text
    }

    class Product {
        <<Entity>>
        - ID: PK Integer
        - NAME: Text
        - PRICE: Decimal
    }

    class Purchase {
        <<Entity>>
        - ID: PK Integer
        - USERID: Integer
        - ORDERDATE: DateTime
        - TOTAL: Decimal
    }

    class PurchaseItem {
        <<Entity>>
        - ID: PK Integer
        - PURCHASEID: Integer
	     - PRODUCTID: Integer	
        - QUANTITY: Integer
    }

  Role --o User
  User --|> Purchase
  Purchase --|> PurchaseItem
  PurchaseItem  --|> Product

```

### Frontend
1. **`/src/app/components`** - Holds all individual components.
2. **`/src/app/core`** - Core functionalities, including **Models**, **Services**, and **Global Constants**.
3. **`/src/app/dialogs`** - Collection of all dialog components.
4. **`/src/app/shared`** - Contains all shared pages. 

# Screens Example
 <img src="./img/MenuAdmin.png" alt="Menu Screen">
  <img src="./img/Swagger.png" alt="Swagger Screen">
 <img src="./img/MakingPurchase.png" alt="Your Purchase Screen">
 <img src="./img/Postman.png" alt="Postaman Screen">
   <img src="./img/PowerBiMaisVendidos.pngg" alt="PowerBI Screen">
 <img src="./img/PurchaseDetails.png" alt="Details Screen">



# How to deploy this project

## On Local Machine:

### Required software:
- [Git](https://git-scm.com/download/win)
- [.NET Core 8.0 SDK](https://dotnet.microsoft.com/pt-br/download/dotnet/8.0)
- [NVM](https://github.com/coreybutler/nvm-windows/releases/tag/1.1.12)

### Getting source code:
- Clone this repository
- Open cmd and execute this code changing the CHANGE-ME to the software Git URL:
```
https://github.com/delmiraugusto/SistemaGerenciamentoCompras.git
```
### Building:

- Run the build.bat
