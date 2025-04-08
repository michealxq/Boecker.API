using Boecker.API.Extentions;
using Boecker.API.Middlewares;
using Boecker.Application.Extensions;
using Boecker.Infrastructure.Extensions;
using Boecker.Infrastructure.persistence;
using Boecker.Infrastructure.Seeding;
using QuestPDF.Infrastructure;
using Serilog;

var builder = WebApplication.CreateBuilder(args);

QuestPDF.Settings.License = LicenseType.Community;

builder.AddPresentation();
builder.Services.AddApplication();
builder.Services.AddInfrastructure(builder.Configuration);

var allowedOrigins = "_allowedOrigins";

builder.Services.AddCors(options =>
{
    options.AddPolicy(name: allowedOrigins,
        policy =>
        {
            policy
                .WithOrigins("http://localhost:4200")
                .AllowAnyHeader()
                .AllowAnyMethod();
        });
});

var app = builder.Build();


var scope = app.Services.CreateScope();

var seeder = scope.ServiceProvider.GetRequiredService<IDatabaseSeeder>();
await seeder.Seed();

var services = scope.ServiceProvider;
await IdentitySeeder.SeedAsync(services);

app.UseMiddleware<ErrorHandlingMiddleware>();
app.UseMiddleware<RequestTimeLoggingMiddleware>();

app.UseSerilogRequestLogging();

app.UseAuthentication();
app.UseAuthorization();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseCors(allowedOrigins);

app.MapControllers();

await app.RunAsync();
