using System;
using System.Collections.Generic;

namespace EfSqliteUwpDemo.Models
{
    public class Sensor
    {

        public Guid SensorId { get; set; }

        public string Location { get; set; }

        public List<Ambience> CurrentAmbientData { get; set; }
    }
}
