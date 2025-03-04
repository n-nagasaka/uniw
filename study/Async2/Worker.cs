namespace Async2;

public class Worker : BackgroundService
{
    private readonly ILogger<Worker> _logger;

    private readonly IHostApplicationLifetime _appLifetime;

    public Worker(ILogger<Worker> logger, IHostApplicationLifetime appLifetime)
    {
        _logger = logger;
        _appLifetime = appLifetime;
    }

    public override Task StartAsync(CancellationToken cancellationToken)
    {
        _appLifetime.ApplicationStarted.Register(() => Task.Run(Exec1, cancellationToken));
        _appLifetime.ApplicationStarted.Register(Exec2);
        _logger.LogInformation("StartAsync");
        return Task.CompletedTask;
    }

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        while (!stoppingToken.IsCancellationRequested)
        {
            if (_logger.IsEnabled(LogLevel.Information))
            {
                _logger.LogInformation("Worker running at: {time}", DateTimeOffset.Now);
            }
            await Task.Delay(1000, stoppingToken);
        }
    }

    protected async Task Exec1()
    {
        _logger.LogInformation("Exec1: hello");
        await Task.Delay(1000);
        _logger.LogInformation("Exec1: bye");
    }

    protected async void Exec2()
    {
        _logger.LogInformation("Exec2: hello");
        await Task.Delay(1000);
        _logger.LogInformation("Exec2: bye");
    }
}
