namespace LendingPlatform
{
    public class LoanApplicationSubmitter
    {
        private readonly MockServiceBusClient _serviceBusClient;
        
        public LoanApplicationSubmitter(MockServiceBusClient serviceBusClient)
        {
            _serviceBusClient = serviceBusClient;
        }

        public async Task SubmitApplicationAsync(decimal loanAmount, decimal assetsValue, int creditScore)
        {
            var isSent = await _serviceBusClient.SendMessageAsync(new LoanApplication(loanAmount, assetsValue, creditScore));

            if (!isSent)
            {
                // Introduce a retry policy
            }
        }
    }
}