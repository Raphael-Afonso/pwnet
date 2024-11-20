using PwNet.Application.Dto;
using PwNet.Application.Events;
using PwNet.Application.Events.Login;
using PwNet.Application.Events.Server;
using PwNet.Application.Handlers;
using PwNet.Application.Handlers.Login;
using PwNet.Application.Handlers.Server;
using PwNet.Application.Interfaces.Events;
using PwNet.Application.Interfaces.Services;
using PwNet.Application.Interfaces.UseCases;
using PwNet.Application.Services;
using PwNet.Application.UseCases.MessageExchange;
using PwNet.Application.UseCases.PlayerFeatures;
using PwNet.Server;
using PwNet.Infra.Persistence;
using PwNet.Infra.Cryptography;
using PwNet.Common.Configurations;
using PwNet.Application.UseCases.LoginFlow;

var builder = Host.CreateApplicationBuilder(args);
builder.Services.AddHostedService<Worker>();

builder.Services
    .AddSingleton<IPlayerMessageListener, PlayerMessageListener>()
    .AddSingleton<IEventDispatcher, EventDispatcher>()
    .AddSingleton<ISessionsManager, SessionsManager>()

    // EventHandlers
    .AddSingleton<IEventHandler<PlayerMessageContext>, MessageFactoryHandler>()
    .AddSingleton<IEventHandler<PlayerConnectedEvent>, SendServerChallengeHandler>()
    .AddSingleton<IEventHandler<LoginRequestedEvent>, AuthenticationHandler>()
    .AddSingleton<IEventHandler<LoginFailedEvent>, SendAuthenticationErrorHandler>()
    .AddSingleton<IEventHandler<LoginSuccessfulEvent>, SendAuthenticationSuccessfulHandler>()

    // UseCases
    .AddSingleton<IReadMessageUseCase, ReadMessageUseCase>()
    .AddSingleton<IWriteMessageUseCase, WriteMessageUseCase>()

    .AddSingleton<IAuthenticatePlayerUseCase, AuthenticatePlayerUseCase>()
    .AddSingleton<ICheckIpBlockUseCase, CheckIpBlockUseCase>()
    .AddSingleton<IBuildCryptographyUseCase, BuildCryptographyUseCase>()

    // Infra
    .AddCryptographyServices()
    .AddSqlPersistence()
    ;

builder.Services
    .AddOptions<ServerConfig>()
    .BindConfiguration(nameof(ServerConfig));


var host = builder.Build();
host.Run();