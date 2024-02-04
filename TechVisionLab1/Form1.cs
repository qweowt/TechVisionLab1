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
                Points[i].X = ints[i];
                Points[i].Y = ints[i+1];
                Points[i].Z = ints[i+2];
            }
        }

        private void DataGridShow(object sender, EventArgs e)
        {
            dataGridView1.ReadOnly = true;

            dataGridView1.AllowUserToAddRows = false;

            for (int i = 0; i < CountPoints; i++)
            {
                dataGridView1.Rows.Add(Points[i].X, Points[i].Y);
            }

            for (int i = 0; i < CountPoints; i++)
            {
                dataGridView1.Rows[i].Cells[2].Style.BackColor = Color.FromArgb(255, 0, 0, Points[i].Z/4);
            }
        }

        private void Draw_Click(object sender, EventArgs e)
        {
            pictureBox1.Image = new Bitmap(pictureBox1.Width, pictureBox1.Height);
            Graphics g = Graphics.FromImage(pictureBox1.Image);
            for (int i = 0; i < CountPoints; i++)
            {
                Color PointColor = Color.FromArgb(255, 0, Points[i].Z/4, 0);
                Brush brush = new SolidBrush(PointColor);
                g.FillEllipse(brush, (int)(Points[i].X / 2.56), (int)(Points[i].Y / 2.56), 3, 3);
            }
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
                    g.FillRectangle(brush, cordX/*(int)(PointsX[] / 2.56)*/, cordY, 20, 20);
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

                Color PointColor = Color.FromArgb(255, 0, Points[i].Z / 4, 0);
                Brush brush = new SolidBrush(PointColor);
                g.FillEllipse(brush, (int)(Points[i].X / 2.56), (int)(Points[i].Y / 2.56), 5, 5);
            }
        }

        private void Clusters_Click(object sender, EventArgs e)
        {
            List<Cluster> clusters = new List<Cluster>();
            int PointsInCluster = 0;
            for (int i = 0; i < CountPoints; i++)
            {
                List<int> x = new List<int>();
                List<int> y = new List<int>();
                for (int j = 0; j < CountPoints; j++)
                {
                    double L = Math.Sqrt(Math.Pow(Points[j].X - Points[i].X, 2) + Math.Pow(Points[j].Y - Points[i].Y, 2));

                    if (L <= 45)
                    {
                        PointsInCluster++;
                        x.Add(Points[j].X);
                        y.Add(Points[j].Y);
                    }
                }

                if (PointsInCluster > CountPoints / 85.3)
                {
                    clusters.Add(new Cluster(i, CountPoints, x, y, Points[i].X, Points[i].Y));
                }
            }


        }
    }
}
