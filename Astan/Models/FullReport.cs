using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Astan.Models
{
    public class FullReport
    {
        public int ClientsCount { get; set; }
        public int ClinetMemberCount { get; set; }
        public int Count
        {
            get
            {
                return ClientsCount + ClinetMemberCount;
            }
        }
        public int SolvedCount { get; set; }
    }
}