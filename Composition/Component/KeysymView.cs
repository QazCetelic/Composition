using System.Collections.Generic;
using System.Linq;
using Gtk;
using Key = Gdk.Key;

namespace Composition.Component;

public class KeysymView: HBox
{
    private readonly Label _noneLabel = new();
    public string NoneText
    {
        get => _noneLabel.Text;
        set => _noneLabel.Text = value;
    }
    
    public KeysymView()
    {
        
    }
    
    public void Update(IReadOnlyCollection<string> keys)
    {
        Foreach(child => Remove(child));

        string[] keyArray = keys.ToArray();
        if (keyArray.Length > 0)
        {
            for (var i = 0; i < keyArray.Length; i++)
            {
                Label label = new(keyArray[i]);
                label.Visible = true;
                label.StyleContext.AddClass("sequence-key");
            
                // Separator
                if (i != 0)
                {
                    Label seperator = new(" + ");
                    seperator.Visible = true;
                    seperator.StyleContext.AddClass("sequence-seperator");
                    Add(seperator);
                }

                // Add the label to the box
                Add(label);
            }
        }
        else
        {
            Label label = new("No keys chosen\nPress a key to add it");
            label.Visible = true;
            label.StyleContext.AddClass("sequence-seperator");
            Add(label);
        }
    }
}