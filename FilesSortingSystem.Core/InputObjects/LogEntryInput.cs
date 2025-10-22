namespace FilesSortingSystem.Core.InputObjects
{
    public class LogEntryInput
    {
        public bool IsMoved { get; set; }
        public string Message { get; set; } = string.Empty;
        public DateTime MoveDateTime { get; set; }
    }
}
