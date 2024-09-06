namespace insurance.Models
{
    public class claimrecord
    {
        public claimrecord()
        {
        }

        public string? UserName { get; set; }
        public string? FirstName { get; set; }
        public int? PolicyId { get; set; }
        public DateTime? PurchasedON { get; set; }
        public decimal? Amount { get; set; }
        public decimal? ClaimedAmount { get; set; }
        public decimal? RemainingAmount { get; set; }
        public DateTime? ClaimDate { get; set; }
    }
}