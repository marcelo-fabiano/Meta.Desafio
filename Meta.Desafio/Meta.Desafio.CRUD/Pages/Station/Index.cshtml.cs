using Meta.Desafio.CRUD.Model;
using Meta.Desafio.CRUD.Service.Interface;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Meta.Desafio.CRUD.Pages.Station
{
   public class IndexModel : PageModel
    {
        private readonly IStationService _service;

        public IndexModel(IStationService service)
        {
            _service = service;
        }

        public IList<StationModel> Station { get;set; }

        public async Task OnGetAsync()
        {
            Station = await _service.GetListAsync();
        }
    }
}