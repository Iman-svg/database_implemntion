namespace LogisticsWarehouse.Models
{
    public class Facility
    {
        public int FacilityId { get; set; }
        public string Name { get; set; }
        public string Zone { get; set; }
        public string ClimateControl { get; set; }
        public decimal TotalCapacity { get; set; }
        public decimal CurrentOccupancy { get; set; }
        public string Address { get; set; }
    }
}