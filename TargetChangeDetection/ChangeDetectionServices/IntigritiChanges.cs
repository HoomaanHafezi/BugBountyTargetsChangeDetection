using System.Text.Json;
using TargetChangeDetection.DownloadServices;
using TargetChangeDetection.Models;

namespace TargetChangeDetection.ChangeDetectionServices;

public static class IntigritiChanges
{
    public static async Task Detect()
    {
        try
        {
            var currentData = JsonSerializer.Deserialize<List<Intigriti>>(await DownloadService.GetIntigritiData());
            var oldFilePath = Path.Combine(Directory.GetCurrentDirectory(), "Data", "IntigritiData.txt");
            StreamReader textReader = File.OpenText(oldFilePath);
            var oldData = JsonSerializer.Deserialize<List<Intigriti>>(await textReader.ReadToEndAsync());
            bool hasChanged = false;

            Console.WriteLine(
                "\nIntigriti -----------------------------------------------------------------------------------------");

            var changesFilePath = Path.Combine(Directory.GetCurrentDirectory(), "Changes", "IntigritiDataChanges.txt");
            StreamWriter changesFileTextWriter = File.AppendText(changesFilePath);
            await changesFileTextWriter.WriteAsync($"{DateTime.Now}=============================================================================\n");

            foreach (var current in currentData)
            {
                if (oldData?.FirstOrDefault(x => x.Name.ToLower().Equals(current.Name.ToLower())) is null)
                {
                    hasChanged = true;
                    Console.WriteLine("Intigriti : New Program =>");
                    Console.WriteLine($"\t{current.Name}");
                    Console.WriteLine($"\t\t{JsonSerializer.Serialize(current.Targets)}");
                    await changesFileTextWriter.WriteAsync("Intigriti : New Program =>\n");
                    await changesFileTextWriter.WriteAsync($"\t{current.Name} \n");
                    await changesFileTextWriter.WriteAsync($"\t\t{JsonSerializer.Serialize(current.Targets)} \n");
                    continue;
                }

                foreach (var old in oldData)
                {
                    foreach (var currentInScope in current.Targets?.InScope ?? Enumerable.Empty<IntigritiInScope>())
                    {
                        if (old.Name.ToLower().Equals(current.Name.ToLower()) && old.Targets?.InScope?.FirstOrDefault(x =>
                                x.Endpoint.ToLower().Equals(currentInScope.Endpoint.ToLower())) is null)
                        {
                            hasChanged = true;
                            Console.WriteLine($"Intigriti : New In Scope Target For {current.Name} =>");
                            Console.WriteLine($"\t\t{currentInScope.Endpoint} : {currentInScope.Type}");
                            await changesFileTextWriter.WriteAsync($"Intigriti : New In Scope Target For {current.Name} =>\n");
                            await changesFileTextWriter.WriteAsync($"\t\t{currentInScope.Endpoint} : {currentInScope.Type}\n");
                        }
                    }

                    foreach (var currentOutOfScope in
                             current.Targets?.OutOfScope ?? Enumerable.Empty<IntigritiOutOfScope>())
                    {
                        if (old.Name.ToLower().Equals(current.Name.ToLower()) && old.Targets?.OutOfScope?.FirstOrDefault(x =>
                                x.Endpoint.ToLower().Equals(currentOutOfScope.Endpoint.ToLower())) is null)
                        {
                            hasChanged = true;
                            Console.WriteLine($"Intigriti : New Out Of Scope Target For {current.Name} =>");
                            Console.WriteLine($"\t\t{currentOutOfScope.Endpoint} : {currentOutOfScope.Type}");
                            await changesFileTextWriter.WriteAsync($"Intigriti : New Out Of Scope Target For {current.Name} =>\n");
                            await changesFileTextWriter.WriteAsync($"\t\t{currentOutOfScope.Endpoint} : {currentOutOfScope.Type}\n");
                        }
                    }
                }
            }

            if (hasChanged)
            {
                var filePath = Path.Combine(Directory.GetCurrentDirectory(), "Data", "IntigritiData.txt");
                StreamWriter textWriter = File.CreateText(filePath);
                await textWriter.WriteAsync(JsonSerializer.Serialize(currentData));
                textWriter.Close();
                await textWriter.DisposeAsync();
            }
            else
            {
                Console.WriteLine($"Intigriti : No Changes");
                await changesFileTextWriter.WriteAsync($"Intigriti : No Changes\n");
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