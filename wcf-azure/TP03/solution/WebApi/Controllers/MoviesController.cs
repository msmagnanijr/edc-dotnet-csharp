using Domain.Model.Entities;
using Domain.Model.Interfaces.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace WebApi.Controllers;

[Authorize]
[Route("api/[controller]")]
[ApiController]
public class MoviesController : ControllerBase
{
    private readonly IMovieService _domainService;

    public MoviesController(IMovieService domainService)
    {
        _domainService = domainService;
    }

    [HttpGet]
    [Route("getmovies")]
    public async Task<IEnumerable<MovieEntity>> GetMovies()
    {
        return await _domainService.GetAllAsync();
    }

    [HttpGet]
    [Route("getmovie/{id}")]
    public async Task<ActionResult<MovieEntity>> GetMovie(int id)
    {
        var movie = await _domainService.GetByIdAsync(id);

        if (movie == null)
        {
            return NotFound();
        }
        return movie;
    }

    [HttpPost]
    [Route("createmovie")]
    public async Task<ActionResult<MovieEntity>> PostMovie([FromForm] MovieEntity movie)
    {
        var file = Request.Form.Files.SingleOrDefault();

        await _domainService.InsertAsync(movie, file.OpenReadStream());
        return CreatedAtAction("GetMovie", new { id = movie.Id }, movie);
    }

    [HttpPut]
    [Route("updatemovie")]
    public async Task<ActionResult<MovieEntity>> PutMovie([FromForm] MovieEntity movie)
    {
        var file = Request.Form.Files.SingleOrDefault();
        await _domainService.UpdateAsync(movie, file.OpenReadStream());
        return CreatedAtAction("GetMovie", new { id = movie.Id }, movie);
    }

    [HttpDelete]
    [Route("removemovie/{id}")]
    public async Task<IActionResult> DeleteMovie(int id)
    {
        var movie = await _domainService.GetByIdAsync(id);
        await _domainService.DeleteAsync(movie);
        return NoContent();
    }
}