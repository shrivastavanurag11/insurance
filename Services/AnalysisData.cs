
namespace insurance.Services
{
    public class AnalysisData
    {
        public AnalysisData()
        {
        }

        public string UserName { get; set; }
        public string PolicyName { get; set; }
        public DateTime PurchasedOn { get; set; }
        public decimal Amount { get; set; }
    }
}