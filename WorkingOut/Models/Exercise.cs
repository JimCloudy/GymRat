using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WorkingOut.Models
{
    public class Exercise
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public string Type { get; set; }
        public string BodyPart { get; set; }
        public string imageUrl { get; set; }
    }
}