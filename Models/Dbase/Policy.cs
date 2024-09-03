namespace insurance.Models.Dbase
{
    public class Policy
    {
        public int PolicyId { get; set; }
        public string PolicyNumber { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public int CustomerId { get; set; }
        public Customer Customer { get; set; }
        public object Name { get; internal set; }
        public object Phone { get; internal set; }
        public object Email { get; internal set; }
        public object Address { get; internal set; }
    }
}
