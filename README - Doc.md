# Done

# describing decisions

- Implemented commission calculation inside controller for simplicity.
- Used decimal for monetary calculations.
- Added validation for negative values and upper limits.
- Implemented CORS to allow frontend-backend communication.
- At Frontend Added API Call to server

# Trade-offs

- Commission rates are hardcoded.
- Basic UI error handling.
- No authentication implemented.

# Commission Calculator — Documentation

This project implements a full-stack commission calculator using:

- React (Frontend)
- .NET Web API (Backend)

## How to Run

### Backend

1. Navigate to backend folder (api)
2. Run:
   dotnet run
3. Open:
   https://localhost:5000/swagger

### Frontend

1. Navigate to frontend folder (ui)
2. Run:
   npm install
   npm start
3. Open:
   http://localhost:3000

## How to Test

//I tested API with below in swagger, as there were already set up of swagger was present.

Use sample input:
{
"localSalesCount": 10,
"foreignSalesCount": 10,
"averageSaleAmount": 100
}

Expected:

- Avalpha: £550
- Competitor: £95.5

Test negative values to confirm validation.
