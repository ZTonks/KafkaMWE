using Confluent.Kafka;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Azure.Kafka.SchemaRegistry.Avro;
using Microsoft.Extensions.Logging;
using System.Text;
using System.Text.Json;
using Test;

namespace KafkaMWE;

public class ConsumerFunction(
    ILogger<ConsumerFunction> logger,
    KafkaAvroDeserializer<Bar15> kafkaAvroDeserializer)
{
    [Function(nameof(ConsumerBatchedStrings))]
    public void ConsumerBatchedStrings(
        [KafkaTrigger(
            brokerList: "BootstrapServers",
            topic: "Foos",
            ConsumerGroup = "$Default1",
            IsBatched = true)] string[] events,
        FunctionContext functionContext)
    {
        var kafkaDatas = events.Select(ev => JsonSerializer.Deserialize<KafkaData>(ev)!);

        foreach (var kafkaData in kafkaDatas)
        {
            var kafkaHeaders = kafkaData.Headers!
                .Select(kh => new Header(kh.Key, Convert.FromBase64String(kh.Value)));

            var context = new SerializationContext(
                component: MessageComponentType.Value,
                topic: default,
                headers: [.. kafkaHeaders]);

            if (kafkaData.Value is not null)
            {
                var foo = kafkaAvroDeserializer.Deserialize(
                    Encoding.UTF8.GetBytes(kafkaData.Value),
                    isNull: false,
                    context);

                var json = JsonSerializer.Serialize(foo);

                logger.LogInformation(json.ToString());
            }
            else
            {
                logger.LogInformation($"tombstone event {kafkaData.Key}");
            }
        }
    }

    //[Function(nameof(ConsumerBatchedBytes))]
    //public void ConsumerBatchedBytes(
    //    [KafkaTrigger(
    //        brokerList: "BootstrapServers",
    //        topic: "Foos",
    //        ConsumerGroup = "$Default2",
    //        IsBatched = true)] byte[][] events,
    //    FunctionContext functionContext)
    //{
    //    var i = 0;
    //    var headersArray = functionContext.BindingContext.BindingData["HeadersArray"];
    //    var headers = JsonSerializer.Deserialize<IEnumerable<IEnumerable<KeyValuePair<string, string>>>>(
    //        headersArray!.ToString()!)!;

    //    foreach (var @event in events)
    //    {
    //        var headersThisEvent = headers.Skip(i++).First();

    //        var kafkaHeaders = headersThisEvent
    //            .Select(kh => new Header(kh.Key, Convert.FromBase64String(kh.Value)));

    //        var context = new SerializationContext(
    //            component: MessageComponentType.Value,
    //            topic: default,
    //            headers: [.. kafkaHeaders]);

    //        var foo = kafkaAvroDeserializer.Deserialize(
    //            @event,
    //            isNull: false,
    //            context);

    //        logger.LogInformation(foo.ToString());
    //    }
    //}


    //[Function(nameof(ConsumerSingle))]
    //public void ConsumerSingle(
    //    [KafkaTrigger(
    //        brokerList: "BootstrapServers",
    //        topic: "Foos",
    //        ConsumerGroup = "$Default3",
    //        IsBatched = false)] byte[] @event,
    //    IEnumerable<KeyValuePair<string, string>> headers,
    //    FunctionContext functionContext)
    //{
    //    var context = new SerializationContext(
    //        component: MessageComponentType.Value,
    //        topic: default,
    //        headers: [ .. headers.Select(header => new Header(header.Key, Convert.FromBase64String(header.Value)))]);

    //    var foo = kafkaAvroDeserializer.Deserialize(
    //        @event,
    //        isNull: false,
    //        context);

    //    if (foo is not null)
    //    {
    //        logger.LogInformation(foo.ToString());
    //    }
    //    else
    //    {
    //        logger.LogInformation("tombstone event");
    //    }
    //}
}
