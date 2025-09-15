using CosmenticFormulaApp.Application.Formulas.Commands.ProcessFolderImport;
using CosmenticFormulaApp.Infrastructure.Data.Configuration;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CosmenticFormulaApp.Infrastructure.Services
{
    public class FolderWatcherService : IHostedService, IDisposable
    {
        private readonly IServiceProvider _serviceProvider;
        private readonly ILogger<FolderWatcherService> _logger;
        private readonly ImportSettings _settings;
        private FileSystemWatcher _fileWatcher;
        private Timer _backupTimer;

        public FolderWatcherService(
            IServiceProvider serviceProvider,
            ILogger<FolderWatcherService> logger,
            IOptions<ImportSettings> settings)
        {
            _serviceProvider = serviceProvider;
            _logger = logger;
            _settings = settings.Value;
        }

        public Task StartAsync(CancellationToken cancellationToken)
        {
            if (!Directory.Exists(_settings.WatchFolder))
            {
                _logger.LogWarning("Watch folder does not exist: {WatchFolder}", _settings.WatchFolder);
                return Task.CompletedTask;
            }

            _fileWatcher = new FileSystemWatcher(_settings.WatchFolder)
            {
                Filter = _settings.FilePattern,
                EnableRaisingEvents = true,
                IncludeSubdirectories = false
            };

            _fileWatcher.Created += OnFileCreated;
            _fileWatcher.Changed += OnFileChanged;
            _fileWatcher.Error += OnError;

            _backupTimer = new Timer(ScanFolder, null, TimeSpan.Zero, TimeSpan.FromMilliseconds(_settings.ProcessingInterval));

            _logger.LogInformation("Folder watcher started for: {WatchFolder}", _settings.WatchFolder);
            return Task.CompletedTask;
        }

        public Task StopAsync(CancellationToken cancellationToken)
        {
            _fileWatcher?.Dispose();
            _backupTimer?.Dispose();
            _logger.LogInformation("Folder watcher stopped");
            return Task.CompletedTask;
        }

        private async void OnFileCreated(object sender, FileSystemEventArgs e)
        {
            await ProcessFile(e.FullPath);
        }

        private async void OnFileChanged(object sender, FileSystemEventArgs e)
        {
            await Task.Delay(1000); // Debounce
            await ProcessFile(e.FullPath);
        }

        private void OnError(object sender, ErrorEventArgs e)
        {
            _logger.LogError(e.GetException(), "File system watcher error");
        }

        private async void ScanFolder(object state)
        {
            try
            {
                if (!Directory.Exists(_settings.WatchFolder))
                    return;

                var files = Directory.GetFiles(_settings.WatchFolder, _settings.FilePattern);
                if (files.Length > 0)
                {
                    _logger.LogInformation("Found {FileCount} files to process", files.Length);

                    using var scope = _serviceProvider.CreateScope();
                    var mediator = scope.ServiceProvider.GetRequiredService<IMediator>();

                    var command = new ProcessFolderImportCommand { FilePaths = files.ToList() };
                    var result = await mediator.Send(command);

                    if (result.IsSuccess)
                    {
                        foreach (var filePath in files)
                        {
                            try
                            {
                                File.Delete(filePath);
                            }
                            catch (Exception ex)
                            {
                                _logger.LogWarning(ex, "Failed to delete processed file: {FilePath}", filePath);
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error during folder scan");
            }
        }

        private async Task ProcessFile(string filePath)
        {
            try
            {
                await Task.Delay(500); // Wait for file to be fully written

                if (!File.Exists(filePath))
                    return;

                using var scope = _serviceProvider.CreateScope();
                var mediator = scope.ServiceProvider.GetRequiredService<IMediator>();

                var command = new ProcessFolderImportCommand { FilePaths = new List<string> { filePath } };
                var result = await mediator.Send(command);

                if (result.IsSuccess)
                {
                    File.Delete(filePath);
                    _logger.LogInformation("Successfully processed and deleted file: {FilePath}", filePath);
                }
                else
                {
                    _logger.LogWarning("Failed to process file: {FilePath}", filePath);
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error processing file: {FilePath}", filePath);
            }
        }
        public void Dispose()
        {
            _fileWatcher?.Dispose();
            _backupTimer?.Dispose();
        }
    }
}
