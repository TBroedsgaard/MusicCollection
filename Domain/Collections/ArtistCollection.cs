using Common.Interfaces;
using DataAccess;
using Domain.Model;
using System.Collections.Generic;
namespace Domain.Collections
{
    /// <summary>
    /// Provides a Collection that controls a list of Artists
    /// </summary>
    internal class ArtistCollection
    {
        /// <summary>
        /// Initializes an ArtistCollection, instantiates the dataAccessFacade field and reads all
        /// artists from the dataAccessFacade
        /// </summary>
        /// <param name="dataAccessFacade">A Facade to the persistence subsystem</param>
        internal ArtistCollection(IDataAccessFacade dataAccessFacade)
        {
            this.dataAccessFacade = dataAccessFacade;

            ReadAll();
        }

        /// <summary>
        /// Instantiates a new Artist
        /// </summary>
        /// <param name="artistName">Name of Artist</param>
        /// <returns>The new Artist</returns>
        /// <exception cref="System.ArgumentOutOfRangeException">Thrown when artistName is an empty string or whitespace</exception>
        internal Artist Create(string artistName)
        {
            Artist artist = new Artist(artistName, dataAccessFacade);
            artists.Add(artist);

            return artist;
        }

        /// <summary>
        /// Reads the list of all Artists. If the list has not yet been initialized, it initializes
        /// it and fills it with data from the database.
        /// </summary>
        /// <returns>A list containing all Artists</returns>
        internal List<Artist> ReadAll()
        {
            if (artists == null)
            {
                artists = Artist.ReadAll(dataAccessFacade);
            }

            return artists;
        }

        /// <summary>
        /// Writes changes in Artist to the database
        /// </summary>
        /// <param name="artist">The Artist to update</param>
        internal void Update(Artist artist)
        {
            artist.Update();
        }

        /// <summary>
        /// Soft-deletes the specified Artist
        /// </summary>
        /// <param name="artist">The Artist to delete</param>
        internal void Delete(Artist artist)
        {
            artist.Delete();
            artists.Remove(artist);
        }

        private IDataAccessFacade dataAccessFacade;
        private List<Artist> artists;
    }
}
