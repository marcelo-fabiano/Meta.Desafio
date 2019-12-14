using Meta.Desafio.CRUD.Model;
using Meta.Desafio.CRUD.Service.Interface;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Meta.Desafio.CRUD.Pages.Audience
{
   public class IndexModel : PageModel
   {
      private readonly IAudienceService _audienceService;
      private readonly IStationService _stationService;

      public IndexModel(IAudienceService audienceService, IStationService stationService)
      {
         _audienceService = audienceService;
         _stationService = stationService;
      }

      public IList<AudienceModel> Audience { get; set; }

      public async Task OnGetAsync()
      {
         IList<StationModel> listaEmissoras = await _stationService.GetListAsync();
         Audience = await _audienceService.GetListAsync();

         foreach (var item in Audience)
         {
            item.Emissora_Nome = listaEmissoras.FirstOrDefault(x => x.Id == item.Emissora_Audiencia)?.Nome;
         }
      }
   }
}