using Domain.Model.Entities;
using Domain.Model.Interfaces.Infrastructure;
using Domain.Model.Interfaces.Repositories;
using Domain.Model.Interfaces.Services;
using System.Text.Json;

namespace Domain.Services.Services;

public class ReviewService : IReviewService
{
    private readonly IReviewRepository _repository;
    private readonly IBlobService _blobService;
    private readonly IQueueService _queueService;

    public ReviewService(
        IReviewRepository repository,
        IBlobService blobService,
        IQueueService queueService
    )
    {
        _repository = repository;
        _blobService = blobService;
        _queueService = queueService;
    }

    public async Task<IEnumerable<ReviewEntity>> GetAllAsync()
    {
        return await _repository.GetAllAsync();
    }

    public async Task<ReviewEntity> GetByIdAsync(int id)
    {
        //invocando uma function para atualizar ultima data de visualização
        //await _functionService.InvokeAsync(amigoTask.Result);

        //buscando a entidade amigo com base no id (identificador)
        var reviewTask = _repository.GetByIdAsync(id);

        var reviewEntity = reviewTask.Result;

        //primeira forma de serializar objeto json/base64 (usando package Newtonsoft.Json)
        //var jsonAmigo = JsonConvert.SerializeObject(amigoTask.Result);
        //var bytesJsonAmigo = UTF8Encoding.UTF8.GetBytes(jsonAmigo);
        //string jsonAmigoBase64 = Convert.ToBase64String(bytesJsonAmigo);

        //segunda forma de serializar objeto json/base64 (usando package System.Text.Json)
        //var jsonBytes = JsonSerializer.SerializeToUtf8Bytes(reviewEntity);
        //string jsonAmigoBase64 = Convert.ToBase64String(jsonBytes);

        //enviando objeto serializado em json e base64 como msg para a fila
        //await _queueService.SendAsync(jsonAmigoBase64);

        return reviewEntity;
    }

    public async Task InsertAsync(ReviewEntity reviewEntity)
    {
  
        await _repository.InsertAsync(reviewEntity);
    }

    public async Task UpdateAsync(ReviewEntity reviewEntity)
    {
        await _repository.UpdateAsync(reviewEntity);
    }

    public async Task DeleteAsync(ReviewEntity reviewEntity)
    {
        await _repository.DeleteAsync(reviewEntity);
    }
}