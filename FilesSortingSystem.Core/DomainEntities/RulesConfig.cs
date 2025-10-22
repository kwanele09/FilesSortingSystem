using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FilesSortingSystem.Core.DomainEntities
{
    public class RulesConfig
    {
        public Dictionary<string, string> SubFolders { get; set; } = new();
        public Dictionary<string, string> SpecialFolders { get; set; } = new();
    }
}
