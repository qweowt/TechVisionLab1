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
            richTextBox1.Text = fileText;

            FindCountPoints();

            DataToList();
        }

        private void FindCountPoints()
        {
            string DataStr = richTextBox1.Text;
            DataStr = DataStr.Remove(DataStr.Length - 1);
            int[] ints = Array.ConvertAll(DataStr.Split(), int.Parse);
            CountPoints = ints.Length/3;
        }

        private void SaveFile_Click(object sender, EventArgs e)
        {
            if (saveFileDialog1.ShowDialog() == DialogResult.Cancel)
                return;
            string filename = saveFileDialog1.FileName;
            System.IO.File.WriteAllText(filename, richTextBox1.Text);
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
                richTextBox1.Text += $"{Points[i].X} {Points[i].Y} {Points[i].Z} ";
            }
        }

        private void Clear()
        {
            richTextBox1.Clear();
            Points.Clear();
            dataGridView1.Rows.Clear();
            pictureBox1.Image = null;
        }

        private void DataToList()
        {
            string DataStr = richTextBox1.Text;
            DataStr = DataStr.Remove(DataStr.Length - 1);
            int[] ints = Array.ConvertAll(DataStr.Split(), int.Parse);
            for (int i = 0; i < ints.Length; i+=3)
            {
                Points.Add(new DPoint(ints[i], ints[i+1], ints[i+2]));
            }
        }

        private void DataGridShow()
        {
            dataGridView1.ReadOnly = true;
            dataGridView1.AllowUserToAddRows = false;

            for (int i = 0; i < CountPoints; i++)
                dataGridView1.Rows.Add(Points[i].X, Points[i].Y);

            for (int i = 0; i < CountPoints; i++)
                dataGridView1.Rows[i].Cells[2].Style.BackColor = Color.FromArgb(255, 0, 0, Points[i].Z/4);
        }

        private void Draw_Click(object sender, EventArgs e)
        {
            pictureBox1.Image = new Bitmap(pictureBox1.Width, pictureBox1.Height);
            Graphics g = Graphics.FromImage(pictureBox1.Image);
            for (int i = 0; i < CountPoints; i++)
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
            int cordX = 1;
            int period = int.Parse(textBox1.Text);
            for (int i = 0; i < CountPoints; i++)
            {
                if (i % period == 0)
                {
                    cordY += 21;
                    cordX = 1;
                }
                     
                if (i % period < period)
                {
                    Color PointColor = Color.FromArgb(255, (int)(Points[i].X /4), (int)(Points[i].Y /4), (int)(Points[i].Z / 4));
                    Brush brush = new SolidBrush(PointColor);
                    g.FillRectangle(brush, cordX, cordY, 20, 20);
                    cordX += 21;
                }
            }
        }

        private void Task4_Click(object sender, EventArgs e)
        {
            double sum1 = 0, sum2 = 0, sum3 = 0;
            double sum4 = 0;

            for (int i = 0; i < CountPoints; i++)
            {
                sum1 += Points[i].X * Points[i].Y;
                sum2 += Points[i].X;
                sum3 += Points[i].Y;
                sum4 += Math.Pow(Points[i].X, 2);
            }
            double A = ((CountPoints * sum1 - sum2) / (sum4 - Math.Pow(sum2, 2)));
            double B = ((sum3 - A * sum2) / CountPoints);

            pictureBox1.Image = new Bitmap(pictureBox1.Width, pictureBox1.Height);
            Graphics g = Graphics.FromImage(pictureBox1.Image);

            for (int i = 0; i < CountPoints; i++)
            {
                double y = (A * Points[i].X + B);
                Brush brush1 = new SolidBrush(Color.Red);
                g.FillEllipse(brush1, (int)(Points[i].X / 2.56), (int)(y / 2.56), 5, 5);

                Brush brush = new SolidBrush(Color.FromArgb(255, 0, 0, (int)(Points[i].Z/4)));
                g.FillEllipse(brush, (int)(Points[i].X / 2.56), (int)(Points[i].Y / 2.56), 5, 5);
            }
        }

        private void Clusters_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < CountPoints; i++)
            {
                Points[i].numCluster = 0;
                Points[i].InCluster = false;
            }
            List<Cluster> clusters = new List<Cluster>();
            int ClusterSize = int.Parse(ClusterSizeTB.Text);
            for (int i = 0; i < CountPoints; i++)
            {
                List<DPoint> ClusterPoints = new List<DPoint>();
                for (int j = i+1; j < CountPoints; j++)
                {
                    double L = Math.Sqrt(Math.Pow(Points[j].X - Points[i].X, 2) + Math.Pow(Points[j].Y - Points[i].Y, 2));

                    if (L <= ClusterSize && Points[i].InCluster == false && Points[j].InCluster == false)
                    {
                        //PointsInCluster++;
                        Points[j].InCluster = true;
                        ClusterPoints.Add(Points[j]);
                    }
                }

                if (ClusterPoints.Count > ClusterSize/5)
                {
                    clusters.Add(new Cluster(i, CountPoints, (int)(Points[i].X), (int)(Points[i].Y), ClusterPoints, Color.FromArgb(255, new Random().Next(255), new Random().Next(255), new Random().Next(255))));
                    Points[i].InCluster = true;
                }
                
            }

            for(int i = 0; i < clusters.Count; i++)
            {
                clusters[i].PaintPoint();
            }
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
