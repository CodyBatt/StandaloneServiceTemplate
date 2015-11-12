using CommandLine;
using CommandLine.Text;

namespace Client
{
    interface IProgramOptions
    {
        bool Help { get; }
    }

    class CommandLineOptions : IProgramOptions
    {
        [Option('h', "help", Required = false,
         HelpText = "Display option help message")]
        public bool Help { get; set; }

        [ParserState]
        public IParserState LastParserState { get; set; }

        [HelpOption]
        public string GetUsage()
        {
            return HelpText.AutoBuild(this,
              current => HelpText.DefaultParsingErrorsHandler(this, current));
        }
    }
}
