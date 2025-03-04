namespace Async2;

public class Worker : BackgroundService
{
    private readonly ILogger<Worker> _logger;

    private readonly IHostApplicationLifetime _appLifetime;

    private readonly bool _asyncVoid;  // async void メンバーを登録
    private readonly bool _error;  // error を発生させる

    public Worker(ILogger<Worker> logger, IHostApplicationLifetime appLifetime)
    {
        _logger = logger;
        _appLifetime = appLifetime;
        var args = Environment.GetCommandLineArgs();

        _asyncVoid = args.Contains("void");
        _error = args.Contains("error");
    }

    public override Task StartAsync(CancellationToken cancellationToken)
    {
        _appLifetime.ApplicationStarted.Register(() => Task.Run(Exec1, cancellationToken));
        if (_asyncVoid) {
            _appLifetime.ApplicationStarted.Register(Exec2);
        }
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
        try {

            _logger.LogInformation("Exec1: hello");
            await Task.Delay(1000);
            if (_error) {
                throw new Exception("Exec1: error");
            }
            _logger.LogInformation("Exec1: bye");


        } catch (Exception ex) {
            _logger.LogError(ex.ToString());
            throw;
        }
    }

    protected async void Exec2()
    {
        try {

            _logger.LogInformation("Exec2: hello");
            await Task.Delay(1000);
            if (_error) {
                throw new Exception("Exec2: error");
            }
            _logger.LogInformation("Exec2: bye");


        } catch (Exception ex) {
            _logger.LogError(ex.ToString());
            throw;
        }
    }
}
