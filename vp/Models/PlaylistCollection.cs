using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using vp.Services.Settings;

namespace vp.Models
{
    public class PlaylistCollection : BindingList<Playlist>
    {
        public PlaylistCollection() : base()
        {
            
        }

        public PlaylistCollection(IEnumerable<Playlist> playlistCollection) : base(playlistCollection.ToList())
        {

        }

        public new void Add(Playlist playlist)
        {
            base.Add(playlist);
            //TODO check if needed: playlist.Videos.ListChanged += (sender, args) => this.OnListChanged(args);
        }

        /// <summary>
        /// Creates a new <see cref="Playlist"/> and adds it to the collection
        /// </summary>
        /// <param name="title"></param>
        /// <returns></returns>
        public Playlist Create(string title)
        {
            if (title == null || !CheckTitleUnique(title)) return null;

            Playlist playlist = new Playlist();
            playlist.PlaylistTitle = title;
            playlist.CurrentlyPlayingId = 0;

            this.Add(playlist);

            return playlist;

        }

        /// <summary>
        /// Determines if the passed <see cref="title"/> is unique (none of the playlists has such title)
        /// </summary>
        /// <param name="title"></param>
        /// <returns></returns>
        public bool CheckTitleUnique(string title)
        {
            if (title == null)
            {
                return false;
            }

            return this.All(p => p.PlaylistTitle != title);
        }


        public string GetUniqueNewPlaylistTitle()
        {
            var newTitle = ApplicationConstants.NewPlaylistTitle;
            int i = 1;
            while (!CheckTitleUnique(newTitle))
            {
                newTitle = ApplicationConstants.NewPlaylistTitle + i.ToString();
                i++;
            }

            return newTitle;
        }
    }
}
