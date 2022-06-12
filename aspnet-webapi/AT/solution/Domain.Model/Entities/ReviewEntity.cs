using Domain.Model.Entities.Commons;
using System.ComponentModel.DataAnnotations;

namespace Domain.Model.Entities;
public class ReviewEntity : BaseEntity
{
    [Display(Name = "Avaliação")]
    [Required(ErrorMessage = "Por favor digite a sua Avaliação!")]
    public string TextReview { get; set; }

    [Display(Name = "Nota")]
    [Required(ErrorMessage = "Por favor digite a nota do Filme!")]
    public int ReviwerSatisfaction { get; set; }

    [DataType(DataType.Date)]
    [Display(Name = "Data da Avaliação")]
    [Required(ErrorMessage = "Por favor digite a data de Avaliação")]
    [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:dd/MM/yyyy}")]
    public DateTime ReviewDate { get; set; } = DateTime.Now;

    [Display(Name = "Filme")]
    public int MovieId { get; set; }

    [Display(Name = "Filme")]
    public MovieEntity Movie { get; set; }
    public ICollection<CommentEntity> Comments { get; set; }

    [Display(Name = "Avaliador")]
    public string Reviewer { get; set; }
}



