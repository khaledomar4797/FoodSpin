using System;
using System.Runtime.Serialization;

namespace FoodSpin.Models.Dashboard
{
    [DataContract]
    public class DataPoint
    {
        public DataPoint(string x, double y)
        {
            this.label = x;
            this.Y = y;
        }

        [DataMember(Name = "label")]
        public string label = null;

        [DataMember(Name = "y")]
        public Nullable<double> Y = null;
    }
}
