using twoday_Internship_Task.Models;

namespace twoday_Internship_Task.DtoModels
{
    public class EnclosureGETModel : EnclosureModel
    {
        public List<AnimalModel> Animals { get; set; } = [];
    }
}
