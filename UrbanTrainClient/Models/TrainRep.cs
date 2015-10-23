using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace UrbanTrainClient.Models
{
    public class TrainRep
    {
        public int TrainNO { get; set; }
        public string CurrentLocation { get; set; }
        public string Href { get; set; }
    }
}