namespace Budget_App.Models

{
    public class Month
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public Earnings Earnings { get; set; } = new Earnings();
        public Expenses Expenses { get; set; } = new Expenses();
        public int YearId { get; set; }
        public Year Year { get; set; } = null!;
    }
}