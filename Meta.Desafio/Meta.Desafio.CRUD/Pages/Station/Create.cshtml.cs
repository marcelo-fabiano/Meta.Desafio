using Meta.Desafio.CRUD.Model;
using Meta.Desafio.CRUD.Service.Interface;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Threading.Tasks;

namespace Meta.Desafio.CRUD.Pages.Station
{
   public class CreateModel : PageModel
   {
      private readonly IStationService _service;

      [BindProperty]
      public StationModel Station { get; set; }

      public CreateModel(IStationService service)
      {
         _service = service;
      }

      public IActionResult OnGet()
      {
         return Page();
      }

      public async Task<IActionResult> OnPostAsync()
      {
         if (!ModelState.IsValid) return Page();

         var exists = await _service.ExistsAsync("Nome", Station.Nome);

         if (exists) return BadRequest("O nome informado já existe!");

         await _service.InsertRegistryAsync(Station);

         return RedirectToPage("./Index");
      }
   }
}