using LogisticsWarehouse.Models;
using System.Collections.Generic;

namespace LogisticsWarehouse.Repositories
{
    public interface IFacilityRepository
    {
        bool AddFacility(Facility facility);
        List<Facility> GetAllFacilities();
        Facility GetFacilityById(int id);
        bool UpdateFacility(Facility facility);
        bool DeleteFacility(int id);
    }
}