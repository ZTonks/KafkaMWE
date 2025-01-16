using Confluent.Kafka;
using Microsoft.Azure.Functions.Worker;
using Test;

namespace KafkaMWE;

public class ProducerFunction(
    IProducer<string, Foo> producer)
{
    [Function(nameof(Produce))]
    public Task Produce(
        [TimerTrigger(
            "0 */1 * * * *",
            RunOnStartup = true)] TimerInfo myTimer)
    {
        var produce = producer.ProduceAsync(
            "Foos",
            new Message<string, Foo>
            {
                Key = "Yes",
                Value = new Foo
                {
                    Bar = "bar",
                    Baz = 1,
                },
            });

        var tasks = Enumerable
            .Range(0, 3)
            .Select(_ => ProduceTombstone());

        return Task.WhenAll([produce, .. tasks]);
    }

    private Task<DeliveryResult<string, Foo>> ProduceTombstone() =>
        producer.ProduceAsync(
            "Foos",
            new Message<string, Foo>
            {
                Key = "Yes",
                Value = null!,
            });
}
