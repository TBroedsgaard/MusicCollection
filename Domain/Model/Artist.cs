using System;

using Common.Interfaces;
using DataAccess;

namespace Domain.Model
{
    internal class Artist : IArtist
    {
        private IArtist _artistEntity;
        public string ArtistName 
        {
            get { return _artistEntity.ArtistName; }
            set 
            {
                validateArtistName(value);
                _artistEntity.ArtistName = value; 
            }
        }

        internal Artist(string artistName, IDataAccessFacade dataAccessFacade)
        {
            validateArtistName(artistName);

            _artistEntity = dataAccessFacade.CreateArtist(artistName);

            ArtistName = artistName;

            Update(dataAccessFacade);
        }

        internal Artist(IArtist artistEntity)
        {
            _artistEntity = artistEntity;
        }

        internal void Update(IDataAccessFacade dataAccessFacade)
        {
            dataAccessFacade.UpdateArtist(_artistEntity);
        }

        internal void Delete(IDataAccessFacade dataAccessFacade)
        {
            dataAccessFacade.DeleteArtist(_artistEntity);
        }

        private void validateArtistName(string artistName)
        {
            string paramName = "ArtistName";
            if (artistName.Equals(""))
            {
                throw new ArgumentOutOfRangeException(paramName, "Name may not be empty");
            }
        }
    }
}
