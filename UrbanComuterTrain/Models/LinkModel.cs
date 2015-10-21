using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace UrbanComuterTrain.Models
{
    public class LinkModel
    {
        public int LinkId { get; set; }
        public string Rel { get; set; }
        public string Href { get; set; }
        public string Method { get; set; }
    }
}