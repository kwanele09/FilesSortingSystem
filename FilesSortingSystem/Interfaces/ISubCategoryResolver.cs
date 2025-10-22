using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FilesSortingSystem.Interfaces
{
    public interface ISubCategoryResolver
    {
        string? GetSubDirectory(string category, string extension);
    }
}
