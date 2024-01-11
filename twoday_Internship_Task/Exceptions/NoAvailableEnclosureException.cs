namespace twoday_Internship_Task.Exceptions
{
    public class NoAvailableEnclosureException : Exception
    {
        private const string exceptionMessage = "No available enclosure was found for given animal species: ";

        public NoAvailableEnclosureException() : base() { }

        public NoAvailableEnclosureException(string species) : base(exceptionMessage + species) { }

        public NoAvailableEnclosureException(string species, Exception innerException)
            : base(exceptionMessage + species, innerException) { }
    }
}
