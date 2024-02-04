using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TechVisionLab1
{
    public class Cluster
    {
        public int centerX { get; set; }
        public int centerY { get; set; }
        public Color color = Color.FromArgb(255, new Random().Next(0,255),
                                                    new Random().Next(0, 255),
                                                    new Random().Next(0, 255));
        public int NumOfCluster { get; set; }
        public int CountPointsInCluster { get; set; }

        public List<int> X;
        public List<int> Y;

        public Cluster(int numOfCluster, int countPoints, List<int> x, List<int> y, int cX, int cY)
        {
            NumOfCluster = numOfCluster;
            CountPointsInCluster = countPoints;
            X = x;
            Y = y;
            centerX = cX;
            centerY = cY;
        }
    }
}
