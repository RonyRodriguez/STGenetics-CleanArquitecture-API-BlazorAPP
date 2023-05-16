using Microsoft.Extensions.DependencyInjection.Extensions;
using STG.Api.Controllers;
using STG.Domain.Domain;
using STG.Domain.Entity;
using STG.Infrastructure.Persistence;
using STG.Infrastructure.Shared.Query;
using STG.Infrastructure.Shared.Repository;
using STG.Infrastructure.Shared.UnitOfWork;
using STG.Infrastructure.UnitOfWork;
using STG.Service.Services;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddInfrastructureLayerDependency();
builder.Services.AddServiceLayerDependency();


builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();
builder.Services.AddScoped<IQuery<Animal>, AnimalQuery>();
builder.Services.AddScoped<IRepository<Animal>, AnimalRepository>();
builder.Services.AddScoped<IAnimalService, AnimalService>();

var app = builder.Build();

app.UseHttpsRedirection();

await builder.Services.SeedDatabase(app.Services);


app.MapGet("/animals", async (IAnimalService animalService) =>
{
    return await animalService.GetAll();

});

app.MapGet("/sexes", () =>
{
    return AnimalController.GetSexList();
});


app.MapGet("/breeds", () =>
{
    return AnimalController.GetBreedList();
});

app.MapPost("/animals", async (IAnimalService animalService, IEnumerable<AnimalDomain> animalDomains) =>
{
    await animalService.Update(animalDomains, CancellationToken.None);
    return Results.Ok();
});

app.Run();
