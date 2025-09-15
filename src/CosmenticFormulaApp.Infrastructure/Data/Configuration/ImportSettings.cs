using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CosmenticFormulaApp.Infrastructure.Data.Configuration
{
    public class ImportSettings
    {
        public const string SectionName = "ImportSettings";
        public string WatchFolder { get; set; } = "ImportFolder";
        public string FilePattern { get; set; } = "*.json";
        public int ProcessingInterval { get; set; } = 5000;
    }
}
