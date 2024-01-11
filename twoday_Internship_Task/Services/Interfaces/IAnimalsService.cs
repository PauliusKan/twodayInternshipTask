using twoday_Internship_Task.Models;

namespace twoday_Internship_Task.Services.Interfaces
{
    public interface IAnimalsService
    {
        Task<IEnumerable<AnimalModel>> AddAnimalsAsync(AnimalsJsonModel animals);

        Task DeleteAnimalsAsync(string species, int amount);
    }
}
