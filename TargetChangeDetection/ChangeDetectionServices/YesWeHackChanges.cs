using System.Text.Json;
using TargetChangeDetection.DownloadServices;
using TargetChangeDetection.Models;

namespace TargetChangeDetection.ChangeDetectionServices;

public static class YesWeHackChanges
{
    public static async Task Detect()
    {
        try
        {
            var currentData = JsonSerializer.Deserialize<List<YesWeHack>>(await DownloadService.GetYesWeHackData());
            var oldFilePath = Path.Combine(Directory.GetCurrentDirectory(), "Data", "YesWeHackData.txt");
            StreamReader textReader = File.OpenText(oldFilePath);
            var oldData = JsonSerializer.Deserialize<List<YesWeHack>>(await textReader.ReadToEndAsync());
            bool hasChanged = false;

            Console.WriteLine(
                "\nYesWeHack -----------------------------------------------------------------------------------------");

            foreach (var current in currentData)
            {
                if (oldData?.FirstOrDefault(x => x.Name.ToLower().Equals(current.Name.ToLower())) is null)
                {
                    hasChanged = true;
                    Console.WriteLine("YesWeHack : New Program =>");
                    Console.WriteLine($"\t{current.Name}");
                    Console.WriteLine($"\t\t{JsonSerializer.Serialize(current.Targets)}");
                    continue;
                }

                foreach (var old in oldData)
                {
                    foreach (var currentInScope in current.Targets?.InScope ?? Enumerable.Empty<YesWeHackInScope>())
                    {
                        if (old.Name.ToLower().Equals(current.Name.ToLower()) && old.Targets?.InScope?.FirstOrDefault(x =>
                                x.Target.ToLower().Equals(currentInScope.Target.ToLower())) is null)
                        {
                            hasChanged = true;
                            Console.WriteLine($"YesWeHack : New In Scope Target For {current.Name} =>");
                            Console.WriteLine($"\t\t{currentInScope.Target} : {currentInScope.Type}");
                        }
                    }

                    foreach (var currentOutOfScope in
                             current.Targets?.OutOfScope ?? Enumerable.Empty<YesWeHackOutOfScope>())
                    {
                        if (old.Name.ToLower().Equals(current.Name.ToLower()) && old.Targets?.OutOfScope?.FirstOrDefault(x =>
                                x.Target.ToLower().Equals(currentOutOfScope.Target.ToLower())) is null)
                        {
                            hasChanged = true;
                            Console.WriteLine($"YesWeHack : New Out Of Scope Target For {current.Name} =>");
                            Console.WriteLine($"\t\t{currentOutOfScope.Target} : {currentOutOfScope.Type}");
                        }
                    }
                }
            }

            if (hasChanged)
            {
                var filePath = Path.Combine(Directory.GetCurrentDirectory(), "Data", "YesWeHackData.txt");
                StreamWriter textWriter = File.CreateText(filePath);
                await textWriter.WriteAsync(JsonSerializer.Serialize(currentData));
                textWriter.Close();
                await textWriter.DisposeAsync();
            }
            else
            {
                Console.WriteLine($"YesWeHack : No Changes");
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