namespace netopt
{
    public class Argument
    {
        public string Name { get; }
        
        public bool RequireParams { get; }

        public NetOptHelp Help { get; }

        public OptionType Type { get; } = OptionType.ShortOption;

        public override string ToString() => Type == OptionType.ShortOption ? "-" + Name : "--" + Name;

        public Argument(string argName, bool requiredParams, NetOptHelp help = null) => 
            (Name, RequireParams, Help) = (argName, requiredParams, help);
        
        public Argument(string argName, bool requiredParams, OptionType type, NetOptHelp help = null) => 
            (Name, RequireParams, Type, Help) = (argName, requiredParams, type, help);
        
        public Argument(string argName, NetOptHelp help = null) => (Name, Help) = (argName, help);
        
        public Argument(string argName, OptionType type, NetOptHelp help = null) => (Name, Type, Help) = (argName, type, help);
    }
}