using Meta.Desafio.CRUD.Model;
using Meta.Desafio.CRUD.Service.Interface;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;

namespace Meta.Desafio.CRUD.Pages.Station
{
   public class EditModel : PageModel
    {
        private readonly IStationService _service;

        public EditModel(IStationService service)
        {
            _service = service;
        }

        [BindProperty]
        public StationModel Station { get; set; }

        public async Task<IActionResult> OnGetAsync(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Station = await _service.GetAsync(id);

            if (Station == null)
            {
                return NotFound();
            }

            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            try
            {
                await _service.UpdateRegistryAsync(Station.Id, Station);
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!StationExists(Station.Id))
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

        private bool StationExists(string id)
        {
            return _service.GetList().Any(x => x.Id == id);
        }
    }
}