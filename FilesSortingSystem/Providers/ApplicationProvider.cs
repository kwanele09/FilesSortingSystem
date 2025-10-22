using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FilesSortingSystem.Interfaces;

namespace FilesSortingSystem.Providers
{
    public class ApplicationProvider : IApplicationProvider
    {
        public Application CurrentApplication => Application.Current!;
    }
}
