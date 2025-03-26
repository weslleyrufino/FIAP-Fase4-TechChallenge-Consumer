using GestorContatos.Core.Entities;
using MassTransit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GestorContatos.WorkerService.Eventos;
public class ContatoCriadoConsumidor : IConsumer<Contato>
{
    public Task Consume(ConsumeContext<Contato> context)
    {
        Console.WriteLine(context.Message);
        return Task.CompletedTask;
    }
}
