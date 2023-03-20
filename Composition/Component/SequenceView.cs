using Composition.Model;
using Gtk;

namespace Composition.Component;

public class SequenceView: HBox
{
    private Sequence _sequence;
    public Sequence Sequence
    {
        get => _sequence;
        set
        {
            _sequence = value;
            Update();
        }
    }
    
    private readonly KeysymView _keysymView = new() { Visible = true };
    private readonly Label _separator = new() { Visible = true, Text = "→" };
    private readonly Label _character = new() { Visible = true };

    public SequenceView()
    {
        Add(_keysymView);
        Add(_separator);
        Add(_character);
    }
    
    private void Update()
    {
        _keysymView.Update(_sequence.Keys);
        _character.Text = _sequence.Character.ToString();
    }
}