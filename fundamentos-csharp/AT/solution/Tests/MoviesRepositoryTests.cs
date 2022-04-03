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
            movie.ReleaseDate = System.DateTime.Now ;
            movie.BoxOffice = 155.0;
        }

        [Test]
        public void AddNewMovie()
        {
            var operations = MovieRepositoryFactory.Create(RepositoryType.List);
            operations.Create(movie);
            Assert.That(operations.GetById(movie.Id) == movie);
        }

        [Test]
        public void FindMovieBySearch()
        {
            var operations = MovieRepositoryFactory.Create(RepositoryType.List);
            operations.Create(movie);
            List<Movies> movies = new();
            movies.Add(movie);
            Assert.That(operations.GetBySearch(movie.Name), Is.EquivalentTo(movies));
        }

        [Test]
        public void FindMovieById() {
            var operations = MovieRepositoryFactory.Create(RepositoryType.List);
            operations.Create(movie);
            Assert.AreSame(operations.GetById(movie.Id), movie);
        }
    }
}