using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace Composition.Model;

public class Sequence
{
    private static readonly Regex _keyRegex = new Regex("^KEY_");
    public static string GdkToKeysym(Gdk.Key key) => _keyRegex.Replace(key.ToString(), "");
    
    public string[] Keys { get; }
    public string Character { get; }
    
    public Sequence(IReadOnlyList<string> keys, string character)
    {
        Keys = keys.ToArray();
        Character = character;
    }

    public override string ToString() => Serialize(0);

    public string Serialize(int leftMinWidth)
    {
        string keys = string.Join(" ", Keys.Select(key => $"<{key}>"));
        string start = $"<Multi_key> {keys} ";
        string end = $": \"{Character}\"";
        return start.PadRight(leftMinWidth, ' ') + end;
    }
    
    public int GetLeftMinWidth()
    {
        int length = "<Multi_key>".Length;
        foreach (string key in Keys)
        {
            // +3 for the < and > and the space
            length += key.Length + 3;
        }
        length += 1; // for the space after the keys
        return length;
    }
}