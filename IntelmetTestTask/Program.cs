using Newtonsoft.Json;

namespace IntelmetTestTask
{
    internal class Program
    {
        private const string ConfigFileName = "Config.json";
        private const string DateTimeFormat = "yyyy-MM-ddTHH:mm:ss.fffzzz";

        private static void Main(string[] args)
        {
            Console.WriteLine("Test task for Intelmet Technologies by Andrei Kekov, March 2024");

            if (args.Length == 0)
            {
                DisplayHelp();
                Console.ReadLine();
                return;
            }

            string dataFileName = args[0];
            Config config = LoadConfig();

            if (config is null)
            {
                Console.WriteLine($"Failed to open file: {ConfigFileName}");
                Console.ReadLine();
                return;
            }

            string json = ReadJson(dataFileName);
            Measure measure = JsonConvert.DeserializeObject<Measure>(json);

            if (measure is null)
            {
                Console.WriteLine($"Failed to load data from file: {dataFileName}");
                Console.ReadLine();
                return;
            }

            var helper = new MeasureHelper();
            helper.RequiredSnr1 = config.RequiredSnr1;
            helper.SpeedThreshold = config.SpeedThreshold;
            List<Range> ranges = helper.GetRangesWithoutNoise(measure);
            DisplayRangeList(ranges);
            Console.WriteLine("Press any key to finish...");
            Console.ReadLine();
        }

        private static Config? LoadConfig()
        {
            string json = ReadJson(ConfigFileName);
            return JsonConvert.DeserializeObject<Config>(json);
        }

        private static void DisplayHelp()
        {
            Console.WriteLine("Usage: Demo.exe <filename>");
        }

        private static string ReadJson(string fileName)
        {
            FileStream file = new FileStream(fileName, FileMode.Open, FileAccess.Read);
            StreamReader reader = new StreamReader(file);
            string json = reader.ReadToEnd();
            file.Close();
            return json;
        }

        private static void DisplayRangeList(List<Range> ranges)
        {
            string? start = null;
            string? end = null;

            for (int i = 0; i < ranges.Count; i++)
            {
                start = ranges[i].Start.ToString(DateTimeFormat);
                end = ranges[i].End.ToString(DateTimeFormat);
                Console.WriteLine($"Range {i}: {start} to {end}");
            }
        }
    }
}
