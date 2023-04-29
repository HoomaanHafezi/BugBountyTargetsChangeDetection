using System.Text.Json;
using TargetChangeDetection.DownloadServices;
using TargetChangeDetection.Models;

namespace TargetChangeDetection.ChangeDetectionServices;

public static class WildcardChanges
{
    public static async Task Detect()
    {
        try
        {
            var currentData = await DownloadService.GetWildcards();
            var oldFilePath = Path.Combine(Directory.GetCurrentDirectory(), "Data", "Wildcards.txt");
            StreamReader textReader = File.OpenText(oldFilePath);
            var oldData = (await textReader.ReadToEndAsync()).Split("\n");
            bool hasChanged = false;

            Console.WriteLine(
                "\nWildcard -----------------------------------------------------------------------------------------");

            foreach (var current in currentData.Split("\n"))
            {
                if (oldData.FirstOrDefault(x => x.ToLower().Equals(current.ToLower())) is null)
                {
                    hasChanged = true;
                    Console.WriteLine("Wildcard : New Target =>");
                    Console.WriteLine($"\t{current}");
                }
            }

            if (hasChanged)
            {
                var filePath = Path.Combine(Directory.GetCurrentDirectory(), "Data", "Wildcards.txt");
                StreamWriter textWriter = File.CreateText(filePath);
                await textWriter.WriteAsync(currentData);
                textWriter.Close();
                await textWriter.DisposeAsync();
            }
            else
            {
                Console.WriteLine($"Wildcard : No Changes");
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