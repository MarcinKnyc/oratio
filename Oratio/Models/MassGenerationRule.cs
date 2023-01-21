namespace Oratio.Models
{
    public class MassGenerationRule: Entity
    {
        public DateTime? RuleTerminationTime { get; set; }
        public DayOfWeek? DayOfWeek { get; set; }
        public bool IsActive { get; set; }
        public DateTime? Time { get; set; }
        public DateTime RuleStartTime { get; set; }
    }
}
