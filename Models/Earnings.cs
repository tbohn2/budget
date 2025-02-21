namespace Budget_App.Models

{
    public class Earnings
    {
        public int Id { get; set; }
        public decimal Primary { get; set; } = 0;
        public decimal Secondary { get; set; } = 0;
        public decimal Gifts { get; set; } = 0;
        public int MonthId { get; set; } = 0;
        public Month Month { get; set; } = null!;
    }
}