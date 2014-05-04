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
        private MusicSystem musicSystem;

        public Tester()
        {
            musicSystem = new MusicSystem();
        
        }
        public void Run()
        {
            // Test that validation throws exception, that can be caught by User Interface
            try
            {
                musicSystem.AddArtist("");
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                Console.WriteLine(ex.StackTrace);
            }

            musicSystem.AddArtist("John Lennon");
            List<IArtist> artists = musicSystem.ReadAllArtists();

            foreach (IArtist artist in artists)
            {
                Console.WriteLine(artist.ArtistName);
            }

            //updateCollectionHandler.DeleteArtist(artists[0]);
            artists[0].ArtistName = "Michael Jackson";
            artists[0].AddSong("Bad");
            artists[0].AddSong("Billy Jeans");
            artists[0].AddSong("Ghost");
            IReadOnlyList<string> songs = artists[0].Songs;
            foreach (string song in songs)
            {
                Console.WriteLine(song);
            }
            musicSystem.UpdateArtist(artists[0]);
        }
    }
}
