using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CosmenticFormulaApp.Infrastructure.Data.Configuration
{
    public class FileProcessingOptions
    {
        public const string SectionName = "FileProcessingOptions";
        public int MaxBatchSize { get; set; } = 10;
        public int TimeoutMinutes { get; set; } = 5;
    }
}
