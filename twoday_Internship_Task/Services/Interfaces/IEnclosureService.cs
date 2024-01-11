using twoday_Internship_Task.DtoModels;
using twoday_Internship_Task.Models;

namespace twoday_Internship_Task.Services.Interfaces
{
    public interface IEnclosureService
    {
        Task<IEnumerable<EnclosureGETModel>> AddEnclosuresAsync(EnclosuresJsonModel enclosures);

        Task<IEnumerable<EnclosureGETModel>> GetAllEnclosuresAsync();

        Task DeleteEnclosureAsync(string enclosureName);
    }
}
