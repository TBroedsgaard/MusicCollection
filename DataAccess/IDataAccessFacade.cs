using Common.Interfaces;
namespace DataAccess
{
    public interface IDataAccessFacade
    {
        IArtist CreateArtist();
        void UpdateArtist(IArtist artist);
    }
}
