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

            var changesFilePath = Path.Combine(Directory.GetCurrentDirectory(), "Changes", "HackerOneDataChanges.txt");
            StreamWriter changesFileTextWriter = File.AppendText(changesFilePath);
            await changesFileTextWriter.WriteAsync($"{DateTime.Now}=============================================================================\n");

            foreach (var current in currentData)
            {
                if (oldData?.FirstOrDefault(x=>x.Name.ToLower().Equals(current.Name.ToLower())) is null)
                {
                    hasChanged = true;
                    Console.WriteLine("HackerOne : New Program =>");
                    Console.WriteLine($"\t{current.Name}");
                    Console.WriteLine($"\t\t{JsonSerializer.Serialize(current.Targets)}");
                    await changesFileTextWriter.WriteAsync("HackerOne : New Program =>\n");
                    await changesFileTextWriter.WriteAsync($"\t{current.Name}\n");
                    await changesFileTextWriter.WriteAsync($"\t\t{JsonSerializer.Serialize(current.Targets)}\n");
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
                            await changesFileTextWriter.WriteAsync($"HackerOne : New In Scope Target For {current.Name} =>\n");
                            await changesFileTextWriter.WriteAsync($"\t\t{currentInScope.AssetIdentifier} : {currentInScope.AssetType}\n");
                        }
                    }
                
                    foreach (var currentOutOfScope in current.Targets?.OutOfScope ?? Enumerable.Empty<HackerOneOutOfScope>())
                    {
                        if (old.Name.ToLower().Equals(current.Name.ToLower()) && old.Targets?.OutOfScope?.FirstOrDefault(x=>x.AssetIdentifier.ToLower().Equals(currentOutOfScope.AssetIdentifier.ToLower())) is null)
                        {
                            hasChanged = true;
                            Console.WriteLine($"HackerOne : New Out Of Scope Target For {current.Name} =>");
                            Console.WriteLine($"\t\t{currentOutOfScope.AssetIdentifier} : {currentOutOfScope.AssetType}");
                            await changesFileTextWriter.WriteAsync($"HackerOne : New Out Of Scope Target For {current.Name} =>\n");
                            await changesFileTextWriter.WriteAsync($"\t\t{currentOutOfScope.AssetIdentifier} : {currentOutOfScope.AssetType} \n");
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
                await changesFileTextWriter.WriteAsync($"Hackerone : No Changes\n");
            }
            changesFileTextWriter.Close();
            await changesFileTextWriter.DisposeAsync();
            textReader.Dispose();
        }
        catch (Exception e)
        {
            Console.WriteLine(" :( Something went wrong...");
            Console.WriteLine(e.Message);
        }
    }
}