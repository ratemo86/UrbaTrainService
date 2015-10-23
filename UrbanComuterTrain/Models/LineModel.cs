using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace UrbanComuterTrain.Models
{
    public class LineModel
    {

        public int LineId { get; set; }
       // public int LineStatusId { get; set; }
       
        public string LineStatus { get; set; }
        public LinkModel LineLink { get; set; }
      
        public string LineName { get; set; }
        public List<StopModel> Stops { get; set; }
        //public List<LinkModel> StopLinks { get; set; }
        public List<TrainModel> Trains { get; set; }
        //public List<LinkModel> TrainLinks { get; set; }
    }
}