using System.Text.Json.Serialization;

namespace TargetChangeDetection.Models;

public class Bugcrowd
{
    [JsonPropertyName("name")] public string Name { get; set; } = null!;
    [JsonPropertyName("url")]  public string Url { get; set; }= null!;
    [JsonPropertyName("targets")] public BugcrowdTargets? Targets { get; set; }
}

public class BugcrowdTargets
{
    [JsonPropertyName("in_scope")] public BugcrowdInScope[]? InScope { get; set; }
    [JsonPropertyName("out_of_scope")] public BugcrowdOutOfScope[]? OutOfScope { get; set; }
}

public class BugcrowdInScope
{
    [JsonPropertyName("type")] public string Type { get; set; }= null!;
    [JsonPropertyName("target")] public string Target { get; set; }= null!;
}

public class BugcrowdOutOfScope
{
    [JsonPropertyName("type")] public string Type { get; set; }= null!;
    [JsonPropertyName("target")] public string Target { get; set; }= null!;
}