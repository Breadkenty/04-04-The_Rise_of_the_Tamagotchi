# Tamagotchi API

**_Welcome to Tamagotchi API!_**

This API is used to manage a pet database by the following properties:

**Pet**

- (Int) Id
- (String) Name
- (DateTime) Birthday
- (Int) Hunger Level
- (Int) Happiness Level
- (Int) Last Interacted
- (Bool) Status (Dead or Alive)

**API Link**
https://kento-tamagotchi.herokuapp.com/

**EndPoints**

_GET_
`/Pets` Fetches all pets in the database
`/Pets_alive` Fetches all pets that are alive in the database
`/Pets/{id}` Fetches a pet by their ID

_DELETE_
`/Pets/{id}` Delete a pet from the database by its ID

_POST_
`/Create_new_pet` Create a new pet to add to the database
`​/Pets​/{id}​/Play` Play with a pet selected its ID. Increases happiness by 5 points and hunger by 3 points.
`​/Pets​/{id}​/Feed` Feed a pet selected by its ID. Increases happiness by 3 points and decreases hunger by 5 points.
`​/Pets​/{id}​/Scold` Scold a pet selected by its ID. Decreases happiness by 5 points.

_Give it a try!_
