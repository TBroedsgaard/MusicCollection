using Common.Interfaces;
using DataAccess;
using Domain.Collections;
namespace Domain.Controllers
{
    public class UpdateCollectionHandler
    {
        IDataAccessFacade dataAccessFacade;
        ArtistCollection artistCollection;

        public UpdateCollectionHandler()
        {
            dataAccessFacade = new DataAccessFacade();

            artistCollection = new ArtistCollection(dataAccessFacade);
        }

        public IArtist AddArtist(string artistName)
        {
            return artistCollection.Create(artistName);
        }
    }
}
