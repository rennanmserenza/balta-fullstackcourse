using Dima.Api.Categories.Handlers;
using Dima.Api.Data;
using Dima.Core.Categories.Handlers;
using Dima.Core.Categories.Requests;
using Dima.Core.Models;
using Dima.Core.Responses;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

var cnnStr = builder
    .Configuration
    .GetConnectionString("DefaultConnection")
    ?? string.Empty;

builder.Services.AddDbContext<AppDbContext>(x =>
{
    x.UseSqlServer(cnnStr);
});

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(x =>
{
    x.CustomSchemaIds(n => n.FullName);
});

builder.Services.AddTransient<ICategoryHandler, CategoryHandler>();

var app = builder.Build();

app.UseSwagger();
app.UseSwaggerUI();

app.MapPost("/v1/categories", 
    async (CreateCategoryRequest request
    , ICategoryHandler handler) 
        => await handler.CreateAsync(request))
    .WithName("Categories: Create")
    .WithSummary("Cria uma nova categoria")
    .Produces<Response<Category?>>();

app.MapPut("/v1/categories/{id}",
    async (long id
    , UpdateCategoryRequest request
    , ICategoryHandler handler) => {
        request.Id = id;
        return await handler.UpdateAsync(request);
    })
    .WithName("Categories: Update")
    .WithSummary("Atualiza uma categoria")
    .Produces<Response<Category?>>();

app.MapDelete("/v1/categories/{id}",
    async (long id, ICategoryHandler handler) => {
        var request = new DeleteCategoryRequest {Id = id};
        return await handler.DeleteAsync(request);
    })
    .WithName("Categories: Delete")
    .WithSummary("Exclui uma categoria")
    .Produces<Response<Category?>>();

app.MapGet("/v1/categories/{id}",
    async (long id, ICategoryHandler handler) => {
        var request = new GetCategoryByIdRequest { Id = id, UserId = "rennan@email.com" };
        return await handler.GetByIdAsync(request);
    })
    .WithName("Categories: GetById")
    .WithSummary("Obtém uma categoria pelo seu Id.")
    .Produces<Response<Category?>>();

app.MapGet("/v1/categories/pageNumber={pageNumber}&pageSize={pageSize}",
    async (int pageNumber, int pageSize, ICategoryHandler handler) => {
        var request = new GetAllCategoriesRequest { PageNumber = pageNumber , PageSize = pageSize, UserId = "rennan@email.com" };
        return await handler.GetAllAsync(request);
    })
    .WithName("Categories: GetAll")
    .WithSummary("Retorna todas as categorias de um usuário.")
    .Produces<PagedResponse<List<Category>?>>();

app.Run();
