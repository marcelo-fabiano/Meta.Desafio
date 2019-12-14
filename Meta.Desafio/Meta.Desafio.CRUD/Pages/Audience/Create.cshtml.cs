using Meta.Desafio.CRUD.Model;
using Meta.Desafio.CRUD.Service.Interface;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Meta.Desafio.CRUD.Pages.Audience
{
   public class CreateModel : PageModel
   {
      private readonly IAudienceService _audienceService;
      private readonly IStationService _stationService;

      [BindProperty]
      public AudienceModel Audience { get; set; }

      public IList<StationModel> ListaEmissoras { get; set; }

      public CreateModel(IAudienceService audienceService, IStationService stationService)
      {
         _audienceService = audienceService;
         _stationService = stationService;
      }

      public IActionResult OnGet()
      {
         ListaEmissoras = _stationService.GetList().OrderBy(o => o.Nome).ToList();
         return Page();
      }

      public async Task<IActionResult> OnPostAsync()
      {
         if (!ModelState.IsValid) return Page();

         var exists = await _audienceService.GetListAsync();

         if (exists.Any(x => x.Emissora_Audiencia == Audience.Emissora_Audiencia && x.Data_Hora_Audiencia == Audience.Data_Hora_Audiencia)) return BadRequest("Já existe uma audiência cadastrada para essa emissora nesse dia/hora!");

         await _audienceService.InsertRegistryAsync(Audience);

         return RedirectToPage("./Index");
      }
   }
}