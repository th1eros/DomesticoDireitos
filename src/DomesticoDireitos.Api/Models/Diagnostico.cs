using System;
using System.Collections.Generic;

namespace DomesticoDireitos.Api.Models
{
    public class Diagnostico
    {
        public int Id { get; set; }
        public DateTime DataCriacao { get; set; } = DateTime.Now;

        // Relacionamento: Itens que compõem este diagnóstico específico
        public virtual ICollection<DiagnosticoItem> ItensSelecionados { get; set; } = new List<DiagnosticoItem>();
    }
}