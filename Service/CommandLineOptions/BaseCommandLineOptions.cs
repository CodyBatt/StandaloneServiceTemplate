using CommandLine;
using CommandLine.Text;

namespace Service.CommandLineOptions
{
    interface IBaseProgramOptions
    {
        bool Help { get; }
        bool Interactive { get; }
#if DEBUG
        bool Debug { get; }
#endif
        string GetUsage();
    }

    class BaseCommandLineOptions : IBaseProgramOptions
    {
        [Option('h', "help", Required = false,
         HelpText = "Display option help message")]
        public bool Help { get; set; }

        [Option('i', "interactive", Required = false,
         HelpText = "Run in interactive console mode.")]
        public bool Interactive { get; set; }

#if DEBUG
        [Option('d', "debug", Required = false,
         HelpText = "Force a debug event to allow a debugger to be attached on startup.")]
        public bool Debug { get; set; }
#endif
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
