using System.ComponentModel.DataAnnotations;
using twoday_Internship_Task.Database.DatabaseModels.Enums;

namespace twoday_Internship_Task.Database.DatabaseModels
{
    public class Animal
    {
        [Key]
        public string Species { get; set; } = string.Empty;

        public AnimalDiet Food { get; set; }

        public int Amount { get; set; } = 0;

        public string EnclosureName { get; set; } = string.Empty;
    }
}
