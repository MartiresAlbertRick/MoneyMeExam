namespace MoneyMeExam.Entities
{
    public class MobileNumber
    {
        public long? MobileNumberId { get; set; } 
        public string Number { get; set; }
        public bool? IsBlackListed { get; set; }
    }
}