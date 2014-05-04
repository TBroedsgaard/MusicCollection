using System;
using System.Collections.Generic;

using Common.Interfaces;
using DataAccess;

namespace Domain.Model
{
    internal class Artist : IArtist
    {
        /// <summary>
        /// Set name of Artist
        /// </summary>
        /// <exception cref="System.ArgumentOutOfRangeException">Thrown when artistName is an empty string or whitespace</exception>
        public string ArtistName 
        {
            get { return _artistEntity.ArtistName; }
            set 
            {
                validateArtistName(value);
                _artistEntity.ArtistName = value; 
            }
        }
        /// <summary>
        /// A list of all Songs attributed to Artist
        /// </summary>
        public IReadOnlyList<string> Songs
        {
            get { return _artistEntity.Songs; }
        }
        /// <summary>
        /// Add song to the Artist's list of songs
        /// </summary>
        /// <param name="song">Name of song to add</param>
        public void AddSong(string song)
        {
            validateSong(song);

            _artistEntity.AddSong(song);
        }

        /// <summary>
        /// The ArtistEntity object created by the persistence subsystem, wrapped by IArtist.
        /// Attributes on this object may only be set by the wrapping class! (Artist in this case).
        /// Otherwise, changes will not be validated against business rules.
        /// </summary>
        internal IArtist _artistEntity; 

        /// <summary>
        /// Initializes a new Artist object, and saves it in the persistence subsystem
        /// </summary>
        /// <param name="artistName">Name of artist</param>
        /// <param name="dataAccessFacade">Facade to persistence subsystem</param>
        /// <exception cref="System.ArgumentOutOfRangeException">Thrown when artistName is an empty string or whitespace</exception>
        internal Artist(string artistName, IDataAccessFacade dataAccessFacade)
        {
            validateArtistName(artistName);

            _artistEntity = dataAccessFacade.CreateArtist(artistName);
        }

        /// <summary>
        /// Reads all ArtistEntities from the persistence subsystem and creates a new Artist 
        /// representation for each entity
        /// </summary>
        /// <param name="dataAccessFacade">Facade to persistence subsystem</param>
        /// <returns>A list of all Artists stored in the persistence subsystem</returns>
        internal static List<Artist> ReadAll(IDataAccessFacade dataAccessFacade)
        {
            List<Artist> artists = new List<Artist>();
            List<IArtist> entities = dataAccessFacade.ReadAllArtists();

            foreach (IArtist entity in entities)
            {
                Artist artist = new Artist(entity, dataAccessFacade);
                artists.Add(artist);
            }

            return artists;
        }

        /// <summary>
        /// Writes changes to self to persistence
        /// </summary>
        internal void Update()
        {
            dataAccessFacade.UpdateArtist(_artistEntity);
        }

        /// <summary>
        /// Soft-deletes self in persistence
        /// </summary>
        internal void Delete()
        {
            dataAccessFacade.DeleteArtist(_artistEntity);
        }

        private IDataAccessFacade dataAccessFacade;

        /// <summary>
        /// Instantiates a new Artist from an ArtistEntity return from persistence
        /// </summary>
        /// <param name="artistEntity">ArtistEntity read from persistence, used as backing field</param>
        /// <param name="dataAccessFacade">Facade to persistence</param>
        private Artist(IArtist artistEntity, IDataAccessFacade dataAccessFacade)
        {
            _artistEntity = artistEntity;

            this.dataAccessFacade = dataAccessFacade;
        }

        /// <summary>
        /// Used for checking validity of artistName
        /// </summary>
        /// <param name="artistName">String to check if valid artistName</param>
        /// <exception cref="System.ArgumentOutOfRangeException">Thrown when artistName is an empty string or whitespace</exception>
        private void validateArtistName(string artistName)
        {
            string paramName = "ArtistName";
            if (String.IsNullOrWhiteSpace(artistName))
            {
                throw new ArgumentOutOfRangeException(paramName, "Name may not be empty");
            }
        }

        /// <summary>
        /// Used for checking validity of song
        /// </summary>
        private void validateSong(string song)
        {
            // test if song is valid, if not throw exception
        }
    }
}
