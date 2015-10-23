using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace UrbanTrainClient.Models
{
    public class LineRep
    {
        public int LineId { get; set; }
        public string LineName { get; set; }
        public string LineStatus { get; set; }

        public string Href { get; set; }

        public List<StopRep> LineStops { get; set; }
        public List<TrainRep> LineTrains { get; set; }
    }
}