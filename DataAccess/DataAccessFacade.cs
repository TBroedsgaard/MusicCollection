using Common.Interfaces;
using DataAccess.Entities;
using DataAccess.Mappers;
using System.Collections.Generic;

namespace DataAccess
{
    public class DataAccessFacade : IDataAccessFacade
    {
        string connectionString;
        ArtistMapper artistMapper;

        public DataAccessFacade()
        {
            // TODO: Read from config file!!
            connectionString = 
                @"Data Source=ARYA\SQLEXPRESS;Initial Catalog=MusicCollection;Integrated Security=True";
            artistMapper = new ArtistMapper(connectionString);
        }

        public IArtist CreateArtist()
        {
            return artistMapper.Create();
        }

        public List<IArtist> ReadAllArtists()
        {
            List<IArtist> artists = new List<IArtist>();
            List<ArtistEntity> artistEntities = artistMapper.ReadAll();
            foreach (ArtistEntity artistEntity in artistEntities)
            {
                artists.Add(artistEntity);
            }

            return artists;
        }

        public void UpdateArtist(IArtist artist)
        {
            ArtistEntity a = artist as ArtistEntity;
            artistMapper.Update(a);
        }

        public void DeleteArtist(IArtist artist)
        {
            ArtistEntity a = artist as ArtistEntity;
            artistMapper.Delete(a);
        }

    }
}
