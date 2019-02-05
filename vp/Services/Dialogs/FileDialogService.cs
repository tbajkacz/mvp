using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
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
    }
}
