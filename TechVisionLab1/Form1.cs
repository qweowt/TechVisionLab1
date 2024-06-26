using System.ComponentModel;
using System.Data;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.Tab;

namespace TechVisionLab1
{
    public partial class Form1 : Form
    {
        
        List<DPoint> Points = new List<DPoint>();
        int CountPoints = 0;
        string data = "";
        public Form1()
        {
            InitializeComponent();
        }
        private void OpenFile_Click(object sender, EventArgs e)
        {
            Clear();
            if (openFileDialog1.ShowDialog() == DialogResult.Cancel)
                return;
            string filename = openFileDialog1.FileName;
            string fileText = System.IO.File.ReadAllText(filename);
            data = fileText;
            DataToList(data);
        }

        private void SaveFile_Click(object sender, EventArgs e)
        {
            if (saveFileDialog1.ShowDialog() == DialogResult.Cancel)
                return;
            string filename = saveFileDialog1.FileName;
            System.IO.File.WriteAllText(filename, data);
        }

        private void CountPointTrackBar_Scroll(object sender, EventArgs e)
        {
            CountPointLabel.Text = (CountPointTrackBar.Value).ToString();
            CountPoints = CountPointTrackBar.Value;
        }

        private void PointGeneration_Click(object sender, EventArgs e)
        {
            Clear();
            int X, Y, Z;
            Random random = new Random();
            for (int i = 0; i < CountPoints; i++)
            {
                X = random.Next(1023);
                Y = random.Next(1023);
                Z = random.Next(1023);
                Points.Add(new DPoint(X,Y,Z));
                data += $"{Points[i].X} {Points[i].Y} {Points[i].Z} ";
            }
        }

        private void Clear()
        {
            data = "";
            Points.Clear();
            
            dataGridView1.Rows.Clear();
            pictureBox1.Image = null;
        }

        private void DataToList(string DataStr)
        {            
            DataStr = DataStr.Remove(DataStr.Length - 1);
            int[] ints = Array.ConvertAll(DataStr.Split(), int.Parse);
            CountPoints = ints.Length / 3;
            for (int i = 0; i < ints.Length; i+=3)
            {
                Points.Add(new DPoint(ints[i], ints[i+1], ints[i+2]));
            }
        }

        private void DataGridShow()
        {
            dataGridView1.Rows.Clear ();
            dataGridView1.ReadOnly = true;
            dataGridView1.AllowUserToAddRows = false;

            for (int i = 0; i < CountPoints; i++)
                dataGridView1.Rows.Add(Points[i].X, Points[i].Y, Points[i].Z);
        }

        private void Draw_Click(object sender, EventArgs e)
        {
            pictureBox1.Image = new Bitmap(pictureBox1.Width, pictureBox1.Height);
            Graphics g = Graphics.FromImage(pictureBox1.Image);
            for (int i = 0; i < Points.Count; i++)
            {   
                Brush brush = new SolidBrush(Color.FromArgb(255, 0, 0, (int)(Points[i].Z / 4)));
                g.FillEllipse(brush, (int)(Points[i].X / 2.56), (int)(Points[i].Y / 2.56), 3, 3);
            }
            DataGridShow();
        }

        private void DrawPeriod_Click(object sender, EventArgs e)
        {
            pictureBox1.Image = new Bitmap(pictureBox1.Width, pictureBox1.Height);
            Graphics g = Graphics.FromImage(pictureBox1.Image);
            int cordY = -20;
            int cordX = 0;
            int period = 0;
            try
            {
                period = int.Parse(textBox1.Text);
            }
            catch
            {
                MessageBox.Show("������� ������");
                return;
            }
            for (int i = 0; i < Points.Count; i++)
            {
                if (i % period == 0)
                {
                    cordY += 20;
                    cordX = 0;
                }
                     
                Color PointColor = Color.FromArgb(255, 0, (int)(Points[i].Y / 4)*2, 0);
                Brush brush = new SolidBrush(PointColor);
                g.FillRectangle(brush, cordX, cordY, 20, 20);
                cordX += 20;
            }
        }

        private void Task4_Click(object sender, EventArgs e)
        {
            double sum1 = 0;
            double sum2 = 0;
            double sum3 = 0;
            double sum4 = 0;
            double sum5 = 0;

            for (int i = 0; i < Points.Count; i++)
            {
                sum1 += Points[i].X * Points[i].Y;
                sum2 += Points[i].X;
                sum3 += Points[i].Y;
                sum4 += Math.Pow(Points[i].X, 2);
            }

            double B = (sum1 * sum2 - sum3 * sum4)/(sum4 - Points.Count * sum4);
            for (int i = 0; i < Points.Count; i++)
                sum5 += Points[i].Y - B;
            double A = sum5 / sum2;

            pictureBox1.Image = new Bitmap(pictureBox1.Width, pictureBox1.Height);
            Graphics g = Graphics.FromImage(pictureBox1.Image);

            for (int i = 0; i < Points.Count; i++)
            {
                Brush brush = new SolidBrush(Color.FromArgb(255, 0, 0, (int)(Points[i].Z/4)));
                g.FillEllipse(brush, (int)(Points[i].X / 2.56), (int)(Points[i].Y / 2.56), 5, 5);
            }
            int y0 = (int)((A * 0 + B) / 2.56);
            int y = (int)((A * 1023 + B) / 2.56);
            int x0 = 0;
            int x = (int)(1023 / 2.56);
            g.DrawLine(new Pen(Color.Red), x0, y0, x, y);

        }

