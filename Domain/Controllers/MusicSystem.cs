using Common.Interfaces;
using DataAccess;
using Domain.Collections;
using Domain.Model;
using System.Collections.Generic;
namespace Domain.Controllers
{
    public class MusicSystem
    {
        IDataAccessFacade dataAccessFacade;
        ArtistCollection artistCollection;

        public MusicSystem()
        {
            dataAccessFacade = new DataAccessFacade();

            artistCollection = new ArtistCollection(dataAccessFacade);
        }

        public IArtist AddArtist(string artistName)
        {
            return artistCollection.Create(artistName);
        }

        public List<IArtist> ReadAllArtists()
        {
            List<IArtist> artists = new List<IArtist>();
            foreach (Artist artist in artistCollection.ReadAll())
            {
                artists.Add(artist);
            }

            return artists;
        }

        public void DeleteArtist(IArtist artist)
        {
            Artist a = artist as Artist;
            artistCollection.Delete(a);
        }

        public void UpdateArtist(IArtist artist)
        {
            Artist a = artist as Artist;
            artistCollection.Update(a);
        }
    }
}
