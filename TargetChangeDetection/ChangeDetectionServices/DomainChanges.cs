using TargetChangeDetection.DownloadServices;

namespace TargetChangeDetection.ChangeDetectionServices;

public class DomainChanges
{
    public static async Task Detect()
    {
        try
        {
            var currentData = await DownloadService.GetDomains();
            var oldFilePath = Path.Combine(Directory.GetCurrentDirectory(), "Data", "Domains.txt");
            StreamReader textReader = File.OpenText(oldFilePath);
            var oldData = (await textReader.ReadToEndAsync()).Split("\n");
            bool hasChanged = false;

            Console.WriteLine(
                "\nDomains -----------------------------------------------------------------------------------------");

            foreach (var current in currentData.Split("\n"))
            {
                if (oldData.FirstOrDefault(x => x.ToLower().Equals(current.ToLower())) is null)
                {
                    hasChanged = true;
                    Console.WriteLine("Domains : New Target =>");
                    Console.WriteLine($"\t{current}");
                }
            }

            if (hasChanged)
            {
                var filePath = Path.Combine(Directory.GetCurrentDirectory(), "Data", "Domains.txt");
                StreamWriter textWriter = File.CreateText(filePath);
                await textWriter.WriteAsync(currentData);
                textWriter.Close();
                await textWriter.DisposeAsync();
            }
            else
            {
                Console.WriteLine($"Domains : No Changes");
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