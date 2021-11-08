using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Z3._1
{
    public class ConnectionPolicySettings
    {
        public int ResponseWaitingTime { get; set; }
        public int MaxNumOfConAttempts { get; set; }
        public DataSources[] DataSources { get; set; }
    }
}
