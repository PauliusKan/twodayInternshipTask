using Microsoft.AspNetCore.Mvc;
using twoday_Internship_Task.Models;
using twoday_Internship_Task.Services.Interfaces;

namespace twoday_Internship_Task.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class AnimalsController : ControllerBase
    {
        private readonly IAnimalsService _animalsService;

        public AnimalsController(IAnimalsService animalsService)
        {
            this._animalsService = animalsService;
        }

        [HttpPost]
        [Route("AddAnimals")]
        public async Task<IEnumerable<AnimalModel>> AddAnimalsAsync(AnimalsJsonModel animals) =>
            await _animalsService.AddAnimalsAsync(animals);

        [HttpDelete]
        [Route("DeleteAnimals")]
        public async Task DeleteAnimalsAsync(string species, int amount) =>
            await _animalsService.DeleteAnimalsAsync(species.Trim().ToLower(), amount);
    }
}
