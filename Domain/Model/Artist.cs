using System;
using System.Collections.Generic;

using Common.Interfaces;
using DataAccess;

namespace Domain.Model
{
    internal class Artist : IArtist
    {
        public string ArtistName 
        {
            get { return _artistEntity.ArtistName; }
            set 
            {
                validateArtistName(value);
                _artistEntity.ArtistName = value; 
            }
        }

        public IReadOnlyList<string> Songs
        {
            get { return _songs; }
        }

        public void AddSong(string song)
        {
            validateSong(song);

            _songs.Add(song);
        }

        internal IArtist _artistEntity; // Skulle helst være privat, men det kan være nødvendigt at den er internal

        internal Artist(IArtist artistEntity, IDataAccessFacade dataAccessFacade)
        {
            _artistEntity = artistEntity;

            _songs = new List<string>();
            foreach (string song in Songs)
            {
                _songs.Add(song);
            }

            this.dataAccessFacade = dataAccessFacade;
        }

        internal Artist(string artistName, IDataAccessFacade dataAccessFacade)
        {
            validateArtistName(artistName);

            _artistEntity = dataAccessFacade.CreateArtist(artistName);
            _songs = new List<string>();

        }

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

        internal void Update()
        {
            dataAccessFacade.UpdateArtist(_artistEntity);
        }

        internal void Delete()
        {
            dataAccessFacade.DeleteArtist(_artistEntity);
        }

        private IDataAccessFacade dataAccessFacade;
        private List<string> _songs;

        private void validateArtistName(string artistName)
        {
            string paramName = "ArtistName";
            if (artistName.Equals(""))
            {
                throw new ArgumentOutOfRangeException(paramName, "Name may not be empty");
            }
        }

        private void validateSong(string song)
        {
            // test if song is valid, if not throw exception
        }
    }
}
