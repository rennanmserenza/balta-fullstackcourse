using Dima.Core.Requests;
using System.ComponentModel.DataAnnotations;

namespace Dima.Core.Categories.Requests;

public class UpdateCategoryRequest : Request
{
    [Required(ErrorMessage = "O Id é obriatório")]
    public long Id { get; set; }

    [Required(ErrorMessage = "Título obriatório")]
    [MaxLength(80, ErrorMessage = "O Título de conter até 80 caracteres")]
    public string Title { get; set; } = string.Empty;

    [Required(ErrorMessage = "Descrição obriatório")]
    public string Description { get; set; } = string.Empty;
}
