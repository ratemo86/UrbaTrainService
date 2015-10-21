using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace UrbanComuterTrain.Models
{
    public class StopModel
    {
        public int StopId { get; set; }
        public int LineId { get; set; }
        public string StopName { get; set; }

        public TrainModel NextTrain { get; set; }

        public LinkModel MainStopLink { get; set; }
    }
}