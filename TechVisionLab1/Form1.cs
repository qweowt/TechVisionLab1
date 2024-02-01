using System.Data;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.Tab;

namespace TechVisionLab1
{
    public partial class Form1 : Form
    {
        

        List<int> PointsX = new List<int>();
        List<int> PointsY = new List<int>();
        List<int> PointsZ = new List<int>();
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
            CountPoints = ints.Length / 3;
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

                richTextBox1.Text += $"{X} {Y} {Z} ";
            }
            DataToList();
        }

        private void Clear()
        {
            richTextBox1.Clear();
            PointsX.Clear();
            PointsY.Clear();
            PointsZ.Clear();
            dataGridView1.Rows.Clear();
            pictureBox1.Image = null;
        }

        private void DataToList()
        {
            string DataStr = richTextBox1.Text;
            DataStr = DataStr.Remove(DataStr.Length - 1);
            int[] ints = Array.ConvertAll(DataStr.Split(), int.Parse);
            int helpI = 1;
            for (int i = 0; i < ints.Length; i++)
            {
                if (helpI == 1)
                {
                    PointsX.Add(ints[i]);
                    helpI++;
                }
                else if (helpI == 2)
                {
                    PointsY.Add(ints[i]);
                    helpI++;
                }
                else if (helpI == 3)
                {
                    PointsZ.Add(ints[i] / 4);
                    helpI = 1;
                }

            }
        }

        private void DataGridShow(object sender, EventArgs e)
        {
            dataGridView1.ReadOnly = true;

            dataGridView1.AllowUserToAddRows = false;

            for (int i = 0; i < CountPoints; i++)
            {
                dataGridView1.Rows.Add(PointsX[i], PointsY[i]);
            }

            for (int i = 0; i < CountPoints; i++)
            {
                dataGridView1.Rows[i].Cells[2].Style.BackColor = Color.FromArgb(255, 0, 0, PointsZ[i]);
            }
        }

        private void Draw_Click(object sender, EventArgs e)
        {
            pictureBox1.Image = new Bitmap(pictureBox1.Width, pictureBox1.Height);
            Graphics g = Graphics.FromImage(pictureBox1.Image);
            for (int i = 0; i < CountPoints; i++)
            {
                Color PointColor = Color.FromArgb(255, 0, PointsZ[i], 0);
                Brush brush = new SolidBrush(PointColor);
                g.FillEllipse(brush, (int)(PointsX[i] / 2.56), (int)(PointsY[i] / 2.56), 3, 3);
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
                    Color PointColor = Color.FromArgb(255, (int)(PointsX[i]/4), (int)(PointsY[i]/4), PointsZ[i]);
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
                sum1 += PointsX[i] * PointsY[i];
                sum2 += PointsX[i];
                sum3 += PointsY[i];
                sum4 += Math.Pow(PointsX[i], 2);
            }
            double A = ((CountPoints * sum1 - sum2) / (sum4 - Math.Pow(sum2, 2)));
            double B = ((sum3 - A * sum2) / CountPoints);

            pictureBox1.Image = new Bitmap(pictureBox1.Width, pictureBox1.Height);
            Graphics g = Graphics.FromImage(pictureBox1.Image);

            for (int i = 0; i < CountPoints; i++)
            {
                double y = (A * PointsX[i] + B);
                Brush brush1 = new SolidBrush(Color.Red);
                g.FillEllipse(brush1, (int)(PointsX[i] / 2.56), (int)(y / 2.56), 5, 5);

                Color PointColor = Color.FromArgb(255, 0, PointsZ[i], 0);
                Brush brush = new SolidBrush(PointColor);
                g.FillEllipse(brush, (int)(PointsX[i] / 2.56), (int)(PointsY[i] / 2.56), 3, 3);
            }
        }

        private void Clusters_Click(object sender, EventArgs e)
        {
            List<int> Clusters = new List<int>();
            List<int> cordX = new List<int>();
            List<int> cordY = new List<int>();
            for (int i = 0; i < CountPoints; i++)
            {
                int PointsInCluster = 0;
                for (int j = 0; j < CountPoints; j++)
                {
                    double x = Math.Sqrt(Math.Pow(PointsX[j] - PointsX[i], 2) + Math.Pow(PointsY[j] - PointsY[i], 2));

                    if (x <= 45)
                    {
                        PointsInCluster++;
                    }  
                }
                if (PointsInCluster > CountPoints / 85.3)
                {
                    Clusters.Add(PointsInCluster);
                    cordX.Add(PointsX[i]);
                    cordY.Add(PointsY[i]);
                }
            }
            pictureBox1.Image = new Bitmap(pictureBox1.Width, pictureBox1.Height);
            Graphics g = Graphics.FromImage(pictureBox1.Image);

            for (int i = 0; i < CountPoints; i++)
            {
                Color PointColor = Color.FromArgb(255, 0, PointsZ[i], 0);
                Brush brush = new SolidBrush(PointColor);
                g.FillEllipse(brush, (int)(PointsX[i] / 2.56), (int)(PointsY[i] / 2.56), 5, 5);
            }

            for (int i = 0;i < Clusters.Count;i++)
            {
                Color PointColor = Color.Red;
                Pen pen = new Pen(PointColor);
                g.DrawEllipse(pen, (int)(cordX[i]/2.56-20), (int)(cordY[i] / 2.56 - 20), 45, 45);
            }
        }
    }
}
