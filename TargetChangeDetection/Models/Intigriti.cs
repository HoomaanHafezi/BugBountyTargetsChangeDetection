using System.Text.Json.Serialization;

namespace TargetChangeDetection.Models;

public class Intigriti
{
    [JsonPropertyName("id")]
    public string Id { get; set; } = null!;
    [JsonPropertyName("name")]
    public string Name { get; set; } = null!;
    [JsonPropertyName("company_handle")]
    public string CompanyHandle { get; set; } = null!;
    [JsonPropertyName("handle")]
    public string Handle { get; set; } = null!;
    [JsonPropertyName("url")]
    public string Url { get; set; } = null!;
    [JsonPropertyName("status")]
    public string Status { get; set; } = null!;
    [JsonPropertyName("confidentiality_level")]
    public string ConfidentialityLevel { get; set; } = null!;
    [JsonPropertyName("targets")]
    public IntigritiTargets? Targets { get; set; }
}

public class IntigritiTargets
{
    [JsonPropertyName("in_scope")]
    public IntigritiInScope[]? InScope { get; set; }
    [JsonPropertyName("out_of_scope")]
    public IntigritiOutOfScope[]? OutOfScope { get; set; }
}

public class IntigritiInScope
{
    [JsonPropertyName("type")]
    public string Type { get; set; } = null!;
    [JsonPropertyName("endpoint")]
    public string Endpoint { get; set; } = null!;
    [JsonPropertyName("description")]
    public string Description { get; set; } = null!;
}

public class IntigritiOutOfScope
{
    [JsonPropertyName("type")]
    public string Type { get; set; } = null!;
    [JsonPropertyName("endpoint")]
    public string Endpoint { get; set; } = null!;
    [JsonPropertyName("description")]
    public string Description { get; set; } = null!;
}



