using Meta.Desafio.CRUD.Model;
using Meta.Desafio.CRUD.Service.Interface;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Threading.Tasks;

namespace Meta.Desafio.CRUD.Pages.Audience
{
   public class DeleteModel : PageModel
   {
      private readonly IAudienceService _audienceService;
      private readonly IStationService _stationService;

      [BindProperty]
      public AudienceModel Audience { get; set; }

      public DeleteModel(IAudienceService audienceService, IStationService stationService)
      {
         _audienceService = audienceService;
         _stationService = stationService;
      }

      public async Task<IActionResult> OnGetAsync(string id)
      {
         if (id == null) return NotFound();

         Audience = await _audienceService.GetAsync(id);

         if (Audience == null) return NotFound();

         Audience.Emissora_Nome = _stationService.Get(Audience.Emissora_Audiencia)?.Nome;

         return Page();
      }

      public async Task<IActionResult> OnPostAsync(string id)
      {
         if (id == null) return NotFound();

         Audience = await _audienceService.GetAsync(id);

         if (Audience != null) await _audienceService.DeleteRegistryAsync(id);

         return RedirectToPage("./Index");
      }
   }
}