using Meta.Desafio.CRUD.Model;
using Meta.Desafio.CRUD.Service.Interface;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Threading.Tasks;

namespace Meta.Desafio.CRUD.Pages.Station
{
   public class DeleteModel : PageModel
    {
        private readonly IStationService _service;

        public DeleteModel(IStationService service)
        {
            _service = service;
        }

        [BindProperty]
        public StationModel Station { get; set; }

        public async Task<IActionResult> OnGetAsync(string id)
        {
            if (id == null) return NotFound();

            Station = await _service.GetAsync(id);

            if (Station == null) return NotFound();

            return Page();
        }

        public async Task<IActionResult> OnPostAsync(string id)
        {
            if (id == null) return NotFound();

            Station = await _service.GetAsync(id);

            if (Station != null) await _service.DeleteRegistryAsync(id);

            return RedirectToPage("./Index");
        }
    }
}