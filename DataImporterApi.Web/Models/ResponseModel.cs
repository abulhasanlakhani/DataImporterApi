namespace DataImporterApi.Web.Models
{
    public class ResponseModel
    {
        public ValidationModel Validation { get; set; }
        public bool Success { get; set; }
    }

    public class ResponseModel<T> : ResponseModel
    {
        public T Payload { get; set; }
    }
}