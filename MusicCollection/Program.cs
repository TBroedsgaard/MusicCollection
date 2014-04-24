using Domain.Controllers;
using System;

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
        }
    }
}
