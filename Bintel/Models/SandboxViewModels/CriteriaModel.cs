using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Bintel.Models.SandboxViewModels
{
    public class CriteriaModel
    {
        public int[] MetricIds { get; set; }

        public Period Period { get; set; }

        int PeriodCount { get; set; }

        public string LinkText { get; set; }

        public string LinkDescription { get; set; }

        public string QueryString { get; set; }


    }
}
