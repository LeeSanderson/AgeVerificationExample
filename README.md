# AgeVerificationExample

An example ASP.NET core application using ASP.NET Core Identity for authentication and authorisation.

Features include:

- [Hexagonal/Onion Architecture model AKA Ports and adapters](https://en.wikipedia.org/wiki/Hexagonal_architecture_(software)
- Using [Domain driven design](https://en.wikipedia.org/wiki/Domain-driven_design) bounded contexts.
- Customised ASP.NET Core Identity models.
- Use of [Authorize] attributes to prevent access to actions that require an authenticated user. 
- A mix of standard MVC controllers/action/views and Razor pages.
- libman.json for management of client-side libraries.
- Testing of all layers of the application.
- Multi-stage registration process using TempData to transfer model data between Razor pages. 
- A reusable, template driven tag helper for pagination of data sets.
- An example of using Chart.js to display chart data.
- Uses patternomaly to help those with colour blindness distinguish between areas on charts.
- Simple, readable, fluent tests.
- Uses a combination of mocking and EF in memory database for testing components with data access requirements. 