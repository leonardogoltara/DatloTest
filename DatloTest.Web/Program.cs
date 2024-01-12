using DatloTest.Infrastructure.MongoDBService.Repositories;
using DatloTest.Infrastructure.MongoDBService.Services;
using DatloTest.Infrastructure.Repository;
using DatloTest.Infrastructure.Services;
using DatloTest.Infrastructure.Excel;
using DatloTest.Service.Interfaces;
using DatloTest.Service.Services;
using Microsoft.AspNetCore.Mvc;
using System.Data;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddScoped<IExcelReaderService, ExcelReaderService>();
builder.Services.AddScoped<IConjuntoService, ConjuntoService>();
builder.Services.AddScoped<IMongoDBService, MongoDBService>();
builder.Services.AddScoped<IConjuntoRepository, ConjuntoRepository>();

System.Text.Encoding.RegisterProvider(System.Text.CodePagesEncodingProvider.Instance);

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

#region "Entrada de Dados"

app.MapGet("ListaConjuntos", ([FromServices] IConjuntoService conjuntoService,
    string? nomeConjunto) =>
{
    return conjuntoService.ListaConjuntos(nomeConjunto);
})
    .WithName("ListaConjuntos")
    .WithTags("Entrada de Dados");

app.MapPost("/CarregarConjunto", ([FromServices] IConjuntoService conjuntoService,
    [FromServices] IExcelReaderService excelReaderService,
    string nome,
    IFormFile arquivo) =>
{
    DataTable? dataTable;
    if (!string.IsNullOrEmpty(arquivo?.Name))
    {
        string tempfile = excelReaderService.CreateTempFilePath(arquivo.FileName);
        using (var stream = File.OpenWrite(tempfile))
        {
            arquivo.CopyToAsync(stream).Wait();
        }

        // Format CSV or XLSX to DataTable
        dataTable = excelReaderService.ReadExcelFile(tempfile);

        if (dataTable != null)
           return conjuntoService.CarregarConjunto(nome, dataTable);
    }

    return null;
})
    .WithName("CarregarConjunto")
    .WithTags("Entrada de Dados")
    .DisableAntiforgery();

app.MapPost("/AtualizarConjunto", ([FromServices] IConjuntoService conjuntoService,
    [FromServices] IExcelReaderService excelReaderService,
    Guid idConjunto,
    string? nome,
    IFormFile arquivo) =>
{
    DataTable? dataTable;
    if (!string.IsNullOrEmpty(arquivo?.Name))
    {
        string tempfile = excelReaderService.CreateTempFilePath(arquivo.FileName);
        using (var stream = File.OpenWrite(tempfile))
        {
            arquivo.CopyToAsync(stream).Wait();
        }

        // Format CSV or XLSX to DataTable
        dataTable = excelReaderService.ReadExcelFile(tempfile);

        if (dataTable != null)
            return conjuntoService.AtualizarConjunto(idConjunto, nome, dataTable);
    }

    return null;
})
    .WithName("AtualizarConjunto")
    .WithTags("Entrada de Dados")
    .DisableAntiforgery();

#endregion

#region "Relatórios"

app.MapPost("ConsultarConjunto", ([FromServices] IConjuntoService conjuntoService,
    [FromServices] IExcelReaderService excelReaderService,
    Guid idConjunto,
    IFormFile arquivoFilter) =>
{
    DataTable dataTable = null;
    if (!string.IsNullOrEmpty(arquivoFilter?.Name))
    {
        string tempfile = excelReaderService.CreateTempFilePath(arquivoFilter.FileName);
        using (var stream = File.OpenWrite(tempfile))
        {
            arquivoFilter.CopyToAsync(stream).Wait();
        }

        // Format CSV or XLSX to DataTable
        dataTable = excelReaderService.ReadExcelFile(tempfile);
    }

    return conjuntoService.ConsultarConjunto(idConjunto, dataTable);
})
    .WithName("ConsultarConjunto")
    .WithTags("Relatórios")
    .DisableAntiforgery();

#endregion


app.Run();