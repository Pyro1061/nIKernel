using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Web.Repositories;
using MySqlConnector;
using Web.Models.Produto;
using System;
using System.Threading.Tasks;

namespace Web.Pages.Admin.Produtos
{
    public class CriarModel : PageModel
    {
        private readonly ProdutoRepository _repo;
        private readonly CategoriaRepository _catRepo;
        public CriarModel(ProdutoRepository repo, CategoriaRepository catRepo)
        {
            _repo = repo;
            _catRepo = catRepo;
        }

        [BindProperty]
        public ProdutoModel Produto { get; set; } = new ProdutoModel();

        public IEnumerable<Web.Models.Categoria.CtgCategoriaModel> Categorias { get; set; } = new List<Web.Models.Categoria.CtgCategoriaModel>();

        public IActionResult OnGet()
        {
            // Permissão: ajuste conforme sua lógica de claims
            var claim = User.FindFirst("Permissao_Produtos")?.Value;
            if (string.IsNullOrEmpty(claim) || claim.Split(',')[1] != "S")
                return RedirectToPage("/Index");
            Produto.prd_ativo = "S";
            Produto.prd_data_criacao = System.DateTime.Now;
            Categorias = _catRepo.ListarTodosAsync().GetAwaiter().GetResult();
            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            // Permissão: Inserir
            var claim = User.FindFirst("Permissao_Produtos")?.Value;
            if (string.IsNullOrEmpty(claim) || claim.Split(',')[1] != "S")
                return RedirectToPage("/Index");

            if (!ModelState.IsValid)
                return Page();
            Produto.prd_data_criacao = System.DateTime.Now;
            
            // Calcula o preco de venda baseando-se no custo e margem
            Produto.prd_preco_venda = Produto.prd_preco_compra + (Produto.prd_preco_compra * (Produto.prd_margem_venda / 100));

            try
            {
                await _repo.InserirAsync(Produto);
                return RedirectToPage("Index");
            }
            catch (MySqlException ex) when (ex.Number == 1062)
            {
                AddDuplicateKeyError(ex);
                return Page();
            }
        }

        private void AddDuplicateKeyError(MySqlException ex)
        {
            var message = ex.Message ?? string.Empty;
            if (message.Contains("prd_gtin_ean", StringComparison.OrdinalIgnoreCase))
            {
                ModelState.AddModelError("Produto.prd_gtin_ean", "GTIN/EAN ja cadastrado.");
                return;
            }
            if (message.Contains("prd_cod", StringComparison.OrdinalIgnoreCase))
            {
                ModelState.AddModelError("Produto.prd_cod", "Codigo ja cadastrado.");
                return;
            }

            ModelState.AddModelError(string.Empty, "Nao foi possivel salvar o produto porque um valor duplicado foi encontrado.");
        }
    }
}


