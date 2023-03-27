namespace LendingPlatform
{
    public class LoanProcessingService
    {
        private readonly MockServiceBusClient _serviceBus;
        private readonly LoanApplicationProcessor _processor;

        private List<LoanApplicationObserver> _observers = new List<LoanApplicationObserver>();

        public LoanProcessingService(MockServiceBusClient serviceBus, LoanApplicationProcessor processor)
        {
            _serviceBus = serviceBus;
            _processor = processor;
        }

        public void Subscribe(LoanApplicationObserver observer)
        {
            _observers.Add(observer);
        }

        public async Task ProcessApplicationsAsync(CancellationToken stoppingToken)
        {
            await foreach (var application in _serviceBus.ReadAllAsync(stoppingToken))
            {
                var result = _processor.ProcessLoanApplication(application);
                
                foreach(var observer in _observers)
                {
                    observer.OnDecisionReady(application, result.Result);
                }
            }
        }
    }
}