namespace insurance.Models
{
    public class filterOption
    {
        public int? PolicyId { get; set; }
        public string? PolicyType { get; set; }
        public string? PolicyName { get; set; }
        public string? UserName { get; set; }
        public string? CustomerName { get; set; }
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set;}

    }
}
