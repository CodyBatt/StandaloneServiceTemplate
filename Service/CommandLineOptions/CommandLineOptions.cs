using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.CommandLineOptions
{
    
    interface IProgramOptions : IBaseProgramOptions
    {
        // Command line options specific to your service go here...
    }

    class CommandLineOptions :BaseCommandLineOptions, IProgramOptions
    {
        // Command line options specific to your service go here...
    }
}
