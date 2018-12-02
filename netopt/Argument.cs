namespace netopt
{
    public class Argument
    {
        public string Name { get; }
        
        public bool RequireParams { get; }
        
        public OptionType Type { get; } = OptionType.ShortOption;

        public override string ToString() => Type == OptionType.ShortOption ? "-" + Name : "--" + Name;

        public Argument(string argName, bool requiredParams) => (Name, RequireParams) = (argName, requiredParams);
        
        public Argument(string argName, bool requiredParams, OptionType type) => (Name, RequireParams, Type) = (argName, requiredParams, type);
        
        public Argument(string argName) => Name = argName;
        
        public Argument(string argName, OptionType type) => (Name, Type) = (argName, type);
    }
}