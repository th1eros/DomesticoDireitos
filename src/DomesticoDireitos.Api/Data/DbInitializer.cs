using DomesticoDireitos.Api.Models;
using System.Linq;

namespace DomesticoDireitos.Api.Data
{
    public static class DbInitializer
    {
        public static void Initialize(ApplicationDbContext context)
        {
            // Atende: Segurança (Não duplica dados se o banco já estiver populado)
            if (context.Categorias.Any()) return;

            // 1. Criar Categorias em ordem específica
            var catRemuneracao = new Categoria { Nome = "Remuneração e Encargos" };
            var catJornada = new Categoria { Nome = "Jornada e Descanso" };
            var catRescisao = new Categoria { Nome = "Rescisão e Proteção" };

            context.Categorias.AddRange(catRemuneracao, catJornada, catRescisao);
            context.SaveChanges(); // Salva para gerar os IDs 1, 2 e 3

            // 2. Criar Subitens vinculados aos IDs gerados
            var subitens = new Subitem[]
            {
                // Vinculados à Categoria 1
                new Subitem {
                    Titulo = "FGTS e Antecipação de Multa",
                    Conteudo = "Alíquota de 8% de FGTS mais 3,2% para fundo de multa rescisória (Lei 150/2015).",
                    CategoriaId = catRemuneracao.Id
                },
                new Subitem {
                    Titulo = "Salário Mínimo ou Piso",
                    Conteudo = "Garantia de remuneração nunca inferior ao mínimo nacional ou piso regional.",
                    CategoriaId = catRemuneracao.Id
                },

                // Vinculados à Categoria 2
                new Subitem {
                    Titulo = "Jornada de Trabalho",
                    Conteudo = "Limite de 8h diárias e 44h semanais, com pagamento de horas extras.",
                    CategoriaId = catJornada.Id
                },
                new Subitem {
                    Titulo = "Intervalo para Repouso",
                    Conteudo = "Intervalo de 1h a 2h para jornadas acima de 6h, reduzível para 30min via acordo.",
                    CategoriaId = catJornada.Id
                },

                // Vinculados à Categoria 3
                new Subitem {
                    Titulo = "Seguro-Desemprego",
                    Conteudo = "Direito ao benefício em caso de dispensa sem justa causa.",
                    CategoriaId = catRescisao.Id
                }
            };

            context.Subitens.AddRange(subitens);
            context.SaveChanges();
        }
    }
}