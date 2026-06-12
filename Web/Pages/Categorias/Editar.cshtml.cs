using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Web.Repositories;
using Web.Models.Categoria;
using System.Threading.Tasks;
using MySqlConnector;

namespace Web.Pages.Categorias
{
    public class EditarModel : PageModel
    {
        private readonly CategoriaRepository _repo;
        public EditarModel(CategoriaRepository repo) => _repo = repo;

        [BindProperty]
        public CtgCategoriaModel Categoria { get; set; } = new CtgCategoriaModel();

        public async Task<IActionResult> OnGetAsync(int id)
        {
            var cat = await _repo.BuscarPorIdAsync(id);
            if (cat == null)
                return RedirectToPage("Index");
            Categoria = cat;
            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
                return Page();

            try
            {
                await _repo.AtualizarAsync(Categoria);
                return RedirectToPage("Index");
            }
            catch (MySqlException ex) when (ex.Number == 1062)
            {
                ModelState.AddModelError(string.Empty, "Valor duplicado.");
                return Page();
            }
        }
    }
}