        private void Clusters_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < Points.Count; i++)
            {
                Points[i].numCluster = 0;
                Points[i].InCluster = false;
                Points[i].color = Color.Gray;
            }
            List<Cluster> clusters = new List<Cluster>();
            int? ClusterSize;
            int? PointsInCluster;
            try
            {
                ClusterSize = int.Parse(ClusterSizeTB.Text);
                PointsInCluster = int.Parse(CountPointsInCluster.Text);
            }
            catch{MessageBox.Show("������� ������ ���������");return;}
            for (int i = 0; i < Points.Count; i++)
            {
                List<DPoint> ClusterPoints = new List<DPoint>();
                for (int j = 0; j < Points.Count; j++)
                {
                    double L = Math.Sqrt(Math.Pow(Points[j].X - Points[i].X, 2) + Math.Pow(Points[j].Y - Points[i].Y, 2));

                    if (L <= ClusterSize && Points[i].InCluster == false && Points[j].InCluster == false)
                    {
                        if (Points[i] != Points[j])
                        {
                            Points[j].InCluster = true;
                            ClusterPoints.Add(Points[j]);
                        }
                    }
                }

                if (ClusterPoints.Count >= PointsInCluster)
                {
                    Points[i].InCluster = true;
                    ClusterPoints.Add(Points[i]);
                    clusters.Add(new Cluster(i, ClusterPoints.Count, (int)(Points[i].X), (int)(Points[i].Y), ClusterPoints, 
                        Color.FromArgb(255, new Random().Next(255), new Random().Next(255), new Random().Next(255))));
                }
                else
                {
                    for (int j = 0;j < ClusterPoints.Count; j++)
                        ClusterPoints[j].InCluster = false;
                }
                
            }
            while (PointsInCluster > 0)
            {
                for (int i = 0; i < Points.Count; i++)
                {
                    List<DPoint> ClusterPoints = new List<DPoint>();
                    for (int j = 0; i < Points.Count; i++)
                    {
                        double L = Math.Sqrt(Math.Pow(Points[j].X - Points[i].X, 2) + Math.Pow(Points[j].Y - Points[i].Y, 2));

                        if (L <= ClusterSize && Points[i].InCluster == false && Points[j].InCluster == false)
                        {
                            if (Points[i] != Points[j])
                            {
                                Points[j].InCluster = true;
                                ClusterPoints.Add(Points[j]);
                            }
                        }
                    }

                    if (ClusterPoints.Count >= PointsInCluster)
                    {
                        Points[i].InCluster = true;
                        ClusterPoints.Add(Points[i]);
                        clusters.Add(new Cluster(i, ClusterPoints.Count, (int)(Points[i].X), (int)(Points[i].Y), ClusterPoints,
                            Color.FromArgb(255, new Random().Next(255), new Random().Next(255), new Random().Next(255))));
                    }
                    else
                    {
                        for (int j = 0; j < ClusterPoints.Count; j++)
                            ClusterPoints[j].InCluster = false;
                    }
                }
                PointsInCluster--;
            }

            for (int i = 0; i < clusters.Count; i++)
                clusters[i].PaintPoint();
            pictureBox1.Image = new Bitmap(pictureBox1.Width, pictureBox1.Height);
            Graphics g = Graphics.FromImage(pictureBox1.Image);
            

            for (int i = 0;i < Points.Count; i++)
            {
                Brush brush = new SolidBrush(Points[i].color);
                g.FillEllipse(brush, (int)(Points[i].X / 2.56), (int)(Points[i].Y / 2.56), 5, 5);
            }

            for (int i = 0; i < clusters.Count; i++) 
            {
                Pen pen = new Pen(Color.Red);
                g.DrawEllipse(pen, (int)(clusters[i].centerX / 2.56 - ClusterSize / 2),
                                   (int)(clusters[i].centerY / 2.56 - ClusterSize / 2), 
                                   (int)(ClusterSize), (int)(ClusterSize));
            }
            DataGridShow();
            DataGridShowPlus();
        }

        private void DataGridShowPlus()
        {

            for (int i = 0; i < Points.Count ; i++)
                dataGridView1.Rows[i].Cells[3].Value = Points[i].numCluster;
            dataGridView1.Sort(dataGridView1.Columns[3], ListSortDirection.Descending);
        }
    }
}
