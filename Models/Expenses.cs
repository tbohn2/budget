namespace Budget_App.Models

{
    public class Expenses
    {
        public int Id { get; set; }
        public decimal Rent { get; set; } = 0;
        public decimal Tithing { get; set; } = 0;
        public decimal Fast { get; set; } = 0;
        public decimal Groceries { get; set; } = 0;
        public decimal Gas { get; set; } = 0;
        public decimal CarInsurance { get; set; } = 0;
        public decimal Medical { get; set; } = 0;
        public decimal EatOut { get; set; } = 0;
        public decimal Vacation { get; set; } = 0;
        public decimal Holiday { get; set; } = 0;
        public decimal Misc { get; set; } = 0;
        public int MonthId { get; set; } = 0;
        public Month Month { get; set; } = null!;
    }
}