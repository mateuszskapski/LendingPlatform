namespace LendingPlatform
{
    public class LoanApplication
    {
        public LoanApplication(decimal loanAmount, decimal assetsValue, int creditScore)
        {
            ApplicationId = Guid.NewGuid();
            LoanAmount = loanAmount;
            AssetsValue = assetsValue;
            CreditScore = creditScore;
        }

        public Guid ApplicationId { get; }
        public decimal LoanAmount { get; }
        public decimal AssetsValue { get; }
        public int CreditScore { get; }
    }
}