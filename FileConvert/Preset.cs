using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FileConvert
{
    public class Preset
    {
        public string Name { get; set; }
        public string InputFormat { get; set; }
        public string InputFolder { get; set; }
        public string OutputFormat { get; set; }
        public string OutputFolder { get; set; }

        public Preset() { }
    }
}
