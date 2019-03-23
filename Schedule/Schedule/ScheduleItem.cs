using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Schedule
{
    class ScheduleItem
    {
        public string partiesId { get; set; }
        public string partiesName { get; set; }
        public string machineTools { get; set; }
        public string machineToolsId { get; set; }
        public string startTime { get; set; }
        public string endTime { get; set; }
    }
}
