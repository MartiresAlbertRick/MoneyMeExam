namespace MoneyMeExam.Entities
{
    public class Product
    {
        public long? ProductId { get; set; }
        public string ProductName { get; set; }
        public Enums.ProductType? ProductType { get; set; }
        public decimal? InterestRate { get; set; }
        public decimal? EstablishmentFee { get; set; }
    }
}