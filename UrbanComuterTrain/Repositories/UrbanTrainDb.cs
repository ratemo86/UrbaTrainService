using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using UrbanComuterTrain.Models;

namespace UrbanComuterTrain.Repositories
{
    public class UrbanTrainDb
    {
        private readonly UTEntities _urbanTrainDatabase;
        public UrbanTrainDb(UTEntities context)
        {
            _urbanTrainDatabase = context;
        }


        public UTEntities UrbanTrainDatabase
        {
            get { return _urbanTrainDatabase; }

        }

        public LineModel GetLine(int lineId)
        {
            return GetAllLines().Where(x=>x.LineId==lineId).First();

        }
        public List<LineModel> GetAllLines()
        {
            List<LineModel> lineModels = UrbanTrainDatabase.Lines.Select(x => new LineModel
            {
                LineId = x.LineId,
                LineName = x.LineName,
                LineStatusId = x.LineStatusId,
            }).ToList();
            return lineModels;

        }

        public LinkModel GetLinkforLine(LineModel linemodel)
        {
            return new LinkModel
            {
                Href = "http://localhost:60013/api/Lines/" + linemodel.LineId,
                Rel = "Line NO " + linemodel.LineName,
                Method = "GET"

            };
        }

        
        public List<TrainModel> GetTainsForLine(int lineId)
        {
            return GetAllTrains().Where(x => x.LineId == lineId).ToList<TrainModel>();
        }

        public List<TrainModel> GetAllTrains()
        {
            var allTrainModels = new List<TrainModel>();

            foreach (var train in UrbanTrainDatabase.Trains)
            {
                var trainStatus = GetTrainStatus(train.TrainStatusId);

                
                   
                
                var trainModel = new TrainModel
                {
                    TrainNO = train.TrainNO,
                    LineId = train.LineId,
                    LineTrainIsPlying = train.LineId,
                    NextTrainStop =train.NextStop,
                    LastTrainStop= train.LastStop,
                    TrainStatus = trainStatus,
                    MainTrainLink = new LinkModel
                    {
                        Href = "http://localhost:60013/api/Trains/" + train.TrainNO,
                        Rel = "Train " + train.TrainNO,
                        Method = "GET"
                    }


                };
                trainModel.CurrentLocation = GetTrainCurrentLocation(trainModel);
                allTrainModels.Add(trainModel);
            }
            return allTrainModels;
        }

        public List<StopModel> GetStopsForLine(int lineId)
        {

            return GetAllStops().Where(x => x.LineId == lineId).ToList<StopModel>();
        }

        public List<StopModel> GetAllStops()
        {
            var allStopModels = new List<StopModel>();

            foreach (var stop in UrbanTrainDatabase.Stops)
            {

                var stopModel = new StopModel();
                    stopModel.StopId = stop.StopId;
                    stopModel.LineId = stop.LineId;
                    stopModel.StopName = stop.StopName;
                    stopModel.MainStopLink = new LinkModel
                    {
                        Href = "http://localhost:60013/api/Stops/" + stop.StopId,
                        Rel = stop.StopName,
                        Method = "GET"
                    };
                    stopModel.NextTrain = GetStopNextTrain(stop.StopId);
                    allStopModels.Add(stopModel);
                

                //allStopModels.Add(new StopModel
                //{
                //    StopId = stop.StopId,
                //    LineId = stop.LineId,
                //    StopName = stop.StopName,
                //    MainStopLink = new LinkModel 
                //    {
                //        Href = "http://localhost:60013/api/Stops/" + stop.StopId,
                //        Rel = stop.StopName,
                //        Method = "GET"
                //    }


                //});
            }
            return allStopModels;
        }


        public string GetTrainStatus(int trainStatusId)
        {

            return UrbanTrainDatabase.TrainStatus.Where(x=>x.TrainStatusId== trainStatusId).Select(y=>y.CurrentStatus).First();
        }
        public TrainModel GetStopNextTrain(int stopId)
        {
            var netxTimePoint = DateTime.Now.Hour + 1;
            var nextTimePontScheduled = netxTimePoint.ToString() + ":00:00";
            var nextTrainId = 0;

            if (UrbanTrainDatabase.StopSchedules.Any(x => x.StopId == stopId && x.ScheduledTime == nextTimePontScheduled))
            {
                 nextTrainId = UrbanTrainDatabase.StopSchedules.Where(x => x.StopId == stopId && x.ScheduledTime == nextTimePontScheduled).First().TrainNO;

                return GetAllTrains().Where(x => x.TrainNO == nextTrainId).First();
            }
            else
            {
                return new TrainModel();
            }


          
        }
        public string GetTrainCurrentLocation(TrainModel trainModel)
        {
           
                if (trainModel.TrainStatus == "Moving")
                {
                    return "Moving towards " + trainModel.NextTrainStop + " From " + trainModel.LastTrainStop;
                }

                else if (trainModel.TrainStatus == "Stopped at stop")
                {

                    return trainModel.LastTrainStop;
                }
                else
                {

                    return "Currently stopped between " + trainModel.NextTrainStop + " and " + trainModel.LastTrainStop;

                }
           

           

        }
        //public string GetLineStatus(int lineStatusId)
        //{

        //    return UrbanTrainDatabase.LineStatus.Where(x=>x.LineStatusId ==lineStatusId).Select(y=>y.CurrentStatus).First();
        //}

    }
}