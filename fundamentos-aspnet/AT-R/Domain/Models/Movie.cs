using System.ComponentModel.DataAnnotations;

namespace Domain.Models;
public class Movie
{
    public int Id { get; set; }

    [Display(Name = "Nome")]
    [Required(ErrorMessage = "Por favor digite o nome do Filme!")]
    public string? Name { get; set; }

    [Display(Name = "Estúdio")]
    [Required(ErrorMessage = "Por favor digite o nome do Estúdio!")]
    public string? FilmStudio { get; set; }

    [Required(ErrorMessage = "Por favor digite a Data de Lançamento!")]
    [DataType(DataType.Date)]
    [Display(Name = "Data de Lançamento")]
    [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:dd/MM/yyyy}")]
    public DateTime? ReleaseDate { get; set; }

    [Required(ErrorMessage = "Por favor digite o valor da Bilheteria em milhões!")]
    [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:F2}")]
    [Display(Name = "Bilheteria")]
    public decimal? BoxOffice { get; set; }
    public ICollection<Review>? Reviews { get; set; }
}
