namespace LogisticsWarehouse.Models
{
    public class Section
    {
        public int SectionId { get; set; }
        public int FacilityId { get; set; }
        public string SectionName { get; set; }
        public string SectionType { get; set; }
        public decimal Capacity { get; set; }
        public decimal CurrentOccupancy { get; set; }
    }
}