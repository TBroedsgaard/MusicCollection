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
            set { _artistEntity.ArtistName = value; }
        }

        internal Artist(string artistName, IDataAccessFacade dataAccessFacade)
        {
            validateName(artistName);

            _artistEntity = dataAccessFacade.CreateArtist();

            ArtistName = artistName;

            dataAccessFacade.UpdateArtist(_artistEntity);
        }

        private void validateName(string name)
        {
            string paramName = "ArtistName";
            if (name.Equals(""))
            {
                throw new ArgumentOutOfRangeException(paramName, "Name may not be empty");
            }
        }
    }
}
