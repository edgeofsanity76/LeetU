![Build](https://github.com/edgeofsanity76/LeetU/actions/workflows/dotnet.yml/badge.svg)

LeetU - A Basic REST API using Entity Framework Core and SQLite

This is an archetypal WebAPI implementation using Entity Framework Core and SQLite. Obviously there are many ways to create an API like this.
This is just one example. It shows a typical layered approach to building an API.

- Swagger UI
- Controllers
- Services and Mappers
- Datalayer and Entities
- Unit Tests

I believe in clean architecture and a clear seperation of concerns. Many have advocated for just using the DbContext and removing the Repositories.
I can see their point, but as the API grows, so does complex data logic which requires testing. The repos exist purely for testability and to allow for extension of the data layer without muddying service or controller layers.

Please see comments in the code for more explanation.

Built in VS 2022

1. Clone Repo
2. Restore Nugets (Right click solution, restore)
3. Build
4. Run

Use the Swagger UI to query the database. The database is a SqlLite database, if you wish to view or edit the data you can download the Sqlite Browser (https://sqlitebrowser.org/)

I will be adding to this project as and when I feel like it.

If you want to talk to me, please contact me on Discord on Duster76#3746

Thanks
