using System;
using Composition.Model;
using Gtk;

namespace Composition.Component;

public class FileButtons: HBox
{
    /// Event to trigger when a file path is chosen for exporting
    public event Action<string>? Export;
    /// Event to trigger when a file path is chosen for importing
    public event Action<string>? Import;
    
    private readonly Button _exportButton = new(Localization.Export_Button);
    private readonly Button _importButton = new(Localization.Import_Fragment_Button);
    private readonly Button _importFolderButton = new(Localization.Import_Folder_Button);

    public FileButtons(Window window)
    {
        Add(_exportButton);
        Add(_importButton);
        Add(_importFolderButton);

        const int buttonHeight = 50;
        _exportButton.HeightRequest = buttonHeight;
        _importButton.HeightRequest = buttonHeight;
        _importFolderButton.HeightRequest = buttonHeight;

        _exportButton.Image = new Image(Stock.Save, IconSize.Button);
        _importButton.Image = new Image(Stock.File, IconSize.Button);
        _importFolderButton.Image = new Image(Stock.Directory, IconSize.Button);

        var exportDialog = new FileChooserDialog(Localization.Dialog_Export, window, FileChooserAction.Save, Localization.Dialog_Cancel, ResponseType.Cancel, Localization.Dialog_Export, ResponseType.Apply);
        _exportButton.Clicked += (_, _) => exportDialog.Run();
        exportDialog.Response += (_, response) =>
        {
            if (response.ResponseId == ResponseType.Apply)
            {
                string filePath = exportDialog.Filename;
                if (filePath != null)
                {
                    Export?.Invoke(filePath);
                }
            }
            exportDialog.Hide();
        };
        
        var fileFilter = new FileFilter();
        fileFilter.AddPattern("*.compose");
        fileFilter.Name = Localization.File_Filter_Description;

        var importDialog = new FileChooserDialog(Localization.Dialog_Import, window, FileChooserAction.Open, Localization.Dialog_Cancel, ResponseType.Cancel, Localization.Dialog_Import, ResponseType.Apply);
        importDialog.AddFilter(fileFilter);
        _importButton.Clicked += (_, _) => importDialog.Run();
        importDialog.Response += (_, response) =>
        {
            if (response.ResponseId == ResponseType.Apply)
            {
                string filePath = importDialog.Filename;
                if (filePath != null)
                {
                    Import?.Invoke(filePath);
                }
            }
            importDialog.Hide();
        };

        var importFolderDialog = new FileChooserDialog(Localization.Dialog_Import, window, FileChooserAction.SelectFolder, Localization.Dialog_Cancel, ResponseType.Cancel, Localization.Dialog_Import, ResponseType.Apply);
        _importFolderButton.Clicked += (_, _) => importFolderDialog.Run();
        importFolderDialog.Response += (_, response) =>
        {
            if (response.ResponseId == ResponseType.Apply)
            {
                string filePath = importFolderDialog.Filename;
                if (filePath != null)
                {
                    Import?.Invoke(filePath);
                }
            }
            importFolderDialog.Hide();
        };
    }
}