using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using nIKernel.Repositories;
using nIKernel.Models.Fornecedor;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace nIKernel.Pages.Fornecedor
{
    public class IndexModel : PageModel
    {
        private readonly FornecedorRepository _FornecedorRepo;

        public IndexModel(FornecedorRepository fornecedorRepo)
        {
            _FornecedorRepo = fornecedorRepo;
        }

        public IEnumerable<FornecedorModel> ListaFornecedores { get; set; } = new List<FornecedorModel>();

        public async Task<IActionResult> OnGetAsync()
        {
            var claim = User.FindFirst("Permissao_Fornecedores")?.Value;
            if (string.IsNullOrEmpty(claim) || claim.Split(',')[0] != "S")
                return RedirectToPage("/Index");

            ListaFornecedores = await _FornecedorRepo.GetAllAsync();
            return Page();
        }

        public async Task<IActionResult> OnPostDeleteAsync(int id)
        {
            var claim = User.FindFirst("Permissao_Fornecedores")?.Value;
            if (string.IsNullOrEmpty(claim) || claim.Split(',')[3] != "S")
                return RedirectToPage("/Index");

            await _FornecedorRepo.DeleteAsync(id);
            return RedirectToPage();
        }
    }
}