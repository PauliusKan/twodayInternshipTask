using Microsoft.AspNetCore.Mvc;
using twoday_Internship_Task.DtoModels;
using twoday_Internship_Task.Models;
using twoday_Internship_Task.Services.Interfaces;

namespace twoday_Internship_Task.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class EnclosuresController : ControllerBase
    {
        private readonly IEnclosureService _enclosureService;

        public EnclosuresController(IEnclosureService enclosureService)
        {
            this._enclosureService = enclosureService;
        }

        [HttpPost]
        [Route("AddNewEnclosures")]
        public async Task<IEnumerable<EnclosureGETModel>> AddEnclosuresAsync(EnclosuresJsonModel enclosures) =>
            await _enclosureService.AddEnclosuresAsync(enclosures);

        [HttpGet]
        [Route("GetAllEnclosures")]
        public async Task<IEnumerable<EnclosureGETModel>> GetAllEnclosuresAsync() => await _enclosureService.GetAllEnclosuresAsync();

        [HttpDelete]
        [Route("DeleteEnclosure")]
        public async Task DeleteEnclosureAsync(string enclosureName) => await _enclosureService.DeleteEnclosureAsync(enclosureName.ToLower().Trim());
    }
}
