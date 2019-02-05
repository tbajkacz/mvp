using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using GalaSoft.MvvmLight;

namespace vp.Services.Settings
{
    public class ApplicationConstants
    {
        public static string FFmpegPath => $@"{AppDomain.CurrentDomain.BaseDirectory}\ffmpeg";

        public static double VolumeSliderMaxValue => 100;

        public static string FileDialogFilters => "Video files |*.wmv; *.avi; *.flv; *.mp4;";
    }
}
