using Dima.Api.Data;
using Dima.Core.Categories.Extensions;
using Dima.Core.Categories.Handlers;
using Dima.Core.Categories.Requests;
using Dima.Core.Models;
using Dima.Core.Responses;
using Microsoft.EntityFrameworkCore;

namespace Dima.Api.Categories.Handlers;

public class CategoryHandler(AppDbContext context) : ICategoryHandler
{
    public async Task<Response<Category?>> CreateAsync(CreateCategoryRequest request)
    {
        try
        {
            var entity = request.ToEntity();

            await context.Categories.AddAsync(entity);
            await context.SaveChangesAsync();

            return new Response<Category?>(entity, 201, "Categoria criada com sucesso!");
        }
        catch
        {
            return new Response<Category?>(null, 500, "Não foi possível criar a categoria.");
        }
    }

    public async Task<Response<Category?>> UpdateAsync(UpdateCategoryRequest request)
    {
        try
        {
            var entity = await context.Categories.FirstOrDefaultAsync(x => x.Id == request.Id && x.UserId == request.UserId);

            if (entity == null)
                return new Response<Category?>(null, 404, "Categoria não encontrada.");

            entity.Title = request.Title;
            entity.Description = request.Description;

            context.Categories.Update(entity);
            await context.SaveChangesAsync();

            return new Response<Category?>(entity, message: "Categoria alterada com sucesso");
        }
        catch
        {
            return new Response<Category?>(null, 500, "Não foi possível alterar a categoria.");
        }
    }

    public async Task<Response<Category?>> DeleteAsync(DeleteCategoryRequest request)
    {
        try
        {
            var entity = await context.Categories.FirstOrDefaultAsync(x => x.Id == request.Id && x.UserId == request.UserId);

            if (entity == null)
                return new Response<Category?>(null, 404, "Categoria não encontrada.");

            context.Categories.Remove(entity);
            await context.SaveChangesAsync();

            return new Response<Category?>(entity, message: "Categoria excluída com sucesso!");
        }
        catch
        {
            return new Response<Category?>(null, 500, "Não foi possível excluir a categoria.");
        }
    }

    public Task<Response<Category?>> GetByIdAsync(GetCategoryByIdRequest request)
    {
        throw new NotImplementedException();
    }

    public Task<Response<List<Category>>> GetAllAsync(GetAllCategoriesRequest request)
    {
        throw new NotImplementedException();
    }
}
