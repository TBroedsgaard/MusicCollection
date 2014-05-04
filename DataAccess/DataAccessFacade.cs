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

        /// <summary>
        /// Initializes a DataAccessFacade, reading the connection string and instantiating a new 
        /// ArtistMapper with the connection string
        /// </summary>
        public DataAccessFacade()
        {
            // TODO: Read from config file!!
            connectionString = 
                @"Data Source=ARYA\SQLEXPRESS;Initial Catalog=MusicCollection;Integrated Security=True";
            artistMapper = new ArtistMapper(connectionString);
        }

        /// <summary>
        /// Tells ArtistMapper to create a new ArtistEntity
        /// </summary>
        /// <param name="artistName">Name of Artist</param>
        /// <returns>ArtistEntity wrapped by IArtist</returns>
        public IArtist CreateArtist(string artistName)
        {
            return artistMapper.Create(artistName);
        }

        /// <summary>
        /// Reads the list of all ArtistEntities from ArtistMapper and converts the list to a list
        /// of IArtists
        /// </summary>
        /// <returns>A list of all ArtistEntities wrapped by IArtists</returns>
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

        /// <summary>
        /// Tells the ArtistMapper to update the given ArtistEntity
        /// </summary>
        /// <param name="artist">The ArtistEntity to update</param>
        public void UpdateArtist(IArtist artist)
        {
            ArtistEntity a = artist as ArtistEntity;
            artistMapper.Update(a);
        }

        /// <summary>
        /// Tells the ArtistMapper to soft-delete the given ArtistEntity
        /// </summary>
        /// <param name="artist">the ArtistEntity to soft-delete</param>
        public void DeleteArtist(IArtist artist)
        {
            ArtistEntity a = artist as ArtistEntity;
            artistMapper.Delete(a);
        }

    }
}
