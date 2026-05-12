namespace LogisticsWarehouse.Models
{
    public class Product
    {
        public int ProductId { get; set; }
        public string Name { get; set; }
        public decimal Length { get; set; }
        public decimal Width { get; set; }
        public decimal Height { get; set; }
        public decimal Weight { get; set; }
        public int ManufacturerId { get; set; }
        public int IndustryGroupId { get; set; }
        public string Description { get; set; }
    }
}