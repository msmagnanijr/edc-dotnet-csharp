﻿namespace Domain.Model.Interfaces.Infrastructure;

public interface IQueueService
{
    Task SendAsync(string messageText);
}
