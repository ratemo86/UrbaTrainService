using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace UrbanTrainClient.Models
{

    public class LineLink
    {
        public int LinkId { get; set; }
        public string Rel { get; set; }
        public string Href { get; set; }
        public string Method { get; set; }
    }

    public class MainTrainLink
    {
        public int LinkId { get; set; }
        public string Rel { get; set; }
        public string Href { get; set; }
        public string Method { get; set; }
    }

    public class NextTrain
    {
        public int TrainNO { get; set; }
        public int LineId { get; set; }
        public string TrainStatus { get; set; }
        public object LastTrainStop { get; set; }
        public object NextTrainStop { get; set; }
        public string CurrentLocation { get; set; }
        public int LineTrainIsPlying { get; set; }
        public MainTrainLink MainTrainLink { get; set; }
    }

    public class MainStopLink
    {
        public int LinkId { get; set; }
        public string Rel { get; set; }
        public string Href { get; set; }
        public string Method { get; set; }
    }

    public class Stop
    {
        public int StopId { get; set; }
        public int LineId { get; set; }
        public string StopName { get; set; }
        public NextTrain NextTrain { get; set; }
        public MainStopLink MainStopLink { get; set; }
    }

    public class MainTrainLink2
    {
        public int LinkId { get; set; }
        public string Rel { get; set; }
        public string Href { get; set; }
        public string Method { get; set; }
    }

    public class Train
    {
        public int TrainNO { get; set; }
        public int LineId { get; set; }
        public string TrainStatus { get; set; }
        public string LastTrainStop { get; set; }
        public string NextTrainStop { get; set; }
        public string CurrentLocation { get; set; }
        public int LineTrainIsPlying { get; set; }
        public MainTrainLink2 MainTrainLink { get; set; }
    }

    public class RootObject
    {
        public int LineId { get; set; }
        public string LineStatus { get; set; }
        public LineLink LineLink { get; set; }
        public string LineName { get; set; }
        public List<Stop> Stops { get; set; }
        public List<Train> Trains { get; set; }
    }
    
}