# Lister

Change connection string in `ListerAPI/appsettings`, then run `update-database`

Tests data are added via sql scripts, draft (example) insert scripts can be found at `Lister.Database/SqlQueries`

Main ASP.NET API project is `ListerAPI`, run API from there

You should register a user through API first - `/api/register - {"nickname": "string", "password": "string" }`

React client is in `lister` directory, `npm start` it from there
