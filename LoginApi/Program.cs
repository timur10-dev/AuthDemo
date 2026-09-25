using LoginApi.Data;
using LoginApi.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlServer(
        builder.Configuration.GetConnectionString("DefaultConnection")
    ));

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowFrontend", policy =>
    {
        policy.AllowAnyOrigin()
              .AllowAnyHeader()
              .AllowAnyMethod();
    });
});

var app = builder.Build();

using (var scope = app.Services.CreateScope())
{
    var db = scope.ServiceProvider.GetRequiredService<AppDbContext>();
    db.Database.Migrate();
}

app.UseCors("AllowFrontend");

app.UseSwagger();
app.UseSwaggerUI();


app.UseHttpsRedirection();

app.UseDefaultFiles();
app.UseStaticFiles();

IPasswordHasher<User> passwordHasher = new PasswordHasher<User>();

//redirect http://localhost:5280/ to login page
app.MapGet("/", () => Results.Redirect("/LoginFront/login.html"));

//log in
app.MapPost("/login", async (User user, AppDbContext db) =>
{
    var requestedUser = await db.Users.FirstOrDefaultAsync(u => u.Username == user.Username);
    if (requestedUser == null)
    {
        return Results.BadRequest("Invalid username or password. Please try again.");
    }
    else
    {
        var requestedPassword = requestedUser.Password;
        var verificationResult = passwordHasher.VerifyHashedPassword(requestedUser, requestedPassword, user.Password);

        if (verificationResult == PasswordVerificationResult.Failed)
        {
            return Results.BadRequest("Invalid username or password. Please try again.");
        }
        else
        {
            return Results.Ok(requestedUser);
        }
    }

});

//register
app.MapPost("/register", async (User user, AppDbContext db) =>
{
    var exists = await db.Users.AnyAsync(u => u.Username == user.Username);

    if (exists)
    {
        return Results.BadRequest("Username already exists.");
    }
    else
    {
        var hashedPassword = passwordHasher.HashPassword(user, user.Password);
        user.Password = hashedPassword;

        db.Users.Add(user);
        await db.SaveChangesAsync();

        return Results.Ok("User registered successfully.");
    }

});

app.Run();