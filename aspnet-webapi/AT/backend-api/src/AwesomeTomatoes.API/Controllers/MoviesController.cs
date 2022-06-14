using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using AwesomeTomatoes.BLL.Models;
using WebAwesomeTomatoes.Models;
using Microsoft.AspNetCore.Authorization;
using Azure.Storage.Blobs;
using Azure.Storage.Blobs.Models;

namespace AwesomeTomatoes.API.Controllers;

[Authorize]
[Route("api/[controller]")]
[ApiController]
public class MoviesController : ControllerBase
{
    private readonly EFContext _context;
    private readonly string _azureConnectionString;

    public MoviesController(EFContext context, IConfiguration configuration)
    {
        _context = context;
        _azureConnectionString = configuration.GetConnectionString("AzureConnectionString");

    }

    // GET: api/Movies
    [HttpGet]
    public async Task<ActionResult<IEnumerable<Movie>>> GetMovies()
    {
        return await _context.Movies.ToListAsync();
    }

    // GET: api/Movies/5
    [HttpGet("{id}")]
    public async Task<ActionResult<Movie>> GetMovie(int id)
    {
       var movie = await _context.Movies.FindAsync(id);
        

        if (movie == null)
        {
            return NotFound();
        }

        return movie;
    }

    // PUT: api/Movies/5
    // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
    [HttpPut("{id}")]
    public async Task<IActionResult> PutMovie(int id, Movie movie)
    {
        Console.WriteLine(movie.Id);
        Console.WriteLine(id);


        if (id != movie.Id)
        {
            return BadRequest();
        }

        _context.Entry(movie).State = EntityState.Modified;

        try
        {
            await _context.SaveChangesAsync();
        }
        catch (DbUpdateConcurrencyException)
        {
            if (!MovieExists(id))
            {
                return NotFound();
            }
            else
            {
                throw;
            }
        }

        return NoContent();
    }

    // POST: api/Movies
    // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
    [HttpPost]
    public async Task<ActionResult<Movie>> PostMovie(Movie movie)
    {
        _context.Movies.Add(movie);
        await _context.SaveChangesAsync();

        return CreatedAtAction("GetMovie", new { id = movie.Id }, movie);
    }

    // DELETE: api/Movies/5
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteMovie(int id)
    {
        var movie = await _context.Movies.FindAsync(id);
        if (movie == null)
        {
            return NotFound();
        }

        _context.Movies.Remove(movie);
        await _context.SaveChangesAsync();

        return NoContent();
    }

    private bool MovieExists(int id)
    {
        return _context.Movies.Any(e => e.Id == id);
    }


    [HttpPost]
    [Route("upload")]
    public async Task<IActionResult> Upload()
    {
        try
        {
            var formCollection = await Request.ReadFormAsync();
            var file = formCollection.Files.First();

            if (file.Length > 0)
            {
                var container = new BlobContainerClient(_azureConnectionString, "upload-container");
                var createResponse = await container.CreateIfNotExistsAsync();
                if (createResponse != null && createResponse.GetRawResponse().Status == 201)
                    await container.SetAccessPolicyAsync(Azure.Storage.Blobs.Models.PublicAccessType.Blob);

                var blob = container.GetBlobClient(file.FileName);
                await blob.DeleteIfExistsAsync(DeleteSnapshotsOption.IncludeSnapshots);
                using (var fileStream = file.OpenReadStream())
                {
                    await blob.UploadAsync(fileStream, new BlobHttpHeaders { ContentType = file.ContentType });
                }

                return Ok(blob.Uri.ToString());
            }

            return BadRequest();
        }
        catch (Exception ex)
        {
            return StatusCode(500, $"Internal server error: {ex}");
        }
    }

    [HttpPost]
    [Route("blob")]
    public async Task<ActionResult<MovieBlob>> PostMovieBlob(MovieBlob movieBlob)
    {
        _context.MovieBlobs.Add(movieBlob);
        await _context.SaveChangesAsync();

        return NoContent();
    }

    [HttpGet]
    [Route("[action]/{movieId}")]
    public async Task<ActionResult<string>> GetMovieBlob(int movieId)
    {
        var movieBlog =  await _context.Set<MovieBlob>().FirstOrDefaultAsync(x => x.MovieId == movieId);
        return movieBlog.BlobUrl;
    }
}
