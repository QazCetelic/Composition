using System;
using Composition.Model;
using Gtk;

namespace Composition.Component;

public class SequenceTreeView: TreeView
{
    public SequenceTreeView(): base()
    {
        TreeViewColumn keysymsColumn = new();
        keysymsColumn.Title = "Sequence";
        CellRendererText keysymsCell = new();
        keysymsColumn.PackStart(keysymsCell, true);

        TreeViewColumn characterColumn = new();
        characterColumn.Title = "Result";
        CellRendererText characterCell = new();
        characterCell.Style = Pango.Style.Oblique;
        characterColumn.PackStart(characterCell, true);
        
        TreeViewColumn toggleColumn = new();
        toggleColumn.Title = "Enabled";
        CellRendererToggle toggleCell = new();
        toggleColumn.PackStart(toggleCell, true);

        AppendColumn(keysymsColumn);
        AppendColumn(characterColumn);
        AppendColumn(toggleColumn);

        keysymsColumn.AddAttribute(keysymsCell, "text", 0);
        characterColumn.AddAttribute(characterCell, "text", 1);
        toggleColumn.AddAttribute(toggleCell, "active", 3);
        
        toggleCell.Toggled += (widget, path) =>
        {
            TreeIter iter;
            Model.GetIter(out iter, new TreePath(path.Path));
            bool enabled = (bool) Model.GetValue(iter, 3);
            Model.SetValue(iter, 3, !enabled);
        };
    }
}