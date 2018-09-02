namespace DataImporter.Domain
{
    public class Validation
    {
        public Validation(ValidationType type)
        {
            Type = type;
        }

        public string Message { get; set; }
        public ValidationType Type { get; set; }
    }

    public enum ValidationType
    {
        Info,
        Warning,
        Error
    }
}
