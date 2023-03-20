using System;
using System.Collections.Generic;

namespace Composition.Model;

public class SequenceBuilder
{
    private readonly List<string> _keys = new();
    
    private string? _result;
    public string? Character
    {
        get => _result;
        set {
            _result = value;
            OnChanged?.Invoke(this);
        }
    }
    
    public event Action<SequenceBuilder>? OnChanged;

    public void AddKey(Gdk.Key key)
    {
        _keys.Add(key.ToString());
        OnChanged?.Invoke(this);
    }
    
    public List<string> Keys => _keys;

    public void ClearKeys()
    {
        _keys.Clear();
        OnChanged?.Invoke(this);
    }

    public Sequence? Build()
    {
        if (_result == null || _keys.Count == 0) return null;
        return new Sequence(_keys, _result);   
    }
}