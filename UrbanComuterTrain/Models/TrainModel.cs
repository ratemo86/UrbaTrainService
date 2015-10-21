using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace UrbanComuterTrain.Models
{
    public class TrainModel
    {
        public int TrainNO { get; set; }
        public int LineId { get; set; }
        public string TrainStatus { get; set; }
        public string LastTrainStop { get; set; }
        public string NextTrainStop { get; set; }

       

        public string CurrentLocation
        {
            get; set;
            
            
        }
        
        public int LineTrainIsPlying { get; set; }

        public LinkModel MainTrainLink { get; set; }
    }
}