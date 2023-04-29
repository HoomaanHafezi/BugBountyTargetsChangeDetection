using System.Text.Json.Serialization;

namespace TargetChangeDetection.Models;

public class HackerOne
{
    [JsonPropertyName("handle")] public string Handle { get; set; } = null!;
    [JsonPropertyName("name")] public string Name { get; set; } = null!;
    [JsonPropertyName("offers_bounties")] public bool OffersBounties { get; set; }
    [JsonPropertyName("submission_state")] public string SubmissionState { get; set; } = null!;
    [JsonPropertyName("url")] public string Url { get; set; } = null!;
    [JsonPropertyName("website")] public string Website { get; set; } = null!;
    [JsonPropertyName("targets")] public HackerOneTargets? Targets { get; set; } = null!;
}

public class HackerOneTargets
{
    [JsonPropertyName("in_scope")] public HackerOneInScope[]? InScope { get; set; }
    [JsonPropertyName("out_of_scope")] public HackerOneOutOfScope[]? OutOfScope { get; set; }
}

public class HackerOneInScope
{
    [JsonPropertyName("asset_identifier")] public string AssetIdentifier { get; set; } = null!;
    [JsonPropertyName("asset_type")] public string AssetType { get; set; } = null!;

    [JsonPropertyName("eligible_for_bounty")]
    public bool? EligibleForBounty { get; set; }

    [JsonPropertyName("eligible_for_submission")]
    public bool? EligibleForSubmission { get; set; }

    [JsonPropertyName("instruction")] public string Instruction { get; set; } = null!;
}

public class HackerOneOutOfScope
{
    [JsonPropertyName("asset_identifier")] public string AssetIdentifier { get; set; } = null!;
    [JsonPropertyName("asset_type")] public string AssetType { get; set; } = null!;

    [JsonPropertyName("eligible_for_bounty")]
    public bool? EligibleForBounty { get; set; }

    [JsonPropertyName("eligible_for_submission")]
    public bool? EligibleForSubmission { get; set; }

    [JsonPropertyName("instruction")] public string Instruction { get; set; } = null!;
}