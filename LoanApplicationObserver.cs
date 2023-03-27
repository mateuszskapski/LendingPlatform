namespace LendingPlatform
{
    public class LoanApplicationObserver
    {
        private readonly LoanApplicationStorageClient _storageClient;

        public LoanApplicationObserver(LoanApplicationStorageClient storageClient)
        {
            _storageClient = storageClient;
        }
        
        // loanDecision argument is a simplified concept. It should be extended to an object providing more details. 
        public void OnDecisionReady(LoanApplication application, bool loanDecision) =>
            _storageClient.Add(new LoanApplicationResult(application, loanDecision));
    }
}