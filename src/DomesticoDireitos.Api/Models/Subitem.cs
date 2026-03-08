using System.Text.Json.Serialization;

namespace DomesticoDireitos.Api.Models
{
    public class Subitem
    {
        public int Id { get; set; }
        public string Titulo { get; set; } = string.Empty; // Ex: "Adicional Noturno"
        public string Conteudo { get; set; } = string.Empty; // Texto da Lei 150/2015

        // Chave Estrangeira para Categoria
        public int CategoriaId { get; set; }

        [JsonIgnore] // Impede que o JSON entre em loop infinito no Swagger
        public virtual Categoria? Categoria { get; set; }
    }
}