using System.Linq;
using System.CommandLine;
using System.CommandLine.Invocation;
using System.Threading.Tasks;
using System.CommandLine.Builder;
using System.IO;

namespace JsonMerge.CommandLine
{
    public class Program
    {
        public static async Task<int> Main(string[] args)
        {
            var rootCommand = new RootCommand
            {
                new Argument<FileInfo[]>("input-files")
                {
                    Description = "Two or more JSON files to be merged",
                    Arity = new ArgumentArity(2, byte.MaxValue)
                }.ExistingOnly(),
                new Option(new [] {"-o", "--output", "--output-file"})
                {
                    Description = "Output file path",
                    Argument = new Argument<FileInfo>()
                },
                new Option(new [] {"-f", "--force"})
                {
                    Description = "Override output file if it already exists",
                    Argument = new Argument<bool>()
                }
            };
            rootCommand.Name = "jsonmerge";
            rootCommand.Description = "Merge json files";
            rootCommand.Handler = CommandHandler.Create(typeof(Program).GetMethod(nameof(Merge)));

            return await rootCommand.InvokeAsync(args);
        }

        public static int Merge(FileInfo[] inputFiles, FileInfo output, bool force, IConsole console)
        {
            if (output == null)
            {
                console.Error.WriteLine("Output file path is required.");
                return 1;
            }
            var sourceJsons = inputFiles.Select(f => File.ReadAllText(f.FullName));
            var mergedJson = JsonMerger.Merge(sourceJsons);

            if (output.Exists && !force)
            {
                console.Error.WriteLine("Output file already exists. Use --force option to overwrite it.");
                return 1;
            }

            // Make sure directory exists otherwise saving a file to non-existing directory throws exception.
            output.Directory.Create();

            File.WriteAllText(output.FullName, mergedJson);

            return 0;
        }
    }
}
