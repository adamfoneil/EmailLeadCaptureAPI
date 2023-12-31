using EmailLeadCapture.API;
using HashidsNet;

var builder = WebApplication.CreateBuilder(args);

builder.Configuration.AddJsonFile("appsettings.Secret.json", optional: true, reloadOnChange: true);

builder.Services.AddScoped((sp) => new Hashids(builder.Configuration["HashIds:Salt"], int.Parse(builder.Configuration["HashIds:MinLength"]!)));

var allowedKeys = builder.Configuration.GetSection("AllowedKeys").Get<string[]>();

builder.Services.AddAuthentication("AllowedKeys")
	.AddScheme<AllowedKeyOptions, AllowedKeyAuthHandler>("AllowedKeys", options =>
	{
		options.Values = allowedKeys ?? throw new Exception("missing AllowedKeys configuration");
	});

builder.Services.AddAuthorization();

var app = builder.Build();
app.UseAuthentication();
app.UseAuthorization();

app.MapGet("/", () => "AO Lead Capture API");

var apiRoutes = app.MapGroup("/api").RequireAuthorization();

apiRoutes.MapGet("/encode", (int number, Hashids hashIds) =>
{
	return new { value = hashIds.Encode(number) };
});

app.Run();
