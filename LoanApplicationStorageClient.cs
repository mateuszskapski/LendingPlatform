using System.Collections.Concurrent;
namespace LendingPlatform
{
    public class LoanApplicationStorageClient
    {
        private ConcurrentDictionary<Guid, LoanApplicationResult> _applicationResults = new ConcurrentDictionary<Guid, LoanApplicationResult>();

        public void Add(LoanApplicationResult applicationResult)
        {
            if (_applicationResults.Keys.Contains(applicationResult.Application.ApplicationId))
            {
                _applicationResults[applicationResult.Application.ApplicationId] = applicationResult;
            }
            else
            {
                _applicationResults.TryAdd(applicationResult.Application.ApplicationId, applicationResult);
            }
        }

        public Dictionary<bool, int> GetLoanStatistics() =>
            _applicationResults
                .Select(x => x.Value)
                .GroupBy(x => x.Result)
                .ToDictionary(x => x.Key, x => x.Select(y => y.Result).Count());
    }
}