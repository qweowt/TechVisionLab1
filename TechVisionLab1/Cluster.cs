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
        public Color color {  get; set; }
        public int NumOfCluster { get; set; }
        public int CountPointsInCluster { get; set; }
        public List<DPoint> Points { get; set; }

        public Cluster(int numOfCluster, int countPoints, int cX, int cY, List<DPoint> points, Color ccolor)
        {
            NumOfCluster = numOfCluster;
            CountPointsInCluster = countPoints;
            centerX = cX;
            centerY = cY;
            Points = points;
            color = ccolor;
        }

        public void PaintPoint()
        {
            for (int i = 0; i < Points.Count; i++)
            {
                Points[i].color = color;
                Points[i].numCluster = NumOfCluster;
            }
        }
    }
}
