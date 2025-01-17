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
        //var produce = producer.ProduceAsync(
        //    "Foos",
        //    new Message<string, Bar>
        //    {
        //        Key = "Yes",
        //        Value = new Bar
        //        {
        //            Id = "yes",
        //            Bar1 = "yes",
        //            Bar5 =
        //            [
        //                new Bar5
        //                {
        //                    Id = "yes",
        //                    Bar7 = "yes",
        //                    Bar8 = "yes",
        //                    Bar13 = new Avro.AvroDecimal(new BigInteger(0), 2),
        //                    Bar15  = new(),
        //                },
        //            ]
        //        },
        //    });

        //var produce = producer.ProduceAsync(
        //    "Foos",
        //    new Message<string, Bar5>
        //    {
        //        Key = "Yes",
        //        Value = new Bar5
        //        {
        //            Id = "yes",
        //            Bar7 = "yes",
        //            Bar8 = "yes",
        //            Bar13 = new Avro.AvroDecimal(new BigInteger(0), 2),
        //            Bar15  = new(),
        //        },
        //    });

        var produce = producer.ProduceAsync(
            "Foos",
            new Message<string, Bar15>
            {
                Key = "Yes",
                Value = new Bar15(),
            });

        //var produce = producer.ProduceAsync(
        //    "Foos",
        //    new Message<string, Foo>
        //    {
        //        Key = "Yes",
        //        Value = new Foo()
        //        {
        //            Bar = "behf",
        //        },
        //    });

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
                Key = "Yes",
                Value = null!,
            });
}
