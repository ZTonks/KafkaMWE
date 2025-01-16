using Azure.Identity;
using Confluent.Kafka;
using Microsoft.Azure.Functions.Worker.Builder;
using Microsoft.Azure.Kafka.SchemaRegistry.Avro;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System.Net;
using Test;

var builder = FunctionsApplication.CreateBuilder(args);

builder.ConfigureFunctionsWebApplication();

builder.Configuration.AddEnvironmentVariables();

builder.Services
    .AddLogging(lb => lb.AddConsole())
    .AddSingleton(sp =>
    {
        var configuration = sp.GetRequiredService<IConfiguration>();

        var producerConfig = new ProducerConfig
        {
            BootstrapServers = configuration.GetValue<string>("BootstrapServers"),
            ClientId = Dns.GetHostName(),
            Acks = Acks.All,
            Partitioner = Partitioner.Murmur2Random,
            SecurityProtocol = SecurityProtocol.Plaintext,
        };

        var producerBuilder = new ProducerBuilder<string, string>(producerConfig);
        var baseProducer = producerBuilder.Build();

        var schemaRegistryUrl = configuration.GetValue<string>("SchemaRegistryUrl");
        var schemaGroup = configuration.GetValue<string>("SchemaGroup");

        return new DependentProducerBuilder<string, Foo>(baseProducer.Handle)
            .SetValueSerializer(
                new KafkaAvroAsyncSerializer<Foo>(
                    schemaRegistryUrl,
                    new DefaultAzureCredential(new DefaultAzureCredentialOptions
                    {
                        ExcludeEnvironmentCredential = true,
                        ExcludeWorkloadIdentityCredential = true,
                        ExcludeManagedIdentityCredential = true,
                    }),
                    schemaGroup))
            .Build();
    })
    .AddSingleton(sp =>
    {
        var configuration = sp.GetRequiredService<IConfiguration>();

        var schemaRegistryUrl = configuration.GetValue<string>("SchemaRegistryUrl");

        return new KafkaAvroDeserializer<Foo>(
            schemaRegistryUrl,
            new DefaultAzureCredential(new DefaultAzureCredentialOptions
            {
                ExcludeEnvironmentCredential = true,
                ExcludeWorkloadIdentityCredential = true,
                ExcludeManagedIdentityCredential = true,
            }));
    });

builder
    .Build()
    .Run();
