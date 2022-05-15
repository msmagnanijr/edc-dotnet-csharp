﻿namespace AwesomeTomatoes.WEB.Models;
public class MovieBlob
{
    public int Id { get; set; }

    public string Description { get; set; }

    public string BlobUrl { get; set; }

    public int MovieId { get; set; }

    public Movie Movie { get; set; }
}
