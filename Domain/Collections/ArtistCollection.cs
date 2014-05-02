using Common.Interfaces;
using DataAccess;
using Domain.Model;
using System.Collections.Generic;
namespace Domain.Collections
{
    internal class ArtistCollection
    {
        IDataAccessFacade dataAccessFacade;
        List<Artist> artists;

        internal ArtistCollection(IDataAccessFacade dataAccessFacade)
        {
            this.dataAccessFacade = dataAccessFacade;

            ReadAll();
        }

        internal Artist Create(string artistName)
        {
            Artist artist = new Artist(artistName, dataAccessFacade);
            artists.Add(artist);

            return artist;
        }

        internal List<Artist> ReadAll()
        {
            if (artists == null)
            {
                artists = Artist.ReadAll(dataAccessFacade);
            }

            return artists;
        }

        internal void Update(Artist artist)
        {
            artist.Update();
        }

        internal void Delete(Artist artist)
        {
            artist.Delete();
        }
    }
}
