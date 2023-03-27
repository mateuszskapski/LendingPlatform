namespace LendingPlatform
{
    public class LoanApplicationStorageClient
    {
        private Dictionary<Guid, LoanApplicationResult> _applicationResults = new Dictionary<Guid, LoanApplicationResult>();

        public void Add(LoanApplicationResult applicationResult)
        {
            if (_applicationResults.Keys.Contains(applicationResult.Application.ApplicationId))
            {
                _applicationResults[applicationResult.Application.ApplicationId] = applicationResult;
            }
            else
            {
                _applicationResults.Add(applicationResult.Application.ApplicationId, applicationResult);
            }
        }

        public Dictionary<bool, int> GetLoanStatistics() =>
            _applicationResults
                .Select(x => x.Value)
                .GroupBy(x => x.Result)
                .ToDictionary(x => x.Key, x => x.Select(y => y.Result).Count());
    }
}