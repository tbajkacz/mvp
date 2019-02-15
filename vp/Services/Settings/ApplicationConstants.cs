using System;

namespace vp.Services.Settings
{
    public class ApplicationConstants
    {
        public static string FFmpegPath => $@"{AppDomain.CurrentDomain.BaseDirectory}\ffmpeg";

        public static double VolumeSliderMaxValue => 100;

        public static string FileDialogFilters => "Video files |*.wmv; *.avi; *.flv; *.mp4;";

        //{get;} = is evaluated only once, => on each request
        public static string[] SupportedVideoExtensions { get; } = FileDialogFilters.Remove(0, FileDialogFilters.IndexOf("|") + 1)
            .Replace("*", string.Empty).Replace(";", string.Empty).Split(' ');

        public static TimeSpan AutoSaveInterval => TimeSpan.FromSeconds(1);

        public static string NewPlaylistTitle => "Unnamed";

        public static string DefaultWindowTitle => "vp";
    }
}
