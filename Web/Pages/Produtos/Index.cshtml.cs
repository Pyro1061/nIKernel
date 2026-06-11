using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Web.Repositories;
using Web.Models.Produto;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Web.Pages.Admin.Produtos
{
    public class IndexModel : PageModel
    {
        private readonly ProdutoRepository _repo;
        public IndexModel(ProdutoRepository repo) => _repo = repo;

        public IEnumerable<ProdutoModel> Produtos { get; set; } = new List<ProdutoModel>();

        [BindProperty(SupportsGet = true)]
        public bool ApenasAtivos { get; set; } = true;

        public async Task<IActionResult> OnGetAsync()
        {
            var claim = User.FindFirst("Permissao_Produtos")?.Value;
            if (string.IsNullOrEmpty(claim) || claim.Split(',')[0] != "S")
                return RedirectToPage("/Index");

            Produtos = await _repo.ListarTodosAsync(ApenasAtivos);
            return Page();
        }

        public async Task<IActionResult> OnPostDeletarAsync(int id)
        {
            var claim = User.FindFirst("Permissao_Produtos")?.Value;
            if (string.IsNullOrEmpty(claim) || claim.Split(',')[3] != "S")
                return RedirectToPage("/Index");

            await _repo.DeletarAsync(id);
            return RedirectToPage();
        }
    }
}
