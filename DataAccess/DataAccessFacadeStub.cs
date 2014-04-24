﻿using Common.Interfaces;
using DataAccess.Entities;
using System;
using System.Collections.Generic;
namespace DataAccess
{
    public class DataAccessFacadeStub : IDataAccessFacade
    {
        public IArtist CreateArtist()
        {
            return new ArtistEntity("", 0, DateTime.MinValue, false);
        }

        public List<IArtist> ReadAllArtists()
        {
            return new List<IArtist>();
        }

        public void UpdateArtist(IArtist artist)
        {
        }

        public void DeleteArtist(IArtist artist)
        {}
    }
}
