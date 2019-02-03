using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GalaSoft.MvvmLight;

namespace vp.Services.Settings
{
    public class ApplicationSettings : ObservableObject, IApplicationSettings
    {
        public string FFmpegPath { get; }

        public ApplicationSettings()
        {
            FFmpegPath = Properties.ApplicationSettings.Default.FFmpegPath;
        }
    }
}
