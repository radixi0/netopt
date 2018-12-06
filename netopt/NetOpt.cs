using System;
using System.Collections.Generic;
using System.Linq;

namespace netopt
{
    public enum OptionType
    {
        ShortOption = 0,
        LongOption = 1
    }

    public class NetOpt
    {
        public readonly List<NetOptFlag> Flags;

        public NetOpt()
        {
            Flags = new List<NetOptFlag>();
        }

        public void Parse(string[] args, ArgumentBuilder builder)
        {
            var lineToParse = string.Join(" ", args);

            foreach (var def in builder.Rules)
            {
                if (def.RequireParams)
                {
                    var singleParam = SeekForParam(lineToParse, def.ToString());

                    if (singleParam == "")
                        throw new Exception($"Error: flag '{def.Name}' needs a parameter, but was not provided");

                    if (def.Help == null)
                        Flags.Add(new NetOptFlag(def.Name, singleParam.Split(' ').ToList()));
                    else
                        Flags.Add(new NetOptFlag(def.Name, singleParam.Split(' ').ToList(), def.Help));
                }
                else
                {
                    if (def.Type == OptionType.ShortOption)
                    {
                        var singleOrGrouped = SeekForSingleOrGrouped(lineToParse, def.Name);
                        if (singleOrGrouped != "")
                        {
                            if (def.Help == null)
                                Flags.Add(new NetOptFlag(def.Name));
                            else
                                Flags.Add(new NetOptFlag(def.Name, def.Help));
                        }   
                    }
                    else
                    {
                        if (SeekForLongOption(lineToParse, def.ToString()))
                        {
                            if (def.Help == null)
                                Flags.Add(new NetOptFlag(def.Name));
                            else
                                Flags.Add(new NetOptFlag(def.Name, def.Help));
                        }
                    }
                }
            }
        }

        public NetOptFlag GetFlag(string flagName)
        {
            return Flags.Find(f => f.Name == flagName);
        }

        private static string SeekForParam(string line, string what)
        {
            var position = line.IndexOf(what, StringComparison.Ordinal);

            return position >= 0 ? CopyUntil(line, position + what.Length + 1, '-', '\n') : string.Empty;
        }


        private static string CopyUntil(string line, int startPosition, params char[] delimiters)
        {
            var res = "";
            for (var index = startPosition; index < line.Length; index++)
                if (!delimiters.Any(d => d.Equals(line[index])))
                    res += line[index];
                else
                    break;

            return res.Trim();
        }

        private static bool SeekForLongOption(string line, string what)
        {
            return line.IndexOf(what, StringComparison.InvariantCulture) >= 0;
        }

        private static string SeekForSingleOrGrouped(string line, string what)
        {
            var arr = line.Split('-', StringSplitOptions.RemoveEmptyEntries).Where(r => !r.StartsWith(" "));

            foreach (var s in arr)
            {
                if (s.Trim().Contains(" ")) continue;
                if (s.Length > 1)
                    foreach (var t in s)
                    {
                        if (t.ToString().Equals(what))
                            return t.ToString();
                    }
                else if (s.Equals(what))
                    return s;
            }

            return string.Empty;
        }

        public bool HasFlag(string flagName)
        {
            return Flags.Any(f => f.Name.Equals(flagName));
        }

        public void ShowHelp()
        {
            int maxOptLenght = Flags.Max(f => f.Name).Length;
            string hint = string.Empty;

            foreach (var flag in Flags)
            {
                if (flag.Help != null)
                {
                    hint = flag.Help.Hint ?? $"{(flag.Name.Length > 1 ? "--" : " -")}{flag.Name.PadRight(maxOptLenght)}\t ";
                    hint += flag.Help.Detail ?? "";
                }

                Console.WriteLine(hint);
            }
        }
    }
}