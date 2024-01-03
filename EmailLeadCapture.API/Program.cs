using EmailLeadCapture.API;
using EmailLeadCapture.API.EFData;
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
builder.Services.AddRazorPages();
builder.Services.Configure<ConnectionStrings>(builder.Configuration.GetSection("ConnectionStrings"));
builder.Services.AddDbContext<LeadCaptureDbContext>();
builder.Services.AddSingleton<LeadCaptureDatabase>();

var app = builder.Build();

app.UseAuthentication();
app.UseAuthorization();

app.MapGet("/", () => "AO Lead Capture API");
MapApiEndpoints(app);
app.MapRazorPages();

app.Run();
