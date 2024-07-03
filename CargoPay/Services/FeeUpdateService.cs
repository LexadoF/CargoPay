using CargoPay.Data;

namespace CargoPay.Services
{
    public class FeeUpdateService : IHostedService, IDisposable
    {
        private Timer _timer;
        private readonly IServiceScopeFactory _scopeFactory;

        public FeeUpdateService(IServiceScopeFactory scopeFactory)
        {
            _scopeFactory = scopeFactory;
        }
        public Task StartAsync(CancellationToken cancellationToken)
        {
            _timer = new Timer(UpdateFee, null, TimeSpan.Zero, TimeSpan.FromHours(1));
            return Task.CompletedTask;
        }
        private void UpdateFee(object state)
        {
            using IServiceScope scope = _scopeFactory.CreateScope();
            DbSource context = scope.ServiceProvider.GetRequiredService<DbSource>();
            FeeService.Instance.UpdateFee(context);
        }
        public Task StopAsync(CancellationToken cancellationToken)
        {
            _timer?.Change(Timeout.Infinite, 0);
            return Task.CompletedTask;
        }
        public void Dispose()
        {
            _timer?.Dispose();
        }
    }
}
