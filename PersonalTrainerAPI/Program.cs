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

var services = new[]
{
    new { Id = 1, Name = "Personal Training", Price = 50 },
    new { Id = 2, Name = "Nutrition Consultation", Price = 40 },
    new { Id = 3, Name = "Online Coaching", Price = 30 }
};

app.MapGet("/services", () => services)
   .WithName("GetServices");

app.Run();