using Domain.Model.Entities.Commons;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Model.Entities;
public class MovieEntity : BaseEntity
{

    [Display(Name = "Nome")]
    [Required(ErrorMessage = "Por favor digite o nome do Filme!")]
    public string Name { get; set; }

    [Display(Name = "Estúdio")]
    [Required(ErrorMessage = "Por favor digite o nome do Estúdio!")]
    public string FilmStudio { get; set; }

    [Required(ErrorMessage = "Por favor digite a Data de Lançamento!")]
    [DataType(DataType.Date)]
    [Display(Name = "Data de Lançamento")]
    [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:dd/MM/yyyy}")]
    public DateTime ReleaseDate { get; set; }

    [Required(ErrorMessage = "Por favor digite o valor da Bilheteria em milhões!")]
    [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:F2}")]
    [Display(Name = "Bilheteria")]
    public decimal BoxOffice { get; set; }

    [Required(ErrorMessage = "Por favor digite a URL do Trailer!")]
    [Display(Name = "Trailer")]
    public string VideoUrl { get; set; }

    public string ImageUrl { get; set; }
    public ICollection<ReviewEntity> Reviews { get; set; }

    [NotMapped]
    [JsonProperty("image")]
    public IFormFile Image { get; set; }

    [Display(Name = "Último Acesso Síncrono")]
    public DateTime LastView { get; set; }

    [Display(Name = "Último Acesso Assíncrono")]
    public DateTime LastViewQueue { get; set; }
}