using System.Threading.Channels;
namespace LendingPlatform
{
    public class MockServiceBusClient
    {
        private readonly Channel<LoanApplication> _channel;

        public MockServiceBusClient()
        {
            _channel = Channel.CreateUnbounded<LoanApplication>();
        }

        public async ValueTask<bool> SendMessageAsync(LoanApplication request, CancellationToken ct = default(CancellationToken))
        {
            while (await _channel.Writer.WaitToWriteAsync(ct) && !ct.IsCancellationRequested)
            {
                if (_channel.Writer.TryWrite(request))
                {
                    return true;
                }
            }

            return false;
        }

        public IAsyncEnumerable<LoanApplication> ReadAllAsync(CancellationToken ct = default(CancellationToken))
        {
            return _channel.Reader.ReadAllAsync(ct);
        }
    }
}