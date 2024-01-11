using twoday_Internship_Task.Database.DatabaseModels.Enums;

namespace twoday_Internship_Task.Models
{
    public class AnimalModel
    {
        public string Species { get; set; } = string.Empty;

        public AnimalDiet Food { get; set; }

        public int amount { get; set; }
    }
}
