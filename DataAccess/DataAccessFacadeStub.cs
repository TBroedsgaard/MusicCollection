using Common.Interfaces;
using DataAccess.Entities;
using System;
namespace DataAccess
{
    public class DataAccessFacadeStub : IDataAccessFacade
    {
        public IArtist CreateArtist()
        {
            return new ArtistEntity("", 0, DateTime.MinValue, false);
        }

        public void UpdateArtist(IArtist artist)
        {
        }
    }
}
