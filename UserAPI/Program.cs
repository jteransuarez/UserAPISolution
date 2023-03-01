using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using UserAPI.Core.Application.IRepositories;
using UserAPI.Core.Domain.Model;
using UserAPI.DTOs;
using UserAPI.Infrastructure.DataContext;
using UserAPI.Infrastructure.Repositories;


var MyAllowSpecificOrigins = "_myAllowSpecificOrigins";
var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<UserAPIDataContext>(x => x.UseInMemoryDatabase("UserTestDB"));
builder.Services.AddScoped<IUserRepository, UserRepository>();

builder.Services.AddCors(options =>
{
    options.AddPolicy(MyAllowSpecificOrigins,
                          policy =>
                          {
                              policy.AllowAnyOrigin().AllowAnyHeader().AllowAnyMethod();
                          });
});

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseCors(MyAllowSpecificOrigins);


app.MapGet("api/users", (IUserRepository service) => {
    return service.GetAll();
});

app.MapGet("api/user", async (IUserRepository service, string uid) => {
    return await service.GetByIdAsync(uid);
});

app.MapPost("api/delete",  (IUserRepository service, string uid) => {

    User objUser = service.GetByIdAsync(uid).Result;
    service.Delete(objUser);
});

app.MapPost("api/edit",  (IUserRepository service, UserDTO user) =>
{
    User objUser = null;
    if (string.IsNullOrEmpty(user.Uid))
    {
        objUser = new User()
        {
            Username = user.Username,
            FirstName = user.FirstName,
            LastName = user.LastName,
            Email = user.Email
        };

        service.AddAsync(objUser);
    }
    else
    {
        objUser =  service.GetByIdAsync(user.Uid).Result;
        objUser.Username = user.Username;
        objUser.FirstName = user.FirstName;
        objUser.LastName = user.LastName;
        objUser.Email = user.Email;

        service.Update(objUser);
    }

    return ;

});

app.Run();
