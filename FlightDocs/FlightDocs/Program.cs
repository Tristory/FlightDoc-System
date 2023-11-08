using FlightDocs.Data;
using FlightDocs.Swagger;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Swashbuckle.AspNetCore.SwaggerGen;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

// Add Db Context services
builder.Services.AddDbContext<ApplicationDbContext>( x => x.UseSqlServer(builder.Configuration.GetConnectionString("DbCon")));

// Add services to the container.
builder.Services.AddControllers();

// Add authentication and authorization service
builder.Services.AddAuthentication(x =>
{
    x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
    x.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
}).AddJwtBearer(x =>
{
    x.TokenValidationParameters = new TokenValidationParameters
    {
        ValidIssuer = "Issuer",
        ValidAudience = "Audience",
        ValidateIssuer = true,
        ValidateAudience = true,
        ValidateLifetime = true,
        ValidateIssuerSigningKey = true,
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("The Super Secret"))
    };
});

builder.Services.AddAuthorization(options =>
{
    options.AddPolicy("Admin Only", p => p.RequireClaim("Role", "admin"));        
    options.AddPolicy("For Name", p => p.RequireClaim("Name", "Star Platinum"));

    //options.AddPolicy("Someone and Admin", p => p.RequireClaim("Role", "other than admin").RequireClaim("Role", "admin"));
    options.AddPolicy("Staff and Admin", p =>
    {
        p.RequireAssertion(context =>
            context.User.HasClaim(c => c.Type == "Role" && c.Value == "staff GO") ||
            context.User.HasClaim(c => c.Type == "Role" && c.Value == "admin"));
    });

    options.AddPolicy("Fly Attendant and Admin", p =>
    {
        p.RequireAssertion(context =>
            context.User.HasClaim(c => c.Type == "Role" && c.Value == "crew") ||
            context.User.HasClaim(c => c.Type == "Role" && c.Value == "pilot") ||
            context.User.HasClaim(c => c.Type == "Role" && c.Value == "admin"));
    });
});

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddTransient<IConfigureOptions<SwaggerGenOptions>, SwaggerConfigureOption>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
