using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using Composition.Model;

namespace Composition;

public static class FileParser
{
    public static List<Sequence> Parse(string filePath)
    {
        var sequences = new List<Sequence>();
        var lines = File.ReadAllLines(filePath);
        foreach (string line in lines)
        {
            Sequence? sequence = ParseLine(line);
            if (sequence != null)
            {
                sequences.Add(sequence);
            }
        }
        return sequences;
    }
    
    private static readonly Regex _whiteSpaceRegex = new Regex("\\s+");
    private static readonly Regex _sequenceRegex = new Regex("<Multi_key>\\s+(?<keysyms><[^>]+>(\\s+<[^>]+>)*)\\s*:\\s*\"(?<result>[^\"]+)\"(\\s+.+)?\\s*(#.+)?");
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