namespace Oratio.Models
{
    public class MassGenerationRule: Entity
    {
        public Parish? Parish { get; set; }
        public int? TimesToRepeat { get; set; }
        public string? TimespanToRepeat { get; set; }
        public DateTime? RuleTerminationTime { get; set; }
        public DayOfWeek? DayOfWeek { get; set; }
        public int? WeekNumber { get; set; }
        public bool IsActive { get; set; }
        public DateTime RuleStartTime { get; set; }
    }
}
