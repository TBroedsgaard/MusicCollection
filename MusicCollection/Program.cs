using Common.Interfaces;
using Domain.Controllers;
using System;
using System.Collections.Generic;

namespace MusicCollection
{
    class Program
    {
        static void Main(string[] args)
        {
            Tester test = new Tester();
            test.Run();

            Console.ReadKey();
        }
    }

    class Tester
    {
        private UpdateCollectionHandler updateCollectionHandler;

        public Tester()
        {
            updateCollectionHandler = new UpdateCollectionHandler();
        
        }
        public void Run()
        {
            try
            {
                updateCollectionHandler.AddArtist("");
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                Console.WriteLine(ex.StackTrace);
            }

            //updateCollectionHandler.AddArtist("John Lennon");
            List<IArtist> artists = updateCollectionHandler.ReadAllArtists();

            foreach (IArtist artist in artists)
            {
                Console.WriteLine(artist.ArtistName);
            }

            //updateCollectionHandler.DeleteArtist(artists[0]);
            artists[0].ArtistName = "Michael Jackson";
            updateCollectionHandler.UpdateArtist(artists[0]);
        }
    }
}
