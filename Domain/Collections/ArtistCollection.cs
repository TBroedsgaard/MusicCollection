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

            artists = new List<Artist>();
        }

        internal Artist Create(string artistName)
        {
            Artist artist = new Artist(artistName, dataAccessFacade);
            artists.Add(artist);

            return artist;
        }
    }
}
