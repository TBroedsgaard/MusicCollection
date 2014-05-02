using System.Collections.Generic;
namespace Common.Interfaces
{
    public interface IArtist
    {
        string ArtistName { get; set; }
        IReadOnlyList<string> Songs { get; }

        void AddSong(string song);
    }
}
