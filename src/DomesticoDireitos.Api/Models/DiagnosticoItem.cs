namespace DomesticoDireitos.Api.Models
{
    public class DiagnosticoItem
    {
        // Chave Composta: Liga a Consulta (Diagnostico) ao Direito (Subitem)
        public int DiagnosticoId { get; set; }
        public virtual Diagnostico? Diagnostico { get; set; }

        public int SubitemId { get; set; }
        public virtual Subitem? Subitem { get; set; }
    }
}