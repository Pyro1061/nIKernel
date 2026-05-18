using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using nIKernel.Models.Perfil;
using nIKernel.Repositories;

namespace nIKernel.Pages.Admin.Perfis
{
    public class IndexModel : PageModel
    {
      private readonly PerfilRepository _repo;

      public IndexModel (PerfilRepository repo)
      {
        _repo = repo;
      }

      public IEnumerable<PerfilModel> Perfis { get; set; } = new List<PerfilModel>();

      public async Task<IActionResult> OnGetAsync()
      {
        var claimPerfis = User.FindFirst("Permissao_Perfis")?.Value;
        bool podeConsultar = !string.IsNullOrEmpty(claimPerfis) && claimPerfis.Split(',')[0].Trim().ToUpper() == "S";
        
        if (!podeConsultar)
        {
            return RedirectToPage("/Index");
        }

        Perfis = await _repo.ListarTodosAsync();
        return Page();
      }

      public async Task<IActionResult> OnPostDeletarAsync(int id)
        {
            var claimPerfis =
                User.FindFirst("Permissao_Perfis")?.Value;

            bool podeDeletar = false;

            if (!string.IsNullOrWhiteSpace(claimPerfis))
            {
                var permissoes = claimPerfis.Split(',');

                if (permissoes.Length > 3)
                {
                    podeDeletar =
                        permissoes[3].Trim().ToUpper() == "S";
                }
            }

            if (!podeDeletar)
            {
                TempData["Erro"] =
                    "Você não possui permissão para excluir.";

                return RedirectToPage();
            }

            try
            {
                await _repo.DeletarAsync(id);

                TempData["Sucesso"] =
                    "Perfil excluído com sucesso.";
            }
            catch (Exception ex)
            {
                TempData["Erro"] =
                    "Erro ao excluir perfil: Verifique se não existem usuarios vinculados a esse perfil";
            }

            return RedirectToPage();  
        }
    }
}