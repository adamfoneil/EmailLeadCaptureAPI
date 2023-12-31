using HashidsNet;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddScoped((sp) => new Hashids(builder.Configuration["HashIds:Salt"], int.Parse(builder.Configuration["HashIds:MinLength"]!)));
builder.Services.AddAuthentication().AddBearerToken();
builder.Services.AddAuthorization();

var app = builder.Build();
app.UseAuthentication();
app.UseAuthorization();

app.MapGet("/", () => "Hello World!");

app.MapGet("/encode", (int number, Hashids hashIds) =>
{
	return new { value = hashIds.Encode(number) };
}).RequireAuthorization();

app.Run();
