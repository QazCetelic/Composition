using Composition.Component;
using Composition.Model;
using Gdk;
using Gtk;
using Window = Gtk.Window;

namespace Composition;

class Program
{
    public static void Main(string[] args)
    {
        Application.Init();

        var cssProvider = new CssProvider();
        cssProvider.LoadFromData(Resources.Style);
        StyleContext.AddProviderForScreen(Screen.Default, cssProvider, 800);

        var window = new Window(Resources.Name);
        window.SetSizeRequest(600, 800);
        window.IconName = "input-keyboard";
        window.DeleteEvent += (_, _) => Application.Quit();

        var store = new SequenceTreeStore();
        
        var mainBox = new VBox();
        window.Add(mainBox);

        var buttonBox = new FileButtons(window);
        buttonBox.Import += filePath => store.LoadFile(filePath);
        buttonBox.Export += filePath => store.SaveFile(filePath);
        mainBox.PackStart(buttonBox, false, false, 0);

        var scrollable = new ScrolledWindow();
        mainBox.Add(scrollable);
        
        var tree = new SequenceTreeView { Model = store };
        scrollable.Add(tree);
        
        // var box = new VBox();
        // window.Add(box);
        // var builder = new SequenceBuilder();
        // builder.Character = "a";
        //
        // var input = new SequenceInput(builder);
        // input.HeightRequest = 50;
        // input.CompletedEvent += sequence =>
        // {
        //     var view = new SequenceView { Sequence = sequence };
        //     view.Visible = true;
        //     box.Add(view);
        // };
        // box.Add(input);

        window.ShowAll();
        Application.Run();
    }
}