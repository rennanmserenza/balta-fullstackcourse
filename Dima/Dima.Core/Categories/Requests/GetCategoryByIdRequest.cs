using Dima.Core.Requests;
using System.ComponentModel.DataAnnotations;

namespace Dima.Core.Categories.Requests;

public class GetCategoryByIdRequest : Request
{
    [Required(ErrorMessage = "O Id é obrigatório")]
    public long Id { get; set; }
}
