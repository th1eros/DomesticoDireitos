using System.Collections.Generic;

namespace DomesticoDireitos.Api.Models
{
    public class Categoria
    {
        // Chave Primária para o banco de dados
        public int Id { get; set; }

        // Nome da categoria (Ex: "Direitos Financeiros", "Jornada de Trabalho")
        public string Nome { get; set; } = string.Empty;

        // Relacionamento 1:N (Uma Categoria possui vários Subitens/Direitos)
        public virtual ICollection<Subitem> Subitens { get; set; } = new List<Subitem>();
    }
}