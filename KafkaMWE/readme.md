# Bug - Avro cannot be deserialized properly under KafkaTrigger using string[] (batched) overload

This repo provides a minimal (non) working example of KafkaTrigger providing data which cannot then be deserialized from Avro.

There are two overloads we are aware of for receiving a batch of messages from a KafkaTrigger for performance purposes, essentially `string[]` and `byte[][]`.
* The former is more intuitive, as it provides an array of json where each packet of json contains metadata generally helpful for processing with Kafka, such as the headers and partitions, which we use in practice.
* The `byte[][]` overload is less intuitive, as the metadata is buried within an injectable `FunctionContext` object and, worse, distributed amongst several arrays rather than being one array containing a relevant metadata class. Additionally receiving and processing tombstone events under this overload is nontrivial albeit not impossible, although that is not the focus of this issue.

Our first attempts to use the `string[]` overload failed, as we received fairly opaque errors trying to deserialize Avro from the data within the json. Because the errors come from deep in the Avro deserialization call stack they were completely opaque to us, and worse, the in the end the minimal example required to reproduce this here is actually from the most nested type as part of the Avro we were originally trying to deserialize (in this repo's terms, we wanted Bar, and the actual error is on behalf of deserializing Bar15). These types have been anonymised from our business logic.

The root cause seems to be that the byte array for the actual Kafka event is wrong under the `string[]` overload, but not under the `byte[][]` overload.

The csvs stringTrigger.csv and bytesArrayTrigger.csv are the actual array of bytes for Bar15 under the `string[]` and `byte[][]` overloads respectively.

## WHAT THIS APP DOES

Publishes a static event of type Bar15, serialized using Avro, to Kafka. There are then consumer functions which process batches of these events (albeit typically one of a time as we produce one at a time here) in different ways, and print the corresponding json for the event of type Bar15 from the deserialized POCO. Each consumer function has its own consumer group to not compete for events.

To reproduce this we have used the dependencies below. 

## DEPENDENCIES

* Kafka (e.g. Confluent Kafka). A docker compose has been provided for this.
* Azure schema registry. We do not think the schema registries are relevant to the bug however one is required. A schema registry container is available in the docker compose however commented out.

## EXPECTED RESULT

In a basic sense, running this app with both ConsumerBatchedStrings and ConsumerBatchedBytes uncommented should work without errors.

## ACTUAL RESULT

ConsumerBatchedStrings errors, particularly on behalf of trying to deserialize the Kafka event value from Avro. The relevant array of bytes for the Avro seems to be wrong, in comparison to debugging of ConsumerBatchedBytes which works, implying the byte array there is somehow not mutated in comparison to usage of the `string[]` overload.

## TO REPRODUCE

* Bar15 should exist as a type in an Azure schema registry. The avro file for this can be found as `Bar15.avsc`.
* Set SchemaGroup and SchemaRegistryUrl in local.settings.json per configuration of the Azure schema registry used above. Authentication with Azure schema registry is on behalf of your machine's Azure AAD auth.
* Set BootstrapServers in local.settings.json to the URL for Kafka, e.g. Confluent Kafka. No further configuration is particularly required if a Confluent Kafka container is being used, the topic will autocreate.
    * If using the provided docker compose then this is `"BootstrapServers": "host.docker.internal:9092"`
