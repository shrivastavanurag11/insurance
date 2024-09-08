namespace insurance.Models
{
    public class groupPolicyDetail
    {
        public int PolicyId { get; set; }
        public string PolicyName { get; set; }
        public string PolicyType { get; set; }
        public decimal InsuredAmount { get; set; }
        public int? numberOFClaims { get; set; }
        public decimal? totalClaimedAmount { get; set; }
        public DateTime? lastClaimDate { get; set; }
        public decimal? RemainingAmount { get; set; }
        public int PurchaseId { get; internal set; }
    }
}