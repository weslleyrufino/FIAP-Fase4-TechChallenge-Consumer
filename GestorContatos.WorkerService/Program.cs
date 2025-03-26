using GestorContatos.WorkerService;
using GestorContatos.WorkerService.Eventos;
using MassTransit;

var builder = Host.CreateApplicationBuilder(args);

var configurationMassTransit = builder.Configuration;
var fila = configurationMassTransit.GetSection("MassTransit_InserirContato")["NomeFila"] ?? string.Empty;
var fila_alterar_contato = configurationMassTransit.GetSection("MassTransit_AlterarContato")["NomeFila"] ?? string.Empty;
var servidor = configurationMassTransit.GetSection("MassTransit_InserirContato")["Servidor"] ?? string.Empty;
var usuario = configurationMassTransit.GetSection("MassTransit_InserirContato")["Usuario"] ?? string.Empty;
var senha = configurationMassTransit.GetSection("MassTransit_InserirContato")["Senha"] ?? string.Empty;

builder.Services.AddMassTransit(x =>
{
    x.UsingRabbitMq((context, cfg) =>
    {
        cfg.Host(servidor, "/", h =>
        {
            h.Username(usuario);
            h.Password(senha);
        });

        cfg.ReceiveEndpoint(fila, e =>
        {
            e.Consumer<ContatoCriadoConsumidor>();
        });

        cfg.ConfigureEndpoints(context);
    });

    x.AddConsumer<ContatoCriadoConsumidor>();
});


builder.Services.AddHostedService<Worker>();

var host = builder.Build();
host.Run();
