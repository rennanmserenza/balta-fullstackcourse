using Dima.Core.Requests;
using System.ComponentModel.DataAnnotations;

namespace Dima.Core.Categories.Requests;

public class CreateCategoryRequest : Request
{
    [Required(ErrorMessage = "Título inválido")]
    [MaxLength(80, ErrorMessage = "O Título de conter até 80 caracteres")]
    public string Title { get; set; } = string.Empty;

    [Required(ErrorMessage = "Descrição inválida")]
    public string Description { get; set; } = string.Empty;
}
