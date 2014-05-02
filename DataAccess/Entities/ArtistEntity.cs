using Common.Interfaces;
using System;
using System.Collections.Generic;
namespace DataAccess.Entities
{
    internal class ArtistEntity : Entity, IArtist
    {
        public string ArtistName { get; set; }
        public IReadOnlyList<string> Songs 
        {
            get { return _songs; }
        }

        public ArtistEntity(string artistName, int id, DateTime lastModified, bool deleted)
            : base(id, lastModified, deleted)
        {
            ArtistName = artistName;
            _songs = new List<string>();
        }

        public void AddSong(string song)
        {
            _songs.Add(song);
        }

        private List<string> _songs;
    }
}
