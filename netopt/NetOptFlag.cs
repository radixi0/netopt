using System.Collections.Generic;

namespace netopt
{
    public class NetOptFlag
    {
        public string Name { get; }
        public List<string> Params { get; }
        public NetOptHelp Help { get; }
        public NetOptFlag(string flagName, NetOptHelp help = null) => 
            (Name, Params, Help) = (flagName, new List<string>(), help);
        public NetOptFlag(string flagName, List<string> @params, NetOptHelp help = null) => 
            (Name, Params, Help) = (flagName, @params, help);
    }
}