using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace UrbanTrainClient.Models
{
    public class StopRep
    {
        public int StopID { get; set; }
        public string StopName { get; set; }
        public int NextTrain { get; set; }
        public string Href { get; set; }

    }
}