using SQLite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FilesSortingSystem.Storage.DatabaseEntities
{
    public class FileSortRuleEntity
    {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }
        public string Extension { get; set; } = string.Empty;
        public string Category { get; set; } = string.Empty;
        public bool IsCaseSensitive { get; set; }
        public bool IsUserDefined { get; set; }
    }
}
