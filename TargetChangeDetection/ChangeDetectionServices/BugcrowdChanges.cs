using System.Text.Json.Nodes;
using TargetChangeDetection.Static;
using TargetChangeDetection.DownloadServices;
using System.Text.Json;
using TargetChangeDetection.Models;

namespace TargetChangeDetection.ChangeDetectionServices;

public static class BugcrowdChanges
{
    public static async Task Detect()
    {
        try
        {
            // Get from Github if the is any change delete old files , rename new ones to old and create new files
            var currentData = JsonSerializer.Deserialize<List<Bugcrowd>>(await DownloadService.GetBugCrowdData());
            var oldFilePath = Path.Combine(Directory.GetCurrentDirectory(), "Data" ,"BugCrowdData.txt");
            StreamReader textReader = File.OpenText(oldFilePath);
            var oldData = JsonSerializer.Deserialize<List<Bugcrowd>>(await textReader.ReadToEndAsync());
            bool hasChanged = false;
        
            Console.WriteLine("\nBugcrowd ------------------------------------------------------------------------------------------");

            var changesFilePath = Path.Combine(Directory.GetCurrentDirectory(), "Changes", "BugCrowdDataChanges.txt");
            StreamWriter changesFileTextWriter = File.AppendText(changesFilePath);
            await changesFileTextWriter.WriteAsync($"{DateTime.Now}=============================================================================\n");

            foreach (var current in currentData)
            {
                if (oldData?.FirstOrDefault(x=>x.Name.ToLower().Equals(current.Name.ToLower())) is null)
                {
                    hasChanged = true;
                    Console.WriteLine("Bugcrowd : New Program =>");
                    Console.WriteLine($"\t{current.Name}");
                    Console.WriteLine($"\t\t{JsonSerializer.Serialize(current.Targets)}");
                    await changesFileTextWriter.WriteAsync("Bugcrowd : New Program =>\n");
                    await changesFileTextWriter.WriteAsync($"\t{current.Name} \n");
                    await changesFileTextWriter.WriteAsync($"\t\t{JsonSerializer.Serialize(current.Targets)} \n");
                    continue;
                }

                foreach (var old in oldData)
                {
                    foreach (var currentInScope in current.Targets?.InScope ?? Enumerable.Empty<BugcrowdInScope>())
                    {
                        if (old.Name.ToLower().Equals(current.Name.ToLower()) && old.Targets?.InScope?.FirstOrDefault(x=>x.Target.ToLower().Equals(currentInScope.Target.ToLower())) is null)
                        {
                            hasChanged = true;
                            Console.WriteLine($"Bugcrowd : New In Scope Target For {current.Name} =>");
                            Console.WriteLine($"\t\t{currentInScope.Target} : {currentInScope.Type}");
                            await changesFileTextWriter.WriteAsync($"Bugcrowd : New In Scope Target For {current.Name} =>\n");
                            await changesFileTextWriter.WriteAsync($"\t\t{currentInScope.Target} : {currentInScope.Type}\n");
                        }
                    }
                
                    foreach (var currentOutOfScope in current.Targets?.OutOfScope ?? Enumerable.Empty<BugcrowdOutOfScope>())
                    {
                        if (old.Name.ToLower().Equals(current.Name.ToLower()) && old.Targets?.OutOfScope?.FirstOrDefault(x=>x.Target.ToLower().Equals(currentOutOfScope.Target.ToLower())) is null)
                        {
                            hasChanged = true;
                            Console.WriteLine($"Bugcrowd : New Out Of Scope Target For {current.Name} =>");
                            Console.WriteLine($"\t\t{currentOutOfScope.Target} : {currentOutOfScope.Type}");
                            await changesFileTextWriter.WriteAsync($"Bugcrowd : New Out Of Scope Target For {current.Name} =>\n");
                            await changesFileTextWriter.WriteAsync($"\t\t{currentOutOfScope.Target} : {currentOutOfScope.Type}\n");
                        }
                    }
                }
            }

            if (hasChanged)
            {
                var filePath = Path.Combine(Directory.GetCurrentDirectory(), "Data" ,"BugCrowdData.txt");
                StreamWriter textWriter = File.CreateText(filePath);
                await textWriter.WriteAsync(JsonSerializer.Serialize(currentData));
                textWriter.Close();
                await textWriter.DisposeAsync();
            }
            else
            {
                Console.WriteLine($"Bugcrowd : No Changes");
                await changesFileTextWriter.WriteAsync($"Bugcrowd : No Changes\n");
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