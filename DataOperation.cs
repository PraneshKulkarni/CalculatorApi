using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CalculatorAPI
{
    public class DataOperation
    {
        public int Id { get; set; }

        public int Input1 { get; set; }

        public int Input2 { get; set; }

        public Double Result { get; set; }

        public string Perform { get; set; }

        public bool Delete { get; set; }

        public DateTime dateTime { get; set; }
    }
}
