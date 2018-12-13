## NetOpt

A simple command line parser for .NET Core.

Install nuget package:

> Install-Package NetOpt -Version 1.0.0

### Definitions

`OptionType.ShortOption` should be used for simple flags, such as: **-a**, **-b**, **-c**. 

Also accepted in a grouped form: `-abc` - and in any order: `-c -a -b ` .. `-cab`

`OptionType.LongOption` should be used for long options, like: `--help --version --files-to-build`


### How to use

**ArgumentBuilder** is the class that contains all flags definitions.


#### Serch for a single flag
```
var p = new ArgumentBuilder();
p.Add(new Argument("v")); // Look for -v flag on args array. By default, flags has OptionType = ShortOption

```

#### Search for a single flag, long option

```
var p = new ArgumentBuilder();
p.Add(new Argument("version", OptionType.LongOption)); // Look for --version flag on args array.

```

### Search for a flag, but with params

**RequireParams** must be set true:

```
var p = new ArgumentBuilder();
p.Add(new Argument("file", true, OptionType.LongOption)); // Look for flag and params


Input Sample: --file filea.c fileb.c filec.c
```

The strings "filea.c", "fileb.c" and "filec.c" will be available at the property `Params` of flag "file".


### Sample program

```csharp
using System;
using netopt;

namespace OpTest
{
    public static class Program
    {
        public static void Main(string[] args)
        {
            try
            {
                var p = new ArgumentBuilder();
                p.Add(new Argument("file", true, OptionType.LongOption));
                p.Add(new Argument("e"));
                p.Add(new Argument("f"));
                p.Add(new Argument("a"));
                p.Add(new Argument("x"));
                p.Add(new Argument("foo", OptionType.LongOption));

                var x = new NetOpt();
                x.Parse(args, p);

                if (x.HasFlag("help"))
                    Console.WriteLine("NetOpt version 1.0.0");

                if (x.HasFlag("file"))
                {
                    Console.WriteLine("Param value for file:");

                    foreach (var param in x.GetFlag("file").Params)
                        Console.WriteLine(param);
                }
                
                if (x.HasFlag("x"))
                    Console.WriteLine("Executing option for 'x' flag!");

                foreach (var flag in x.Flags)
                    Console.WriteLine($"Flag '{flag.Name}' found!");
                
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }
    }
}
```

### Input for program

> -ef -a --file a.c b.c d.c -x --foo

### Program Output

``` 
Param value for file: a.c
b.c
d.c
Executing option for 'x' flag!
Flag 'file' found!
Flag 'e' found!
Flag 'f' found!
Flag 'a' found!
Flag 'x' found!
Flag 'foo' found!
```

### Exception Handling

if a flag is marked `RequiredParams` and no parameter is entered, an exception will be thrown:

Sample input: 

```-a -b --file -x -p // --file is set to require params```

Output: 

> Error: flag 'file' needs a parameter, but was not provided 

