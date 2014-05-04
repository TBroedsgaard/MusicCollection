using Common.Interfaces;
using DataAccess;
using Domain.Collections;
using Domain.Model;
using System;
using System.Collections.Generic;
namespace Domain.Controllers
{
    /// <summary>
    /// Provides a Facade Controller for MusicCollection
    /// </summary>
    public class MusicSystem
    {
        /// <summary>
        /// Initializes a new MusicSystem instance, and instantiates a new DataAccessFacade and
        /// ArtistCollection.
        /// </summary>
        public MusicSystem()
        {
            dataAccessFacade = new DataAccessFacade();

            artistCollection = new ArtistCollection(dataAccessFacade);
        }

        /// <summary>
        /// Creates a new instance of Artist with ArtistName set to artistName
        /// </summary>
        /// <param name="artistName">Name of artist</param>
        /// <returns>Artist wrapped by IArtist</returns>
        /// <exception cref="System.ArgumentOutOfRangeException">Thrown when artistName is an empty string or whitespace</exception>
        public IArtist AddArtist(string artistName)
        {
            return artistCollection.Create(artistName);
        }

        /// <summary>
        /// Reads the list of all Artists
        /// </summary>
        /// <returns>A list of Artists wrapped by IArtist</returns>
        public List<IArtist> ReadAllArtists()
        {
            List<IArtist> artists = new List<IArtist>();
            foreach (Artist artist in artistCollection.ReadAll())
            {
                artists.Add(artist);
            }

            return artists;
        }

        /// <summary>
        /// Soft-deletes the specified Artist
        /// </summary>
        /// <param name="artist">The Artist to delete</param>
        public void DeleteArtist(IArtist artist)
        {
            Artist a = artist as Artist;
            artistCollection.Delete(a);
        }

        /// <summary>
        /// Writes changes in Artist to the database
        /// </summary>
        /// <param name="artist">The Artist to update</param>
        public void UpdateArtist(IArtist artist)
        {
            Artist a = artist as Artist;
            artistCollection.Update(a);
        }

        private IDataAccessFacade dataAccessFacade;
        private ArtistCollection artistCollection;
    }
}
