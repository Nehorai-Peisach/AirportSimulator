using Airport.BLL.Interfaces;
using Airport.DAL.Repositories;
using Airport.Models;
using System;
using System.Collections.Generic;

namespace Airport.BLL.Services
{
    public class StationService : IStationService
    {
        StationRepository repo;
        public StationService()
        {
            repo = new StationRepository();
        }

        public void Add(Station station)
        {
            if (station != default)
                repo.Add(station);
        }

        public List<Station> GetAll()
        {
            return repo.GetAll();
        }

        public void Update(Station station)
        {
            if (station != default)
                repo.Update(station);
        }
    }
}
