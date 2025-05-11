namespace Negozio_its31.models
{
    public class Prodotto
    {
        public int Id { get; set; }
        public string? Nome { get; set; }
        public string? Descrizione { get; set; }

        public int Codice  { get; set; }
        public decimal Prezzo { get; set; }
        public string? Categoria { get; set; }
    }
}
