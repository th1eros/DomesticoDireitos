using System.Collections.Generic;

namespace DomesticoDireitos.Api.DTOs
{
    public class ResumoResponse
    {
        public int DiagnosticoId { get; set; }
        public List<ItemResumoDTO> Itens { get; set; } = new();

        // Campos da Calculadora 
        public decimal SalarioBase { get; set; }
        public int DiasTrabalhados { get; set; }
        public decimal ValorTotalEstimado { get; set; }
    }

    public class ItemResumoDTO
    {
        public string Titulo { get; set; } = string.Empty;
        public string Conteudo { get; set; } = string.Empty;
        public decimal ValorCalculado { get; set; }
    }
}