namespace Budget_App.Models

{
    public class Month
    {
        public int Id { get; set; }
        public required string Name { get; set; }
        public Earnings Earnings { get; set; }
        public Expenses Expenses { get; set; }
        public required int YearId { get; set; }
        public required Year Year { get; set; } = null!;

        public Month()
        {
            Expenses = new Expenses
            {
                MonthId = this.Id,
                Month = this
            };

            Earnings = new Earnings
            {
                MonthId = this.Id,
                Month = this
            };
        }
    }
}