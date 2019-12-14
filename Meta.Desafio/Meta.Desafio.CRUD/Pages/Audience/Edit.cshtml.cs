using Meta.Desafio.CRUD.Model;
using Meta.Desafio.CRUD.Service.Interface;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Meta.Desafio.CRUD.Pages.Audience
{
   public class EditModel : PageModel
   {
      private readonly IAudienceService _audienceService;
      private readonly IStationService _stationService;

      [BindProperty]
      public AudienceModel Audience { get; set; }

      public IList<StationModel> ListaEmissoras { get; set; }


      public EditModel(IAudienceService audienceService, IStationService stationService)
      {
         _audienceService = audienceService;
         _stationService = stationService;
      }

      public async Task<IActionResult> OnGetAsync(string id)
      {
         if (id == null) return NotFound();

         ListaEmissoras = _stationService.GetList().OrderBy(o => o.Nome).ToList();
         Audience = await _audienceService.GetAsync(id);

         if (Audience == null) return NotFound();

         return Page();
      }

      public async Task<IActionResult> OnPostAsync()
      {
         if (!ModelState.IsValid) return Page();

         try
         {
            await _audienceService.UpdateRegistryAsync(Audience.Id, Audience);
         }
         catch (DbUpdateConcurrencyException)
         {
            if (!AudienceExists(Audience.Id))
            {
               return NotFound();
            }
            else
            {
               throw;
            }
         }

         return RedirectToPage("./Index");
      }

      private bool AudienceExists(string id)
      {
         return _audienceService.GetList().Any(x => x.Id == id);
      }
   }
}