This is a learning project from me that brings together several things:

- a new approach to hosting, using Docker, [DigitalOcean](https://www.digitalocean.com/) and [DotNetEngine](https://dotnetengine.com/). I'm not super happy with Azure, so I'm experimenting with this combo of services as an alternative
- minimal API development in .NET8. There are neat new debugging capabilities for APIs you can learn about in this [@JamesMontemagno video](https://www.youtube.com/watch?v=uv_jK44WdLg)
- Postgres using my [Dapper.Entities.PostgreSql](https://www.nuget.org/packages/Dapper.Entities.PostgreSql/) project, because new ORM projects always make sense, right?
- more EF migration practice, because I don't have [Ensync](https://github.com/adamfoneil/Ensync) working for Postgres. EF does not bring me joy, but it is the path of least resistance for code-first entity development

This project is in a very rough state and not intended as a general-purpose solution, but rather is a personal project I wanted to share.
