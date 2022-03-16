using NUnit.Framework;
using Domain.Models;
using Infrastructure.Repositories;
using System.Collections.Generic;

namespace Tests
{
    [TestFixture]
    public class MoviesRepositoryTests
    {
        readonly Movies movie = new();

        [SetUp]
        public void Init() {
            
            movie.Name = "Matrix";
            movie.FilmStudio = "Warner Bros";
            movie.ReleaseDate = System.DateTime.Now;
            movie.BoxOffice = 155.0;
        }

        [Test]
        public void AddNewMovie()
        {
            MoviesRepository.Create(movie);
            Assert.That(MoviesRepository.GetById(movie.Id) == movie);
        }

        [Test]
        public void FindMovieBySearch()
        {
            MoviesRepository.Create(movie);
            List<Movies> movies = new();
            movies.Add(movie);
            Assert.That(MoviesRepository.GetBySearch(movie.Name), Is.EquivalentTo(movies));
        }

        [Test]
        public void FindMovieById() {
            MoviesRepository.Create(movie);
            Assert.AreSame(MoviesRepository.GetById(movie.Id), movie);
        }
    }
}