using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using GalaSoft.MvvmLight;
using Newtonsoft.Json;
using vp.Extensions;

namespace vp.Models
{
    public class Video : ObservableObject
    {
        private string _path;
        private string _name;
        private double _length;
        private double _timeWatched;
        private bool _finished;

        [JsonProperty("path")]
        [JsonRequired]
        public string Path
        {
            get => _path;
            set { Set(() => Path, ref _path, value); }
        }

        [JsonProperty("name")]
        public string Name
        {
            get => _name;
            set { Set(() => Name, ref _name, value); }
        }

        [JsonProperty("length")]
        public double Length
        {
            get => _length;
            set { Set(() => Length, ref _length, value); }
        }

        [JsonProperty("timeWatched")]
        public double TimeWatched
        {
            get => _timeWatched;
            set { Set(() => TimeWatched, ref _timeWatched, value); }
        }

        [JsonProperty("finished")]
        public bool Finished
        {
            get => _finished;
            set { Set(() => Finished, ref _finished, value); }
        }


        private Video()
        {

        }

        public static Video CreateFromPath(string path)
        {
            Video video = new Video();
            video.Path = path;
            video.Name = System.IO.Path.GetFileNameWithoutExtension(path);
            video.Length = video.GetDuration().TotalSeconds;
            video.TimeWatched = 0;
            video.Finished = false;

            return video;
        }
    }
}
