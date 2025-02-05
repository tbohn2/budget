namespace Budget_App.Models
{
    public class Year
    {
        public int Id { get; set; }
        public int YearValue { get; set; }
        public List<Month> Months { get; set; } = new List<Month>();
    }
}