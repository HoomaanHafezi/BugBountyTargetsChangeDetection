// See https://aka.ms/new-console-template for more information

using TargetChangeDetection;
using TargetChangeDetection.ChangeDetectionServices;
using TargetChangeDetection.DownloadServices;
using TargetChangeDetection.Models;

if (Path.Exists(Path.Combine(Directory.GetCurrentDirectory(), "Data")) == false)
{
    Directory.CreateDirectory(Path.Combine(Directory.GetCurrentDirectory(), "Data"));
    var domains = await DownloadService.GetDomains();
    var domainFilePath = Path.Combine(Directory.GetCurrentDirectory(), "Data", "Domains.txt");
    StreamWriter domainTextWriter = File.CreateText(domainFilePath);
    await domainTextWriter.WriteAsync(domains);
    domainTextWriter.Close();
    
    var wildcards = await DownloadService.GetWildcards();
    var wildcardFilePath = Path.Combine(Directory.GetCurrentDirectory(), "Data", "Wildcards.txt");
    StreamWriter wildcardTextWriter = File.CreateText(wildcardFilePath);
    await wildcardTextWriter.WriteAsync(wildcards);
    wildcardTextWriter.Close();
    await wildcardTextWriter.DisposeAsync();
    
    var hackerOneData = await DownloadService.GetHackerOneData();
    var hackerOneFilePath = Path.Combine(Directory.GetCurrentDirectory(), "Data", "HackerOneData.txt");
    StreamWriter hackerOneTextWriter = File.CreateText(hackerOneFilePath);
    await hackerOneTextWriter.WriteAsync(hackerOneData);
    hackerOneTextWriter.Close();
    await hackerOneTextWriter.DisposeAsync();
    
    
    var bugcrowdData = await DownloadService.GetBugCrowdData();
    var bugcrowdFilePath = Path.Combine(Directory.GetCurrentDirectory(), "Data", "BugCrowdData.txt");
    StreamWriter bugcrowdTextWriter = File.CreateText(bugcrowdFilePath);
    await bugcrowdTextWriter.WriteAsync(bugcrowdData);
    bugcrowdTextWriter.Close();
    await bugcrowdTextWriter.DisposeAsync();
    
    var yesWeHackData = await DownloadService.GetYesWeHackData();
    var yesWeHackFilePath = Path.Combine(Directory.GetCurrentDirectory(), "Data", "YesWeHackData.txt");
    StreamWriter yesWeHackTextWriter = File.CreateText(yesWeHackFilePath);
    await yesWeHackTextWriter.WriteAsync(yesWeHackData);
    yesWeHackTextWriter.Close();
    await yesWeHackTextWriter.DisposeAsync();
    
    
    var intigritiData = await DownloadService.GetIntigritiData();
    var intigritiFilePath = Path.Combine(Directory.GetCurrentDirectory(), "Data", "IntigritiData.txt");
    StreamWriter intigritiTextWriter = File.CreateText(intigritiFilePath);
    await intigritiTextWriter.WriteAsync(intigritiData);
    intigritiTextWriter.Close();
    await intigritiTextWriter.DisposeAsync();
    
    Console.WriteLine("You are running this app for the first time.");
    Console.WriteLine("Current data of targets is stored.");
    Console.WriteLine("Run the app later to see new targets and programs.");
    Console.Write("Press Enter to Exit...");
    Console.ReadLine();
    return;
}

if (Path.Exists(Path.Combine(Directory.GetCurrentDirectory(), "Changes")) == false){
    Directory.CreateDirectory(Path.Combine(Directory.GetCurrentDirectory(), "Changes"));
}

Console.Write("Press Enter to Start...");
Console.ReadLine();

await DomainChanges.Detect();
await WildcardChanges.Detect();
await HackerOneChanges.Detect();
await BugcrowdChanges.Detect();
await IntigritiChanges.Detect();
await YesWeHackChanges.Detect();

Console.Write("\nPress Enter to Exit...");
Console.ReadLine();

