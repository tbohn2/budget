namespace Budget_App.Models

{
    public class Expenses
    {
        public int Id { get; set; }
        public decimal Rent { get; set; }
        public decimal Tithing { get; set; }
        public decimal Fast { get; set; }
        public decimal Groceries { get; set; }
        public decimal Gas { get; set; }
        public decimal CarInsurance { get; set; }
        public decimal Medical { get; set; }
        public decimal EatOut { get; set; }
        public decimal Vacation { get; set; }
        public decimal Holiday { get; set; }
        public decimal Misc { get; set; }
        public int MonthId { get; set; }
        public Month Month { get; set; } = null!;
    }
}