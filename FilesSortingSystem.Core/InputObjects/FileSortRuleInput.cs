namespace FilesSortingSystem.Core.InputObjects
{
    public class FileSortRuleInput
    {
        public string Extension { get; set; } = string.Empty;
        public string Category { get; set; } = string.Empty;
        public bool IsCaseSensitive { get; set; }
    }
}
