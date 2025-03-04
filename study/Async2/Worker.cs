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
        _appLifetime.ApplicationStarted.Register(() => Task.Run(Exec, cancellationToken));
        _appLifetime.ApplicationStarted.Register(() => Task.Run(ExecWithoutErrorHandling, cancellationToken));
        if (_asyncVoid) {
            _appLifetime.ApplicationStarted.Register(AsyncVoidExec);
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


    protected async Task Exec()
    {
        const string name = "Exec";
        try {
            await Task.Delay(1000);
            _logger.LogInformation($"{name}: hello");
            await Task.Delay(1000);
            if (_error) {
                throw new Exception($"{name}: error");
            }
            _logger.LogInformation($"{name}: bye");


        } catch (Exception ex) {
            _logger.LogError(ex.ToString());
            throw;
        }
    }

    protected async Task ExecWithoutErrorHandling()
    {
        const string name = "ExecWithoutErrorHandling";

        await Task.Delay(1300);
        _logger.LogInformation($"{name}: hello");
        await Task.Delay(1000);
        if (_error) {
            throw new Exception($"{name}: error");
        }
        _logger.LogInformation($"{name}: bye");
    }

    protected async void AsyncVoidExec()
    {
        const string name = "AsyncVoidExec";

        await Task.Delay(700);
        _logger.LogInformation($"{name}: hello");
        await Task.Delay(1000);
        if (_error) {
            throw new Exception($"{name}: error");
        }
        _logger.LogInformation($"{name}: bye");
    }
}
