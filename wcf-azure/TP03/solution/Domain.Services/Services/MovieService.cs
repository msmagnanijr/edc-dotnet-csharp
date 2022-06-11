using Domain.Model.Entities;
using Domain.Model.Interfaces.Infrastructure;
using Domain.Model.Interfaces.Repositories;
using Domain.Model.Interfaces.Services;
using Newtonsoft.Json;
using System.Text;
using System.Text.Json;

namespace Domain.Services.Services;

public class MovieService : IMovieService
{
    private readonly IMovieRepository _repository;
    private readonly IBlobService _blobService;
    private readonly IQueueService _queueService;

    public MovieService(
        IMovieRepository repository,
        IBlobService blobService,
        IQueueService queueService
    )
    {
        _repository = repository;
        _blobService = blobService;
        _queueService = queueService;
    }

    public async Task<IEnumerable<MovieEntity>> GetAllAsync()
    {
        return await _repository.GetAllAsync();
    }

    public async Task<MovieEntity> GetByIdAsync(int id)
    {
        //invocando uma function para atualizar ultima data de visualização
        //await _functionService.InvokeAsync(amigoTask.Result);

        //buscando a entidade amigo com base no id (identificador)
        var movieTask = _repository.GetByIdAsync(id);

        var movieEntity = movieTask.Result;

        //primeira forma de serializar objeto json/base64 (usando package Newtonsoft.Json)
        var jsonMovie = JsonConvert.SerializeObject(movieTask.Result);
        var bytesJsonMovie = UTF8Encoding.UTF8.GetBytes(jsonMovie);
        string jsonMovieBase64 = Convert.ToBase64String(bytesJsonMovie);

        //segunda forma de serializar objeto json/base64 (usando package System.Text.Json)
        //var jsonBytes = JsonSerializer.SerializeToUtf8Bytes(movieEntity);
        //string jsonAmigoBase64 = Convert.ToBase64String(jsonBytes);

        //enviando objeto serializado em json e base64 como msg para a fila
        await _queueService.SendAsync(jsonMovieBase64);

        return movieEntity;
    }

    public async Task InsertAsync(MovieEntity movieEntity, Stream stream)
    {
        var newUri = await _blobService.UploadAsync(stream);
        movieEntity.ImageUrl = newUri;

        await _repository.InsertAsync(movieEntity);
    }

    public async Task UpdateAsync(MovieEntity movieEntity, Stream stream)
    {
        //var movie = await _repository.GetByIdAsync(movieEntity.Id);
        //await _blobService.DeleteAsync(movie.ImageUrl);
        var newUri = await _blobService.UploadAsync(stream);
        movieEntity.ImageUrl = newUri;
        await _repository.UpdateAsync(movieEntity);
    }

    public async Task DeleteAsync(MovieEntity movieEntity)
    {
        if(!(movieEntity.ImageUrl == null))
            await _blobService.DeleteAsync(movieEntity.ImageUrl);
        await _repository.DeleteAsync(movieEntity);
    }
}