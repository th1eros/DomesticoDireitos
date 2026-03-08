using DomesticoDireitos.Api.Data;
using DomesticoDireitos.Api.DTOs;
using Microsoft.EntityFrameworkCore;

namespace DomesticoDireitos.Api.Services
{
    public class DiagnosticoService
    {
        private readonly ApplicationDbContext _context;

        public DiagnosticoService(ApplicationDbContext context) => _context = context;

        public async Task<ResumoResponse?> GerarResumoComCalculoAsync(int diagnosticoId, decimal salarioBase, int diasTrabalhados)
        {
            // Atende: Segurança (Busca segura com tratamento de nulo)
            var diagnostico = await _context.Diagnosticos
                .Include(d => d.ItensSelecionados)
                .ThenInclude(i => i.Subitem)
                .FirstOrDefaultAsync(d => d.Id == diagnosticoId);

            // Se não encontrar o diagnóstico, retorna null com segurança
            if (diagnostico == null) return null;

            var resposta = new ResumoResponse
            {
                DiagnosticoId = diagnosticoId,
                SalarioBase = salarioBase,
                DiasTrabalhados = diasTrabalhados
            };

            foreach (var item in diagnostico.ItensSelecionados)
            {
                // Verifica se o Subitem existe antes de acessar o Titulo (Elimina o aviso CS8602)
                if (item.Subitem == null) continue;

                decimal valorCalculado = 0;
                string titulo = item.Subitem.Titulo;

                if (titulo.Contains("FGTS"))
                    valorCalculado = salarioBase * 0.112m;
                else if (titulo.Contains("Férias"))
                    valorCalculado = ((salarioBase / 30) * diasTrabalhados) * 1.3333m;
                else if (titulo.Contains("Salário") || titulo.Contains("Jornada"))
                    valorCalculado = salarioBase;

                resposta.Itens.Add(new ItemResumoDTO
                {
                    Titulo = titulo,
                    Conteudo = item.Subitem.Conteudo ?? "Informação não disponível.",
                    ValorCalculado = Math.Round(valorCalculado, 2)
                });
            }

            resposta.ValorTotalEstimado = resposta.Itens.Sum(i => i.ValorCalculado);
            return resposta;
        }
    }
}