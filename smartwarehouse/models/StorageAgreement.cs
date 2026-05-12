using System;

namespace LogisticsWarehouse.Models
{
    public class StorageAgreement
    {
        public int AgreementId { get; set; }
        public int ClientId { get; set; }
        public int FacilityId { get; set; }
        public int ProductId { get; set; }
        public int Quantity { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public int CurrentStock { get; set; }
    }
}