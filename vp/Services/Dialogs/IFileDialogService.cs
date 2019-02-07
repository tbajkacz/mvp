using System.Threading.Tasks;

namespace vp.Services.Dialogs
{
    /// <summary>
    /// Service used to display different types of dialogs
    /// </summary>
    public interface IFileDialogService
    {
        /// <summary>
        /// Opens a video selection dialog and returns paths of selected files
        /// </summary>
        Task<string[]> OpenVideoFileDialogAsync();

        ///// <summary>
        ///// Opens a text dialog window and returns user input if accepted or null if canceled
        ///// </summary>
        //string OpenTextDialog(string title = "", string message = "", string leftButtonText = "", string rightButtonText = "");

        ///// <summary>
        ///// Opens a text dialog window asynchronously and returns user input if accepted or null if canceled
        ///// </summary>
        //Task<string> OpenTextDialogAsync(string title = "", string message = "", string leftButtonText = "", string rightButtonText = "");
    }
}
