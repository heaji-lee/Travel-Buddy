using Microsoft.AspNetCore.Authentication;
using Microsoft.EntityFrameworkCore;
using TravelBuddy.Data;
using TravelBuddy.Security;
using TravelBuddy.Services;
using TravelBuddy.Repositories;
using Supabase;
using DotNetEnv;

string? FindEnvFile() {
  var current = new DirectoryInfo(Directory.GetCurrentDirectory());

  while (current is not null) {
    var candidate = Path.Combine(current.FullName, ".env");
    if (File.Exists(candidate)) {
      return candidate;
    }

    current = current.Parent;
  }

  return null;
}

var envFilePath = FindEnvFile();
if (!string.IsNullOrWhiteSpace(envFilePath)) {
  DotNetEnv.Env.Load(envFilePath);
}

var builder = WebApplication.CreateBuilder(args);

var supabaseUrl = Environment.GetEnvironmentVariable("SUPABASE_URL");
var supabaseKey = Environment.GetEnvironmentVariable("SUPABASE_KEY");
var connectionString = Environment.GetEnvironmentVariable("CONNECTION_STRING");

builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseNpgsql(connectionString, npgsqlOptions => {
      npgsqlOptions.EnableRetryOnFailure(
          maxRetryCount: 3,
          maxRetryDelay: TimeSpan.FromSeconds(5),
          errorCodesToAdd: null);

      npgsqlOptions.CommandTimeout(60);
    }));

builder.Services.AddScoped<Supabase.Client>(_ => {
  return new Supabase.Client(supabaseUrl!, supabaseKey!, new SupabaseOptions {
    AutoRefreshToken = true,
    AutoConnectRealtime = true
  });
});

builder.Services.AddHttpClient<AuthService>(client => {
  client.BaseAddress = new Uri(supabaseUrl!);
  client.DefaultRequestHeaders.Add("apikey", supabaseKey!);
  client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", supabaseKey!);
});

builder.Services.AddAuthentication("Supabase")
    .AddScheme<AuthenticationSchemeOptions, SupabaseAuthenticationHandler>("Supabase", _ => { });

builder.Services.AddAuthorization();

builder.Services.AddScoped<TripsRepository>();
builder.Services.AddScoped<TripsService>();
builder.Services.AddScoped<CompanionsRepository>();
builder.Services.AddScoped<CompanionsService>();
builder.Services.AddScoped<InterestsRepository>();
builder.Services.AddScoped<InterestsService>();
builder.Services.AddScoped<TravelStylesRepository>();
builder.Services.AddScoped<TravelStylesService>();
builder.Services.AddScoped<DestinationsRepository>();
builder.Services.AddScoped<DestinationsService>();
builder.Services.AddHttpClient<OpenAiService>();

builder.Services.AddControllers()
    .AddJsonOptions(options => {
      options.JsonSerializerOptions.Converters.Add(
          new System.Text.Json.Serialization.JsonStringEnumConverter());
      options.JsonSerializerOptions.ReferenceHandler = System.Text.Json.Serialization.ReferenceHandler.IgnoreCycles;
    });

builder.Services.AddOpenApi();

builder.Services.AddCors(options => {
  options.AddPolicy("AllowFrontend", policy => {
    policy.WithOrigins("http://localhost:4200")
          .AllowAnyHeader()
          .AllowAnyMethod();
  });
});

var app = builder.Build();

if (app.Environment.IsDevelopment()) {
  app.MapOpenApi();
}

app.UseHttpsRedirection();
app.UseCors("AllowFrontend");
app.UseAuthentication();
app.UseAuthorization();
app.MapControllers();

using var scope = app.Services.CreateScope();
var context = scope.ServiceProvider.GetRequiredService<AppDbContext>();
await DbSeeder.SeedAllAsync(context);

app.Run();
