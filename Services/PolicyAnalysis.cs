
namespace insurance.Services
{
    public class PolicyAnalysis
    {
        public PolicyAnalysis()
        {
        }

        public int? PolicyID { get; set; }
        public string? PolicyName { get; set; }
        public string? UserName { get; set; }
        public decimal? Amount { get; set; }
        public string? PolicyType { get; set; }
        public DateTime SoldOn { get; set; }
    }
}