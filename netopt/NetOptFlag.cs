using System.Collections.Generic;

namespace netopt
{
    public class NetOptFlag
    {
        public string Name { get; }
        public List<string> Params { get; }

        public NetOptFlag(string flagName) => (Name, Params) = (flagName, new List<string>());
        
        public NetOptFlag(string flagName, List<string> @params) => (Name, Params) = (flagName, @params);
    }
}