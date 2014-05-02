using Common.Interfaces;
using System.Collections.Generic;
namespace DataAccess
{
    public interface IDataAccessFacade
    {
        IArtist CreateArtist(string artistName);
        List<IArtist> ReadAllArtists();
        void UpdateArtist(IArtist artist);
        void DeleteArtist(IArtist artist);
    }
}
