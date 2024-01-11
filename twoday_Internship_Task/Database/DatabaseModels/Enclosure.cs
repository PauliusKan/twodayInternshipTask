using System.ComponentModel.DataAnnotations;
using twoday_Internship_Task.Database.DatabaseModels.Enums;

namespace twoday_Internship_Task.Database.DatabaseModels
{
    public class Enclosure
    {
        [Key]
        public string Name { get; set; } = string.Empty;

        public Size Size { get; set; }

        public Location Location { get; set; }

        public List<EnclosureObject> Objects { get; set; } = [];

        public List<Animal> Animals { get; set; } = [];
    }
}
