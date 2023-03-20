using System;
using Composition.Model;
using Gtk;
using Key = Gdk.Key;

namespace Composition.Component;

public class SequenceInput: HBox
{
    readonly SequenceBuilder builder;
    private readonly Button _clear = new();
    private readonly Button _keys = new();
    private readonly KeysymView _keysymsView = new();
    private readonly Entry _character = new();
    private readonly Button _complete = new();
    
    public event Action<Sequence>? CompletedEvent;
    
    public SequenceInput(SequenceBuilder builder)
    {
        this.builder = builder;

        Add(_clear);
        Add(_keys);
        _keys.Add(_keysymsView);
        Add(_character);
        Add(_complete);
        
        // Clear button
        _clear.Image = new Image(Stock.Clear, IconSize.Button);
        _clear.AlwaysShowImage = true;
        _clear.Clicked += (_, _) => builder.ClearKeys();
        // Apply button
        _complete.Image = new Image(Stock.Apply, IconSize.Button);
        _complete.AlwaysShowImage = true;
        _complete.Clicked += (_, _) => BuildSequence();
        // Key input
        _keys.KeyPressEvent += (_, e) => e.RetVal = true; // Prevent event propagation to the window
        _keys.KeyReleaseEvent += KeyInput;
        _keysymsView.NoneText = "No keys chosen\nPress a key to add it";
        // Character output
        _character.Alignment = 0.5f; // Center
        _character.StyleContext.AddClass(Resources.CSS_sequence_character);
        
        this.builder.OnChanged += _ => Update();
        Update();
    }

    public void KeyInput(object _, KeyReleaseEventArgs e)
    {
        // Completes the sequence when enter is pressed
        if (e.Event.Key == Key.Return)
        {
            _complete.Click();
        }
        // Cancel input
        else if (e.Event.Key == Key.Escape)
        {
            _clear.Click();
        }
        // Appends the key to the sequence
        else
        {
            builder.AddKey(e.Event.Key);
        }
    }

    private void BuildSequence()
    {
        Sequence sequence = builder.Build()!;
        CompletedEvent?.Invoke(sequence);
        builder.ClearKeys();
    }

    private void Update()
    {
        _keysymsView.Update(builder.Keys);
        _character.Text = builder.Character?.ToString() ?? "";
    }
}