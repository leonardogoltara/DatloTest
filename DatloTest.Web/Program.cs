using DatloTest.Infrastructure.Excel;
using DatloTest.Infrastructure.Services;
using Microsoft.AspNetCore.Mvc;
using System.Data;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddScoped<IExcelReaderService, ExcelReaderService>();
System.Text.Encoding.RegisterProvider(System.Text.CodePagesEncodingProvider.Instance);

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.MapGet("ListaConjuntos", (string nomeConjunto) =>
{
    // Id e Nome
    return "Listar Conjuntos";
})
    .WithName("ListaConjuntos")
    .WithOpenApi();

app.MapGet("ConsultarConjunto", ([FromServices] IExcelReaderService excelReaderService,
    Guid idConjunto,
    IFormFile arquivoFilter) =>
{
    // Id e Nome
    return "Consultar Conjunto";
})
    .WithName("ConsultarConjunto")
    .WithOpenApi();

app.MapPost("/CarregarConjunto", ([FromServices] IExcelReaderService excelReaderService,
    string descricao,
    IFormFile arquivo) =>
{
    // Save file
    string tempfile = excelReaderService.CreateTempFilePath(arquivo.FileName);
    using (var stream = File.OpenWrite(tempfile))
    {
        arquivo.CopyToAsync(stream).Wait();
    }

    // Format CSV or XLSX to DataSet
    DataSet dataSet = excelReaderService.ReadExcelFile(tempfile);



})
    .WithName("CarregarConjunto")
    .DisableAntiforgery();

app.MapPost("/AtualizarConjunto", ([FromServices] IExcelReaderService excelReaderService,
    Guid idConjunto,
    IFormFile arquivo) =>
{
    // Save file
    string tempfile = excelReaderService.CreateTempFilePath(arquivo.FileName);
    using (var stream = File.OpenWrite(tempfile))
    {
        arquivo.CopyToAsync(stream).Wait();
    }

    // Format CSV or XLSX to DataSet
    DataSet dataSet = excelReaderService.ReadExcelFile(tempfile);



})
    .WithName("AtualizarConjunto")
    .DisableAntiforgery();

app.Run();