namespace FinanceTrackerApp.DTOs
{
    public class SummaryQueryDto
    {
        public int? Month { get; set; }
        public int? Year { get; set; }
        public bool HasFilter => Month.HasValue && Year.HasValue;
        public string Label => HasFilter
            ? new DateTime(Year!.Value, Month!.Value, 1).ToString("MMMM yyyy")
            : "All Time";
    }

}
