namespace LendingPlatform
{
    public class LoanApplicationResult
    {
        public LoanApplicationResult(LoanApplication application, bool result)
        {
            Application = application;
            Result = result;
        }

        public LoanApplication Application { get; }
        public bool Result { get; }
    }
}