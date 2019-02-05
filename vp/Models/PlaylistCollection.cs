using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace vp.Models
{
    public class PlaylistCollection : ObservableCollection<Playlist>
    {
        public PlaylistCollection() : base()
        {

        }

        public PlaylistCollection(IEnumerable<Playlist> playlistCollection) : base(playlistCollection)
        {

        }

        /// <summary>
        /// Creates a new <see cref="Playlist"/> and adds it to the collection
        /// </summary>
        /// <param name="title"></param>
        /// <returns></returns>
        public Playlist Create(string title)
        {
            Playlist playlist = new Playlist();
            playlist.PlaylistTitle = title;
            playlist.CurrentlyPlayingId = 0;

            this.Add(playlist);

            return playlist;
        }
    }
}
