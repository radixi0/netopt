using System.Collections.Generic;

namespace netopt
{
    public class ArgumentBuilder
    {
        public List<Argument> Rules { get; }

        public void Add(Argument argument) => Rules.Add(argument);

        public ArgumentBuilder() => Rules = new List<Argument>();
    }
}