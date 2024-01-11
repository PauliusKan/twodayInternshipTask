using twoday_Internship_Task.Database.DatabaseModels.Enums;

namespace twoday_Internship_Task.Models
{
    public class EnclosureModel
    {
        public string Name { get; set; } = string.Empty;

        public Size Size { get; set; }

        public Location Location { get; set; }

        public List<string> Objects { get; set; } = [];
    }
}
