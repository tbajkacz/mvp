using System;
using System.Threading.Tasks;
using Microsoft.Win32;
using vp.Services.Settings;

namespace vp.Services.Dialogs
{
    public class FileDialogService : IFileDialogService
    {
        public async Task<string[]> OpenVideoFileDialogAsync()
        {
            return await Task.Run(() =>
                {
                    OpenFileDialog fileDialog = new OpenFileDialog();
                    fileDialog.CheckFileExists = true;
                    fileDialog.Multiselect = true;
                    fileDialog.Filter = ApplicationConstants.FileDialogFilters;
                    return fileDialog.ShowDialog() ?? false ? fileDialog.FileNames : Array.Empty<string>();
                });
        }

        //public string OpenTextDialog(string title = "", string message = "", string leftButtonText = "", string rightButtonText = "")
        //{
        //    OpenTextDialog textDialog = new OpenTextDialog();
        //    var result = textDialog.ShowDialog(title, message, leftButtonText, rightButtonText);
        //    return result.Result ?? false ? result.ReturnedString : null;
        //}

        //public async Task<string> OpenTextDialogAsync(string title = "", string message = "", string leftButtonText = "", string rightButtonText = "")
        //{
        //    OpenTextDialog textDialog = new OpenTextDialog();
        //    var result = await textDialog.ShowDialogAsync(title, message, leftButtonText, rightButtonText);
        //    return result.Result ?? false ? result.ReturnedString : null;
        //}
    }
}
