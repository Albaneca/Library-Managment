using Library.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Library.Services.Services
{
    public class BanHostedService : IHostedService, IDisposable
    {
        private Timer _timer = null!;
        private readonly IServiceScopeFactory scopeFactory;

        public BanHostedService(IServiceScopeFactory scopeFactory)
        {
            this.scopeFactory = scopeFactory;
        }

        public Task StartAsync(CancellationToken stoppingToken)
        {
            _timer = new Timer(DoWork, null, TimeSpan.Zero,
                TimeSpan.FromDays(1));

            return Task.CompletedTask;
        }

#pragma warning disable CS8632 
        private async void DoWork(object? state)
        {
            using (var scope = scopeFactory.CreateScope())
            {
                var _db = scope.ServiceProvider.GetRequiredService<LibraryDbContext>();

                await _db.Users.Include(x => x.Ban)
                .Where(x => x.Ban.BlockedDue < DateTime.UtcNow.Date)
                .ForEachAsync(x => { x.Ban.BlockedOn = null; x.Ban.BlockedDue = null; x.ApplicationRoleId = 2; });

                await _db.SaveChangesAsync();
            }

        }
        public Task StopAsync(CancellationToken stoppingToken)
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
