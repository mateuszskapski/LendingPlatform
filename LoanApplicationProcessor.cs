namespace LendingPlatform
{
    public class LoanApplicationProcessor
    {
        public LoanApplicationResult ProcessLoanApplication(LoanApplication application)
        {
            var result = false;
            // Add rules

            return new LoanApplicationResult(application, result);
        }
    }
}