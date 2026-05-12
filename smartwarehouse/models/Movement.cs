using System;

namespace LogisticsWarehouse.Models
{
    public class Movement
    {
        public int MovementId { get; set; }
        public int ProductId { get; set; }
        public int SourceSectionId { get; set; }
        public int DestinationSectionId { get; set; }
        public int EmployeeId { get; set; }
        public int QuantityMoved { get; set; }
        public DateTime Timestamp { get; set; }
        public string Notes { get; set; }
    }
}