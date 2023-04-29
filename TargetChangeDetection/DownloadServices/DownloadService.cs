using System.Net.Http.Json;
using System.Runtime.CompilerServices;
using TargetChangeDetection.Models;
using TargetChangeDetection.Static;

namespace TargetChangeDetection.DownloadServices;

public static class DownloadService
{
    public static async Task<string> GetDomains()
    {
        using var httpClient = new HttpClient();
        var domains = await httpClient.GetStringAsync(StaticData.DomainsUrl);
        return domains;
        // var filePath = Path.Combine(Directory.GetCurrentDirectory(), "Data" ,"Domains.txt");
        // StreamWriter textWriter = File.CreateText(filePath);
        // await textWriter.WriteAsync(domains);
        // textWriter.Close();
    }
    
    public static async Task<string> GetWildcards()
    {
        using var httpClient = new HttpClient();
        var wildcards = await httpClient.GetStringAsync(StaticData.WildCardsUrl);
        return wildcards;
        // var filePath = Path.Combine(Directory.GetCurrentDirectory(), "Data" ,"Wildcards.txt");
        // StreamWriter textWriter = File.CreateText(filePath);
        // await textWriter.WriteAsync(domains);
        // textWriter.Close();
    }
    
    public static async Task<string> GetHackerOneData()
    {
        using var httpClient = new HttpClient();
        var data = await httpClient.GetStringAsync(StaticData.HackerOneDataUrl);
        return data;
        // var filePath = Path.Combine(Directory.GetCurrentDirectory(), "Data" ,"HackerOneData.txt");
        // StreamWriter textWriter = File.CreateText(filePath);
        // await textWriter.WriteAsync(domains);
        // textWriter.Close();
    }
    
    public static async Task<string> GetIntigritiData()
    {
        using var httpClient = new HttpClient();
        var data = await httpClient.GetStringAsync(StaticData.IntigritiUrl);
        return data;
        // var filePath = Path.Combine(Directory.GetCurrentDirectory(), "Data" ,"IntigritiData.txt");
        // StreamWriter textWriter = File.CreateText(filePath);
        // await textWriter.WriteAsync(domains);
        // textWriter.Close();
    }
    
    public static async Task<string> GetYesWeHackData()
    {
        using var httpClient = new HttpClient();
        var data = await httpClient.GetStringAsync(StaticData.YesWeHackUrl);
        return data;
        // var filePath = Path.Combine(Directory.GetCurrentDirectory(), "Data" ,"YesWeHackData.txt");
        // StreamWriter textWriter = File.CreateText(filePath);
        // await textWriter.WriteAsync(domains);
        // textWriter.Close();
    }
    
    public static async Task<string> GetBugCrowdData()
    {
        using var httpClient = new HttpClient();
        var data = await httpClient.GetStringAsync(StaticData.BugCrowdDataUrl);
        return data;
        // var filePath = Path.Combine(Directory.GetCurrentDirectory(), "Data" ,"BugCrowdData.txt");
        // StreamWriter textWriter = File.CreateText(filePath);
        // await textWriter.WriteAsync(domains);
        // textWriter.Close();
    }
}