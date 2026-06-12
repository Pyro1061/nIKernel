using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Web.Repositories;
using Web.Models.Categoria;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Web.Pages.Categorias
{
    public class IndexModel : PageModel
    {
        private readonly CategoriaRepository _repo;
        public IndexModel(CategoriaRepository repo) => _repo = repo;

        public IEnumerable<CtgCategoriaModel> Categorias { get; set; } = new List<CtgCategoriaModel>();

        public async Task<IActionResult> OnGetAsync()
        {
            Categorias = await _repo.ListarTodosAsync(false);
            return Page();
        }

        public async Task<IActionResult> OnPostDeletarAsync(int id)
        {
            await _repo.DeletarAsync(id);
            return RedirectToPage();
        }
    }
}
