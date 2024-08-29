namespace insurance.Models
{
    public class DataTransferModel
    {
        public object? Data { get; set; }   //token model will be stored here
        public string? Message { get; set; }
        public bool  Success { get; set; }
    }
}
