namespace KafkaMWE;

public class KafkaData
{
    public string Key { get; set; } = string.Empty;

    public string? Value { get; set; }

    public IEnumerable<KeyValuePair<string, string>>? Headers { get; set; }
}
