namespace twoday_Internship_Task.Database.DatabaseModels
{
    public class EnclosureObject
    {
        public Guid Id { get; set; }

        public required string Name { get; set; }

        public string EnclosureName { get; set; } = string.Empty;
    }
}
