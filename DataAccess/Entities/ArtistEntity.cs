using Common.Interfaces;
using System;
namespace DataAccess.Entities
{
    internal class ArtistEntity : Entity, IArtist
    {
        public string ArtistName { get; set; }

        public ArtistEntity(string artistName, int id, DateTime lastModified, bool deleted)
            : base(id, lastModified, deleted)
        {
            ArtistName = artistName;
        }

    }
}
