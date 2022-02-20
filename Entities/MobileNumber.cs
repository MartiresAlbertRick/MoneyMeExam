namespace MoneyMeExam.Entities
{
    public class MobileNumber
    {
        public long? MobileNumberId { get; set; } 
        public string Number { get; set; }
        public string IsBlackListed { get; set; }
    }
}