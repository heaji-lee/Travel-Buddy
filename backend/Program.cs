using Microsoft.EntityFrameworkCore;
using TravelBuddy.Data;
using TravelBuddy.Services;
using TravelBuddy.Repositories;
using Supabase;
using DotNetEnv;

var builder = WebApplication.CreateBuilder(args);

DotNetEnv.Env.Load();
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

builder.Services.AddScoped<TripsRepository>();
builder.Services.AddScoped<TripsService>();
builder.Services.AddScoped<CompanionsRepository>();
builder.Services.AddScoped<CompanionsService>();
builder.Services.AddScoped<InterestsRepository>();
builder.Services.AddScoped<InterestsService>();
builder.Services.AddScoped<TravelStylesRepository>();
builder.Services.AddScoped<TravelStylesService>();

builder.Services.AddControllers()
    .AddNewtonsoftJson();

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
app.UseAuthorization();
app.MapControllers();

app.Run();