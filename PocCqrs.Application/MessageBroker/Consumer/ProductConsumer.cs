using MassTransit;
using PocCqrs.Aplication.MessageBroker.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PocCqrs.Aplication.MessageBroker.Consumer
{
    public class ProductConsumer : IConsumer<ProductMessage>
    {
        public async Task Consume(ConsumeContext<ProductMessage> context)
        {
            var messageConsume =  context.Message;
        }
    }
}
