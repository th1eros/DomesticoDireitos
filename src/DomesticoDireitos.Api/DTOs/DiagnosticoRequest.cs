using System.Collections.Generic;

namespace DomesticoDireitos.Api.DTOs
{
    /// <summary>
    /// Esta é a "folha de rosto" que o Front-end preenche 
    /// para enviar os dados da calculadora para a API.
    /// </summary>
    public class DiagnosticoRequest
    {
        // Lista de IDs dos direitos selecionados (ex: [1, 2, 5])
        public List<int> SubitemIds { get; set; } = new List<int>();

        // Salário que o idoso digitou na calculadora
        public decimal SalarioBase { get; set; }

        // Dias trabalhados (opcional, padrão 30 dias)
        public int DiasTrabalhados { get; set; } = 30;
    }
}