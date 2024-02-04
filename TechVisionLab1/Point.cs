using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TechVisionLab1
{
    public class DPoint
    {
        public int X { get; set; }
        public int Y { get; set; }
        public int Z { get; set; }
        public bool InCluster = false;
        public Color color;
        
        public DPoint(int x, int y, int z)
        {
            X = x;
            Y = y;
            Z = z;
        }
    }
}
