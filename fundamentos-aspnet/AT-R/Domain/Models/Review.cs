using System.ComponentModel.DataAnnotations;

namespace Domain.Models;
public class Review
{

    public int Id { get; set; }

    [Display(Name = "Avaliação")]
    [Required(ErrorMessage = "Por favor digite a sua Avaliação!")]
    public string? TextReview { get; set; }

    [Display(Name = "Nota")]
    [Required(ErrorMessage = "Por favor digite a nota do Filme!")]
    [Range(0,5)]
    public int Score { get; set; }

    [DataType(DataType.Date)]
    [Display(Name = "Data da Avaliação")]
    [Required(ErrorMessage = "Por favor digite a data de Avaliação")]
    [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:dd/MM/yyyy}")]
    public DateTime ReviewDate { get; set; } = DateTime.Now;


    [Display(Name = "Filme")]
    public int MovieId { get; set; }

    [Display(Name = "Filme")]
    public Movie? Movie { get; set; }
}

  

