using Confluent.Kafka;
using Microsoft.Azure.Functions.Worker;
using System.Numerics;
using Test;

namespace KafkaMWE;

public class ProducerFunction(
    IProducer<string, Bar15> producer)
{
    [Function(nameof(Produce))]
    public Task Produce(
        [TimerTrigger(
            "0 */1 * * * *",
            RunOnStartup = true)] TimerInfo myTimer)
    {
        var produce = producer.ProduceAsync(
            "Foos",
            new Message<string, Bar15>
            {
                Key = "Key",
                Value = new Bar15
                {
                    Bar16 = 40,
                    Bar17 = 4,
                    Bar23 = new DateTime(1995, 5, 17),
                },
            });

        var tasks = Enumerable
            .Range(0, 0)
            .Select(_ => ProduceTombstone());

        return Task.WhenAll([produce, .. tasks]);
    }

    private Task<DeliveryResult<string, Bar15>> ProduceTombstone() =>
        producer.ProduceAsync(
            "Foos",
            new Message<string, Bar15>
            {
                Key = "Key",
                Value = null!,
            });
}
