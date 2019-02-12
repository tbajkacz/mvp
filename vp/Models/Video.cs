using System;
using GalaSoft.MvvmLight;
using Newtonsoft.Json;
using vp.Extensions;

namespace vp.Models
{
    public class Video : ObservableObject
    {
        private string _path;
        private string _name;
        private TimeSpan _length;
        private TimeSpan _timeWatched;
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
        public TimeSpan Length
        {
            get => _length;
            set { Set(() => Length, ref _length, value); }
        }

        [JsonProperty("timeWatched")]
        public TimeSpan TimeWatched
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

        [Obsolete("Videos should be created using the Video(string path) constructor")]
        public Video()
        {

        }

        public Video(string path)
        {
            Path = path;
            Name = System.IO.Path.GetFileNameWithoutExtension(path);
            TimeWatched = TimeSpan.FromSeconds(0);
            Length = this.GetDuration();
            Finished = false;
        }
    }
}
