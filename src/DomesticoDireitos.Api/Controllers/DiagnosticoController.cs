using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using DomesticoDireitos.Api.Data;
using DomesticoDireitos.Api.Models;
using DomesticoDireitos.Api.Services;
using DomesticoDireitos.Api.DTOs;

namespace DomesticoDireitos.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class DiagnosticoController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        private readonly DiagnosticoService _service;

        public DiagnosticoController(ApplicationDbContext context, DiagnosticoService service)
        {
            _context = context;
            _service = service;
        }

        // 1. Obter opções para os senhores escolherem (GET)
        [HttpGet("obter-opcoes-setup")]
        public async Task<ActionResult> ObterOpcoesSetup()
        {
            var categorias = await _context.Categorias
                .Include(c => c.Subitens)
                .OrderBy(c => c.Id)
                .Select(c => new {
                    c.Id,
                    c.Nome,
                    Subitens = c.Subitens.OrderBy(s => s.Id).ToList()
                })
                .ToListAsync();

            return Ok(categorias);
        }

        // 2. O CORAÇÃO DA CALCULADORA (POST)
        [HttpPost("gerar-analise-direitos")]
        public async Task<ActionResult<ResumoResponse>> GerarAnaliseDireitos([FromBody] DiagnosticoRequest request)
        {
            // Validação de Segurança (CISO)
            if (request.SubitemIds == null || !request.SubitemIds.Any())
                return BadRequest("Por favor, selecione ao menos um direito para calcular.");

            if (request.SalarioBase <= 0)
                return BadRequest("Informe um valor de salário válido para o cálculo.");

            var diagnostico = new Diagnostico { DataCriacao = DateTime.Now };

            foreach (var id in request.SubitemIds)
                diagnostico.ItensSelecionados.Add(new DiagnosticoItem { SubitemId = id });

            _context.Diagnosticos.Add(diagnostico);
            await _context.SaveChangesAsync();

            // Atende: Cálculo financeiro (Calcula e gera o resumo)
            var resumo = await _service.GerarResumoComCalculoAsync(
                diagnostico.Id,
                request.SalarioBase,
                request.DiasTrabalhados
            );

            return Ok(resumo);
        }

        // 3. Atualizar cálculo existente (PUT)
        [HttpPut("atualizar-analise-existente/{id}")]
        public async Task<ActionResult<ResumoResponse>> AtualizarAnaliseExistente(int id, [FromBody] DiagnosticoRequest request)
        {
            var diagnostico = await _context.Diagnosticos
                .Include(d => d.ItensSelecionados)
                .FirstOrDefaultAsync(d => d.Id == id);

            if (diagnostico == null) return NotFound("Cálculo não encontrado.");

            _context.DiagnosticosItens.RemoveRange(diagnostico.ItensSelecionados);

            foreach (var subId in request.SubitemIds)
                diagnostico.ItensSelecionados.Add(new DiagnosticoItem { SubitemId = subId });

            await _context.SaveChangesAsync();

            var resumo = await _service.GerarResumoComCalculoAsync(diagnostico.Id, request.SalarioBase, request.DiasTrabalhados);
            return Ok(resumo);
        }

        // 4. Remover do histórico (DELETE)
        [HttpDelete("remover-historico-analise/{id}")]
        public async Task<ActionResult> RemoverHistoricoAnalise(int id)
        {
            var diagnostico = await _context.Diagnosticos.FindAsync(id);
            if (diagnostico == null) return NotFound("Registro não localizado.");

            _context.Diagnosticos.Remove(diagnostico);
            await _context.SaveChangesAsync();

            return NoContent();
        }
    }
}