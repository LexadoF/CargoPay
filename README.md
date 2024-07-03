# CargoPay

## How to execute and use
### Apply the migrations 
- To execute the restAPI, you need to apply the migrations by configuring the MySql connection string and using Update-database in the package manager console ![imagen](https://github.com/LexadoF/CargoPay/assets/65349887/79a0cb77-23db-430d-8af0-b091f07491af)

- After that is done, you can execute the api and you will be redirected to the swagger page ![imagen](https://github.com/LexadoF/CargoPay/assets/65349887/f3d72c6e-1a8e-4ef0-9618-4de10e14ce49) in which you can see all the api calls and their respective calling method (GET or POST)

### Login to the api
- To login in the api you must use the route /auth/login which will accept the username and it's password returning a token, if using postman, this token must be sent in the Authorization tab as Bearer token ![imagen](https://github.com/LexadoF/CargoPay/assets/65349887/b9659e36-3712-4c25-83f3-7ede0b5de1e2)
![imagen](https://github.com/LexadoF/CargoPay/assets/65349887/0a14b84a-8011-4558-b7f9-7785d893fad8)
![imagen](https://github.com/LexadoF/CargoPay/assets/65349887/028e52e0-6d0c-457d-abea-002da9f16b33)

- And lastly you can use the route /auth/checkAuth to verify that the token is valid within the system
  ![imagen](https://github.com/LexadoF/CargoPay/assets/65349887/ee18ffa2-b2cd-48b8-956e-db8029e3b686)

--- 

#### After doing the steps above, you can use the routes that appear in the swagger docs to make the api calls, you will need to include the Bearer token or the api will not accept your connection
