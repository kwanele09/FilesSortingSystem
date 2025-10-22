namespace FilesSortingSystem.DomainEntities
{
    public class LogEntry
    {
        public Guid Id { get; set; }
        public bool IsMoved { get; set; }
        public string Message { get; set; } = string.Empty;
        public DateTime MoveDateTime { get; set; }
    }
}
