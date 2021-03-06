using System.Data;
using System.Dynamic;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Mvc;
using MinimalJwt.Models;
using MinimalJwt.Services;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddSwaggerGen();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSingleton<IMovieService, MovieService>();
builder.Services.AddSingleton<IUserService, UserService>();

var app = builder.Build();
app.UseSwagger();


app.MapGet("/", () => "Hello World!");

app.MapPost("/create",
    (Movie movie, IMovieService service) => Create(movie, service));

app.MapGet("/get",
    (int id, IMovieService service) => Get(id, service));

app.MapGet("/list",
    (IMovieService service) => List(service));

app.MapPut("/update",
    (Movie newMovie, IMovieService service) => Update(newMovie, service));


app.MapDelete("/delete",
    (int id, IMovieService service) => Delete(id, service));


IResult Create(Movie movie, IMovieService service)
{
    var result = service.Create(movie);
    return Results.Ok(result);
}

IResult Get (int id, IMovieService service)
{
    var result = service.Get(id);
    if (result is null) 
    {
        return Results.NotFound("Movie not foud");
    }
    return Results.Ok(result);
}

IResult List(IMovieService service)
{
    var movies = service.List();
    return Results.Ok(movies);
}

IResult Update(Movie newMovie, IMovieService service)
{
    var movies = service.Update(newMovie);
    if (movies is null) Results.NotFound("movie not found");
    
    return Results.Ok(movies);
}

IResult Delete(int id, IMovieService service)
{
    var result = service.Delete(id);
    if (!result) Results.BadRequest("something went wrong");

    return Results.Ok(result);
}

app.UseSwaggerUI();
app.Run();

