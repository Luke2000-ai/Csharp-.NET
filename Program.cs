using System.Xml.Linq;
using Negozio_its31.models;



var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddCors();

var app = builder.Build();

// Configure the HTTP request pipeline.
#region
List<Prodotto> elenco = new List<Prodotto>()
{
    new Prodotto(){ Id = 1, Nome= "Telefono x2", Descrizione = "modello nuovissimo di google", Prezzo = 800, Categoria="Elettronica"},
    new Prodotto(){ Id = 2, Nome= "Computer MSI 34", Descrizione = "il computer da gaming per eccellenza", Prezzo = 1400, Categoria="Elettronica"},
    new Prodotto(){ Id = 3, Nome= "PS5 pro", Descrizione = "che dire, magnifica", Prezzo = 600, Categoria="Elettronica"},
    new Prodotto(){ Id = 4, Nome= "XBOX series X", Descrizione = "bo, forse interessante", Prezzo = 500, Categoria="Elettronica"},
    new Prodotto(){ Id = 4, Nome= "Prosciutto Trentino", Descrizione = "Il miglior prosciutto del Nord", Prezzo = 5, Categoria="Alimentari"},
};

app.MapGet("/", () => elenco);

app.MapGet("/{varId}", (int varId) =>
{
    Prodotto? prod = elenco.FirstOrDefault(p => p.Id == varId);
    if (prod is not null)
        return Results.Ok(prod);

    return Results.NotFound();
});

app.MapPost("/", (Prodotto prod) =>
{
    if (prod.Nome == "" || prod.Descrizione == "")
        return Results.BadRequest();

    prod.Id = elenco.Count + 1;
    elenco.Add(prod);
    return Results.Ok();
});

app.MapDelete("/{varId}", (int varId) => {
    Prodotto? prod = elenco.FirstOrDefault(p => p.Id == varId);
    if (prod is not null)
    {
        elenco.Remove(prod);
        return Results.Ok();
    }

    return Results.NotFound();
});

app.MapPut("/{varId}", (int varId, Prodotto prodNew) =>
{
    if (prodNew.Nome == "" || prodNew.Descrizione == "")
        return Results.BadRequest();

    //Cerco il libro vecchio
    Prodotto? prodOld = elenco.FirstOrDefault(p => p.Id == varId);
    if (prodOld is null)
        return Results.NotFound();

    if (prodNew.Nome is not null)
        prodOld.Nome = prodNew.Nome;

    if (prodNew.Descrizione is not null)
        prodOld.Descrizione = prodNew.Descrizione;

    if (prodNew.Prezzo != 0)
        prodOld.Prezzo = prodNew.Prezzo;

    return Results.Ok();

});

#endregion


app.UseCors(builder =>
    builder.WithOrigins("*").AllowAnyMethod().AllowAnyHeader()
    );

app.Run();


