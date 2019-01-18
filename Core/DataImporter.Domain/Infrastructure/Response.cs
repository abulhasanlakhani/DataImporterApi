namespace DataImporter.Domain.Infrastructure
{
    public class Response
    {
        public Response() { }

        public Response(Validation validation)
        {
            Validation = validation;
        }

        public bool Success => Validation == null || Validation.Type != ValidationType.Error;

        public Validation Validation { get; set; }
    }

    public class Response<T> : Response
    {
        public Response() { }

        public Response(Validation validation) : base(validation) { }

        public T Payload { get; set; }
    }
}
