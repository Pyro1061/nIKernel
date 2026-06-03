using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using nIKernel.Models.Fornecedor;
using nIKernel.Repositories;
using System.Threading.Tasks;

namespace nIKernel.Pages.Fornecedor
{
    public class CriarModel : PageModel
    {
        private readonly FornecedorRepository _fornecedorRepository;

        public CriarModel(FornecedorRepository fornecedorRepository)
        {
            _fornecedorRepository = fornecedorRepository;
        }

        [BindProperty]
        public FornecedorModel Fornecedor { get; set; } = new();

        public string? ErrorMessage { get; set; }

        public IActionResult OnGet()
        {
            var claim = User.FindFirst("Permissao_Fornecedores")?.Value;
            if (string.IsNullOrEmpty(claim) || claim.Split(',')[1] != "S")
                return RedirectToPage("/Index");

            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            var claim = User.FindFirst("Permissao_Fornecedores")?.Value;
            if (string.IsNullOrEmpty(claim) || claim.Split(',')[1] != "S")
                return RedirectToPage("/Index");

            if (!ModelState.IsValid)
            {
                return Page();
            }

            try
            {
                await _fornecedorRepository.InsertAsync(Fornecedor);
                return RedirectToPage("Index");
            }
            catch
            {
                ErrorMessage = "Erro ao salvar o fornecedor. Verifique os dados e tente novamente.";
                ModelState.AddModelError(string.Empty, ErrorMessage);
                return Page();
            }
        }
    }
}