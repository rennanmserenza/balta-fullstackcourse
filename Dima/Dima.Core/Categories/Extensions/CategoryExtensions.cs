using Dima.Core.Categories.Requests;
using Dima.Core.Models;

namespace Dima.Core.Categories.Extensions;

public static class CategoryExtensions
{
    public static Category ToEntity(this CreateCategoryRequest request)
    {
        return new Category
        {
            UserId = request.UserId,
            Title = request.Title,
            Description = request.Description,
        };
    }
}
