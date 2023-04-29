using System.Text.Json.Serialization;

namespace TargetChangeDetection.Models;

public class YesWeHack
{
    [JsonPropertyName("id")]
    public string Id { get; set; }= null!;
    [JsonPropertyName("name")]
    public string Name { get; set; }= null!;
    [JsonPropertyName("public")]
    public bool IsPublic { get; set; }
    [JsonPropertyName("disabled")]
    public bool Disabled { get; set; }
    [JsonPropertyName("targets")]
    public YesWeHackTargets? Targets { get; set; }
}

public class YesWeHackTargets
{
    [JsonPropertyName("in_scope")]
    public YesWeHackInScope[]? InScope { get; set; }
    [JsonPropertyName("out_of_scope")]
    public YesWeHackOutOfScope[]? OutOfScope { get; set; }
}

public class YesWeHackInScope
{
    [JsonPropertyName("target")]
    public string Target { get; set; }= null!;
    [JsonPropertyName("type")]
    public string Type { get; set; }= null!;
}

public class YesWeHackOutOfScope
{
    [JsonPropertyName("target")]
    public string Target { get; set; }= null!;
    [JsonPropertyName("type")]
    public string Type { get; set; }= null!;
}

