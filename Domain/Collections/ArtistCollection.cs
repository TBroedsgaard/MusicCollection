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
                artists = new List<Artist>();
                List<IArtist> artistEntities = dataAccessFacade.ReadAllArtists();

                foreach (IArtist artistEntity in artistEntities)
                {
                    Artist artist = new Artist(artistEntity);
                    artists.Add(artist);
                }
            }

            return artists;
        }

        internal void Update(Artist artist)
        {
            artist.Update(dataAccessFacade);
        }

        internal void Delete(Artist artist)
        {
            artist.Delete(dataAccessFacade);
        }
    }
}
