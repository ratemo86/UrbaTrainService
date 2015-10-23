using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using UrbanTrainClient.Models;

namespace UrbanTrainClient.Controllers
{
    public class HomeController : Controller
    {
      List<RootObject> jsonObject;

        public ActionResult Index()
        {
            //var content = GetLines();
            //return Json(content, JsonRequestBehavior.AllowGet);
            return View();
        }

        [HttpPost]
        public ActionResult Lines()
        {

            
          
           var content = GetLines();
            return Json(content, JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        public ActionResult GetLine(string href)
        {



            var line = GetLines().Where(x => x.Href == href).First();
            return Json(line, JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        public ActionResult GetStop(string href)
        {
            var stop = GetLines().Where(x => x.LineStops.First().Href == href).First().LineStops.Select(y => new
            {
                StopId = y.StopID,
                Href = y.Href,
                NextTrain = y.NextTrain,
                StopName = y.StopName

            }).First();

            return Json(stop, JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        public ActionResult GetTrain(string href)
        {


            var train = GetLines().Where(x => x.LineTrains.First().Href == href).First().LineTrains.Select(y => new
            {
                TrainNO = y.TrainNO,
                Href = y.Href,
                TrainCurrentLocation = y.CurrentLocation
            }).First();

            return Json(train, JsonRequestBehavior.AllowGet);
        }
        [NonAction]

        //public string GetLines()
        //{
        //    string uri = "http://localhost:60013";
        //    using (HttpClient httpClient = new HttpClient())
        //    {

        //        //return httpClient.GetStringAsync(uri).Result;
        //        return httpClient.GetStringAsync(uri).Result;
        //    }
        //}

    
        public  List<LineRep> GetLines()
        {
            string uri = "http://localhost:60013";

            //LineRep line = new LineRep();
            List<LineRep> lineReps = new List<LineRep>();

            using (HttpClient httpClient = new HttpClient())
            {
                var response = httpClient.GetStringAsync(uri).Result;

                jsonObject = JsonConvert.DeserializeObject<List<RootObject>>(response);

                foreach (var line in jsonObject)
                {
                    var lineRep = new LineRep();
                    




                    lineRep.LineId = line.LineId;
                    lineRep.LineName= line.LineName;
                    lineRep.LineStatus = line.LineStatus;
                    lineRep.Href = line.LineLink.Href;

                    lineRep.LineStops = new List<StopRep>();
                    lineRep.LineTrains = new List<UrbanTrainClient.Models.TrainRep>();

                    foreach (var stop in line.Stops)
                    {
                        lineRep.LineStops.Add(new StopRep
                        {

                            StopID = stop.StopId,
                            StopName = stop.StopName,
                            NextTrain = stop.NextTrain.TrainNO,
                            Href = stop.MainStopLink.Href
                        });
                    }

                    foreach (var train in line.Trains)
                    {
                        lineRep.LineTrains.Add(new Models.TrainRep
                        {
                            TrainNO = train.TrainNO,
                            CurrentLocation = train.CurrentLocation,
                            Href = train.MainTrainLink.Href



                        });
                    }

                    lineReps.Add(lineRep);
                 }

            
                

               
                
            }


            return lineReps;
        }


    }//HomeController
}