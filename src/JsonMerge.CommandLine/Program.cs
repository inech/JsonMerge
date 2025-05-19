using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace JsonMerge.CommandLine
{
    internal static class Program
    {
        private static int Main(string[] args)
        {
            if (args.Length == 0)
            {
                PrintUsage();
                return 1;
            }

            var inputFiles = new List<string>();
            string? outputFile = null;
            bool force = false;

            for (var i = 0; i < args.Length; i++)
            {
                var arg = args[i];
                switch (arg)
                {
                    case "-o":
                    case "--output":
                        if (i + 1 >= args.Length)
                        {
                            Console.Error.WriteLine("Missing output path after -o.");
                            return 1;
                        }
                        outputFile = args[++i];
                        break;
                    case "-f":
                    case "--force":
                        force = true;
                        break;
                    default:
                        inputFiles.Add(arg);
                        break;
                }
            }

            if (inputFiles.Count < 2)
            {
                Console.Error.WriteLine("At least two input files are required.");
                return 1;
            }

            if (string.IsNullOrEmpty(outputFile))
            {
                Console.Error.WriteLine("Output file path is required.");
                return 1;
            }

            if (File.Exists(outputFile) && !force)
            {
                Console.Error.WriteLine("Output file already exists. Use -f to overwrite.");
                return 1;
            }

            var sourceJsons = inputFiles.Select(File.ReadAllText);
            var mergedJson = JsonMerger.Merge(sourceJsons);

            var outputDir = Path.GetDirectoryName(Path.GetFullPath(outputFile));
            if (!string.IsNullOrEmpty(outputDir))
            {
                Directory.CreateDirectory(outputDir);
            }

            File.WriteAllText(outputFile, mergedJson);
            return 0;
        }

        private static void PrintUsage()
        {
            Console.Error.WriteLine("Usage: jsonmerge <input1> <input2> [<inputN>...] -o <output> [-f]");
        }
    }
}
