using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.EntityFrameworkCore;
using PersonalTrainerAPI.Data;
using PersonalTrainerAPI.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using Microsoft.AspNetCore.Authentication.JwtBearer; // For JwtBearerDefaults
using System.Text;


var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Add CORS policy
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowReactApp",
        policy =>
        {
            policy.WithOrigins("http://localhost:3000")
                  .AllowAnyMethod()
                  .AllowAnyHeader();
        });
});

// Register DbContext with Identity
builder.Services.AddDbContext<BookingContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

// Add Identity services
builder.Services.AddIdentity<ApplicationUser, IdentityRole>()
    .AddEntityFrameworkStores<BookingContext>()
    .AddDefaultTokenProviders();

// Configure JWT Authentication
builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
})
.AddJwtBearer(options =>
{
    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuer = true,
        ValidateAudience = true,
        ValidateLifetime = true,
        ValidateIssuerSigningKey = true,
        ValidIssuer = builder.Configuration["Jwt:Issuer"],
        ValidAudience = builder.Configuration["Jwt:Audience"],
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["Jwt:Key"]))
    };
});

builder.Services.AddAuthorization(); // Add authorization services
builder.Services.AddControllers(); 

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

// Enable CORS for your specified policy
app.UseCors("AllowReactApp");

app.UseHttpsRedirection();


// Configure the HTTP request pipeline.
app.UseAuthentication(); // Enable authentication
app.UseAuthorization(); // Enable authorization

app.MapControllers(); 

var services = new[]
{
    new { Id = 1, Name = "Personal Training", Price = 50 },
    new { Id = 2, Name = "Nutrition Consultation", Price = 40 },
    new { Id = 3, Name = "Online Coaching", Price = 30 }
};

app.MapGet("/services", () => services)
   .WithName("GetServices");

// Get Bookings
app.MapGet("/admin/bookings", async (BookingContext context) =>
{
    var bookings = await context.Bookings.ToListAsync();
    return Results.Ok(bookings);
})
.RequireAuthorization() // Require authentication
.WithName("GetAllBookings");

// Post Bookings
app.MapPost("/bookings", async (BookingContext context, Booking booking) =>
{
    // Server-side validation
    if (string.IsNullOrWhiteSpace(booking.Name) || string.IsNullOrWhiteSpace(booking.Email))
    {
        return Results.BadRequest(new { message = "Name and Email are required." });
    }
    
    context.Bookings.Add(booking);

    try
    {
        await context.SaveChangesAsync();
        return Results.Ok(new { message = "Booking received", booking });
    }
    catch (Exception ex)
    {
        return Results.Problem(
            detail: ex.Message,
            title: "An error occurred while saving the booking.",
            statusCode: 500
        );
    }
})
.WithName("CreateBooking");

app.MapGet("/bookings/{id}", async (BookingContext context, int id) => 
{
    var bookings = await context.Bookings.Where(b => b.Id == id).ToListAsync();
    return Results.Ok(bookings);
})
.WithName("GetUserBookings");

app.Run();