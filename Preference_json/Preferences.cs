using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Preference_json
{
    
    class Preference
    {
        public int ID;
        public string ReportName;
        public string Name;
        public string Value;

        public Preference(int id, string reportName, string name, string value)
        {
            ID = id;
            ReportName = reportName;
            Name = name;
            Value = value;
        }

    }
}
