using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FilesSortingSystem.Models
{
    public class LogEntry
    {
        public bool IsMoved { get; set; }
        public string Message { get; set; } = string.Empty;
    }
}
