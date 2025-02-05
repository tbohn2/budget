namespace Budget_App.Models

{
    public class Earnings
    {
        public int Id { get; set; }
        public decimal Primary { get; set; }
        public decimal Secondary { get; set; }
        public decimal Gifts { get; set; }
        public int MonthId { get; set; }
        public Month Month { get; set; } = null!;
    }
}