using MinimalJwt.Models;
using MinimalJwt.Reposotories;

namespace MinimalJwt.Services;

public class MovieService : IMovieService
{
    public Movie Create(Movie movie)
    {
        movie.Id = MovieReposository.Movies.Count + 1;  // make a id for movie in repo 
        MovieReposository.Movies.Add(movie);
        return movie;

    }

    public Movie Get(int id)
    {
        var movie = MovieReposository.Movies.FirstOrDefault(o => o.Id == id);
        if (movie is null) return null;
        return movie;
    }

    public List<Movie> List()
    {
        var movies = MovieReposository.Movies;
        return movies;
    }

    public Movie Update(Movie newMovie)
    {
        var oldMovie = MovieReposository.Movies.FirstOrDefault(o => o.Id == newMovie.Id);
        if (oldMovie is null) return null;
        oldMovie.Title = newMovie.Title;
        oldMovie.Description = newMovie.Description;
        oldMovie.Rating = newMovie.Rating;
        return newMovie;
    }

    public bool Delete(int id)
    {
        var oldMovie = MovieReposository.Movies.FirstOrDefault(o => o.Id == id);
        if (oldMovie is null) return false;

        MovieReposository.Movies.Remove(oldMovie);
        return true;

    }




}