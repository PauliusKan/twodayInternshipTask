namespace twoday_Internship_Task.Exceptions
{
    public class NoEnclosuresException : Exception
    {
        private const string exceptionMessage = "No enclosures were found";

        public NoEnclosuresException() : base(exceptionMessage) { }
    }
}
