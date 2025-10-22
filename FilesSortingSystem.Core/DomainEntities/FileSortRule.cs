namespace FilesSortingSystem.Core.DomainEntities
{
    public class FileSortRule
    {
        public int Id { get; set; }
        public string Extension { get; set; } = string.Empty;
        public string Category { get; set; } = string.Empty;
        public bool IsCaseSensitive { get; set; }
    }
}
