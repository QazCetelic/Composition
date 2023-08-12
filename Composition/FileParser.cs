using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using Composition.Model;

namespace Composition;

public static class ComposeSequencesFileParser
{
    public static List<Sequence> Parse(string filePath)
    {
        string[] lines = File.ReadAllLines(filePath);
        return lines.Select(ParseLine).Where(sequence => sequence != null).ToList()!;
    }
    
    private static readonly Regex _whiteSpaceRegex = new("\\s+");
    private static readonly Regex _sequenceRegex = new("<Multi_key>\\s+(?<keysyms><[^>]+>(\\s+<[^>]+>)*)\\s*:\\s*\"(?<result>[^\"]+)\"(\\s+.+)?\\s*(#.+)?");
    private static Sequence? ParseLine(string line)
    {
        Match match = _sequenceRegex.Match(line);
        
        if (match.Success)
        {
            string[] keysyms = _whiteSpaceRegex
                .Split(match.Groups["keysyms"].Value)
                .Select(keysym => keysym[1..^1])
                .ToArray();
            string result = match.Groups["result"].Value;

            var sequence = new Sequence(keysyms, result);
            return sequence;
        }

        return null;
    }
}