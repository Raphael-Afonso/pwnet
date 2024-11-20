using PwNet.Application.Interfaces.Services;
using PwNet.Application.Services;
using System.Net;
using System.Net.Sockets;

namespace PwNet.Server
{
    public class Worker(IPlayerMessageListener playerMessageListener, ISessionsManager sessionsManager, ILogger<Worker> logger) : BackgroundService
    {
        private TcpListener? _listener;

        private const int _port = 29_000;

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            try
            {
                //TODO: Validate configs

                logger.LogWarning("[TCPWorker] Starting TCP server on port: {port}...", _port);

                _listener = new TcpListener(IPAddress.Any, _port);
                _listener.Start();

                logger.LogWarning("[TCPWorker] Started TCP server on port: {port}.", _port);

                while (!stoppingToken.IsCancellationRequested)
                {
                    var newClient = await _listener.AcceptTcpClientAsync(stoppingToken);

                    var session = new PlayerSession(newClient);

                    var sessionGuid = sessionsManager.Add(session);

                    _ = Task.Run(() => playerMessageListener.HandleClientAsync(session, stoppingToken), stoppingToken)
                        .ContinueWith((_) => sessionsManager.Remove(sessionGuid), stoppingToken);
                }
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "[TCPWorker] TCP server error.");
            }
            finally
            {
                logger.LogCritical("[TCPWorker] TCP server stopped on port: {port}.", _port);
                _listener?.Dispose();
            }
        }
    }
}
