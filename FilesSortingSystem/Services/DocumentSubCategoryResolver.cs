using FilesSortingSystem.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FilesSortingSystem.Services
{
    public class DocumentSubCategoryResolver : ISubCategoryResolver
    {
        public string? GetSubDirectory(string category, string extension)
        {
            if (!category.Equals("documents", StringComparison.OrdinalIgnoreCase))
                return null;

            return extension.ToLowerInvariant() switch
            {
                ".pdf" => "PDF",
                ".doc" or ".docx" => "Word",
                ".xls" or ".xlsx" => "Excel",
                ".ppt" or ".pptx" => "PowerPoint",
                ".txt" => "TextFiles",
                _ => null
            };
        }
    }
}
