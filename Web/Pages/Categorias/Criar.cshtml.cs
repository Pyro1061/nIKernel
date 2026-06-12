using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Web.Repositories;
using Web.Models.Categoria;
using System.Threading.Tasks;
using MySqlConnector;

namespace Web.Pages.Categorias
{
    public class CriarModel : PageModel
    {
        private readonly CategoriaRepository _repo;
        public CriarModel(CategoriaRepository repo) => _repo = repo;

        [BindProperty]
        public CtgCategoriaModel Categoria { get; set; } = new CtgCategoriaModel();

        public IActionResult OnGet()
        {
            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
                return Page();

            try
            {
                await _repo.InserirAsync(Categoria);
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
