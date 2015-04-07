using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WorkingOut.Models
{
    public class Exercise 
    {
        public int ID { get; set; }
        public string User { get; set; }
        public string Name { get; set; }
        public string BodyPart { get; set; }
        public string WeightInfo { get; set; }
    }
}