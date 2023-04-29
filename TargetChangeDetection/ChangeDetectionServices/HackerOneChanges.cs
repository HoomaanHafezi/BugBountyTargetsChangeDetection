using System.Text.Json;
using TargetChangeDetection.DownloadServices;
using TargetChangeDetection.Models;

namespace TargetChangeDetection.ChangeDetectionServices;

public static class HackerOneChanges
{
    public static async Task Detect()
    {
        try
        {
            var currentData = JsonSerializer.Deserialize<List<HackerOne>>(await DownloadService.GetHackerOneData());
            var oldFilePath = Path.Combine(Directory.GetCurrentDirectory(), "Data" ,"HackerOneData.txt");
            StreamReader textReader = File.OpenText(oldFilePath);
            var oldData = JsonSerializer.Deserialize<List<HackerOne>>(await textReader.ReadToEndAsync());
            bool hasChanged = false;
        
            Console.WriteLine("\nHackerOne -----------------------------------------------------------------------------------------");

            foreach (var current in currentData)
            {
                if (oldData?.FirstOrDefault(x=>x.Name.ToLower().Equals(current.Name.ToLower())) is null)
                {
                    hasChanged = true;
                    Console.WriteLine("HackerOne : New Program =>");
                    Console.WriteLine($"\t{current.Name}");
                    Console.WriteLine($"\t\t{JsonSerializer.Serialize(current.Targets)}");
                    continue;
                }

                foreach (var old in oldData)
                {
                    foreach (var currentInScope in current.Targets?.InScope ?? Enumerable.Empty<HackerOneInScope>())
                    {
                        if (old.Name.ToLower().Equals(current.Name.ToLower()) && old.Targets?.InScope?.FirstOrDefault(x=>x.AssetIdentifier.ToLower().Equals(currentInScope.AssetIdentifier.ToLower())) is null)
                        {
                            hasChanged = true;
                            Console.WriteLine($"HackerOne : New In Scope Target For {current.Name} =>");
                            Console.WriteLine($"\t\t{currentInScope.AssetIdentifier} : {currentInScope.AssetType}");
                        }
                    }
                
                    foreach (var currentOutOfScope in current.Targets?.OutOfScope ?? Enumerable.Empty<HackerOneOutOfScope>())
                    {
                        if (old.Name.ToLower().Equals(current.Name.ToLower()) && old.Targets?.OutOfScope?.FirstOrDefault(x=>x.AssetIdentifier.ToLower().Equals(currentOutOfScope.AssetIdentifier.ToLower())) is null)
                        {
                            hasChanged = true;
                            Console.WriteLine($"HackerOne : New Out Of Scope Target For {current.Name} =>");
                            Console.WriteLine($"\t\t{currentOutOfScope.AssetIdentifier} : {currentOutOfScope.AssetType}");
                        }
                    }
                }
            }

            if (hasChanged)
            {
                var filePath = Path.Combine(Directory.GetCurrentDirectory(), "Data" ,"HackerOneData.txt");
                StreamWriter textWriter = File.CreateText(filePath);
                await textWriter.WriteAsync(JsonSerializer.Serialize(currentData));
                textWriter.Close();
                await textWriter.DisposeAsync();
            }
            else
            {
                Console.WriteLine($"Hackerone : No Changes");
            }
            textReader.Dispose();
        }
        catch (Exception e)
        {
            Console.WriteLine(" :( Something went wrong...");
            Console.WriteLine(e.Message);
        }
    }
}