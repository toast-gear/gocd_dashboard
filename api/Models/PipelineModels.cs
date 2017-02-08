using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace api.Models
{
    public class PipelineStatus
    {
        public string pausedCause { get; set; }
        public string pausedBy { get; set; }
        public Boolean paused { get; set; }
        public Boolean schedulable { get; set; }
        public Boolean locked { get; set; }
    }

    public class PausePipeline
    {
        public string pauseCause { get; set; }
    }
}
