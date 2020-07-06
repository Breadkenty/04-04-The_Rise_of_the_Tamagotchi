# Tamagotchi API

**_Welcome to Tamagotchi API!_**

This API is used to manage a pet database by the following properties:
Checkout the console app that manages the data in the API:
https://github.com/Breadkenty/04-04-Rise_of_the_Tamagotchi_Console_App

**Pet**

- `(Int)` Id
- `(String)` Name
- `(DateTime)` Birthday
- `(Int)` Hunger Level
- `(Int)` Happiness Level
- `(Int)` Last Interacted
- `(Bool)` Status (Dead or Alive)

**API Link**
https://kento-tamagotchi.herokuapp.com/

**EndPoints**

_GET_

- `/Pets` Fetches all pets in the database
- `/Pets_alive` Fetches all pets that are alive in the database
- `/Pets/{id}` Fetches a pet by their ID

_DELETE_

- `/Pets/{id}` Delete a pet from the database by its ID

_POST_

- `/Create_new_pet` Create a new pet to add to the database
- `​/Pets​/{id}​/Play` Play with a pet selected its ID. Increases happiness by 5 points and hunger by 3 points.
- `​/Pets​/{id}​/Feed` Feed a pet selected by its ID. Increases happiness by 3 points and decreases hunger by 5 points.
- `​/Pets​/{id}​/Scold` Scold a pet selected by its ID. Decreases happiness by 5 points.

_Give it a try!_

---

# Assignment Overview:

To start your journey you will be creating an API that allows a user to create and care for a virtual pet, reminiscent of a [Tamagotchi](https://en.wikipedia.org/wiki/Tamagotchi). The basic functionality will walk you through the four basic parts of a web API, create, read, update and delete.

## Objectives

- Create an API that can implement CRUD features against a Database.
- Practice creating ASP.NET Web API endpoints.
- Practice EF Core.

### Explorer Mode

- Create and new `sdg-api` that has the following features

- Create a database with one table named `Pets`.
  - Should use a POCO called `Pet` with the following columns:
    - Id (int)
    - Name (string)
    - Birthday (DateTime)
    - HungerLevel (int)
    - HappinessLevel (int)

Your API should have the following endpoints

- [x] `GET /pets`, should return all pets in your database.
- [x] `GET /pets/{id}`, should return the pet with the corresponding id.
- [x] `POST /pets`, should create a new pet. The body of the request should contain a JSON object with a key of "name" and a value of the pet's name. The pets `Birthday` should **default** to the current datetime, `HungerLevel` **defaults** to 0 and `HappinessLevel` **defaults** to 0.
- [x] `POST /pets/{id}/playtimes`, should find the pet by id and add 5 to its happiness level and 3 to its hungry level.
- [x] `POST /pets/{id}/feedings`, should find the pet by id and subtract 5 from its hungry level and 3 from its happiness level.
- [x] `POST /pets/{id}/scoldings`, should find the pet by id and subtract 5 from its happiness level.
- [x] `DELETE /pets/{id}`, should delete a pet from the database by Id

### Adventure Mode

Add the following features to your API

- Add columns `LastInteractedWithDate` (DateTime). Every time a pet us updated in the database, set the `LastInteractedWithDate` to the current time. Add a property named `IsDead` to your `Pets` that has logic such that if the `LastInteractedWithDate` is over 3 days old, the property returns `true` otherwise `false`.
- Add a query parameter go `GET /pets` that only returns Pets that are alive.

### Epic mode

Create a console app that interacts with your API that:

- Allows the user to see all pets
- Select a pet to take care of and then play with, scold, or feed the selected pet.
- Create a new pet.
- Delete a pet.

## Resources

- [Web API Tutorial](https://docs.microsoft.com/en-us/aspnet/core/tutorials/first-web-api?view=aspnetcore-3.1)
- [Web API docs](https://dotnet.microsoft.com/apps/aspnets)
