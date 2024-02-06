namespace TechVisionLab1
{
    partial class Form1
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            richTextBox1 = new RichTextBox();
            OpenFile = new Button();
            openFileDialog1 = new OpenFileDialog();
            SaveFile = new Button();
            saveFileDialog1 = new SaveFileDialog();
            CountPointTrackBar = new TrackBar();
            CountPointLabel = new Label();
            PointGeneration = new Button();
            dataGridView1 = new DataGridView();
            pictureBox1 = new PictureBox();
            Draw = new Button();
            DrawPeriod = new Button();
            textBox1 = new TextBox();
            Task4 = new Button();
            Clusters = new Button();
            ClusterSizeTB = new TextBox();
            X = new DataGridViewTextBoxColumn();
            Y = new DataGridViewTextBoxColumn();
            Z = new DataGridViewTextBoxColumn();
            Cluster = new DataGridViewTextBoxColumn();
            ((System.ComponentModel.ISupportInitialize)CountPointTrackBar).BeginInit();
            ((System.ComponentModel.ISupportInitialize)dataGridView1).BeginInit();
            ((System.ComponentModel.ISupportInitialize)pictureBox1).BeginInit();
            SuspendLayout();
            // 
            // richTextBox1
            // 
            richTextBox1.Location = new Point(185, 12);
            richTextBox1.Name = "richTextBox1";
            richTextBox1.ReadOnly = true;
            richTextBox1.Size = new Size(636, 64);
            richTextBox1.TabIndex = 0;
            richTextBox1.Text = "";
            // 
            // OpenFile
            // 
            OpenFile.Location = new Point(12, 12);
            OpenFile.Name = "OpenFile";
            OpenFile.Size = new Size(167, 29);
            OpenFile.TabIndex = 1;
            OpenFile.Text = "Open File";
            OpenFile.UseVisualStyleBackColor = true;
            OpenFile.Click += OpenFile_Click;
            // 
            // openFileDialog1
            // 
            openFileDialog1.FileName = "openFileDialog1";
            // 
            // SaveFile
            // 
            SaveFile.Location = new Point(12, 47);
            SaveFile.Name = "SaveFile";
            SaveFile.Size = new Size(167, 29);
            SaveFile.TabIndex = 2;
            SaveFile.Text = "Save File";
            SaveFile.UseVisualStyleBackColor = true;
            SaveFile.Click += SaveFile_Click;
            // 
            // CountPointTrackBar
            // 
            CountPointTrackBar.AllowDrop = true;
            CountPointTrackBar.Location = new Point(185, 82);
            CountPointTrackBar.Maximum = 1024;
            CountPointTrackBar.Name = "CountPointTrackBar";
            CountPointTrackBar.Size = new Size(613, 56);
            CountPointTrackBar.TabIndex = 4;
            CountPointTrackBar.Scroll += CountPointTrackBar_Scroll;
            // 
            // CountPointLabel
            // 
            CountPointLabel.AutoSize = true;
            CountPointLabel.Location = new Point(789, 91);
            CountPointLabel.Name = "CountPointLabel";
            CountPointLabel.Size = new Size(17, 20);
            CountPointLabel.TabIndex = 5;
            CountPointLabel.Text = "0";
            // 
            // PointGeneration
            // 
            PointGeneration.Location = new Point(12, 82);
            PointGeneration.Name = "PointGeneration";
            PointGeneration.Size = new Size(167, 29);
            PointGeneration.TabIndex = 6;
            PointGeneration.Text = "Point Generation";
            PointGeneration.UseVisualStyleBackColor = true;
            PointGeneration.Click += PointGeneration_Click;
            // 
            // dataGridView1
            // 
            dataGridView1.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridView1.Columns.AddRange(new DataGridViewColumn[] { X, Y, Z, Cluster });
            dataGridView1.Location = new Point(12, 164);
            dataGridView1.Name = "dataGridView1";
            dataGridView1.RowHeadersWidth = 51;
            dataGridView1.Size = new Size(403, 391);
            dataGridView1.TabIndex = 7;
            // 
            // pictureBox1
            // 
            pictureBox1.BorderStyle = BorderStyle.FixedSingle;
            pictureBox1.Location = new Point(421, 155);
            pictureBox1.Name = "pictureBox1";
            pictureBox1.Size = new Size(400, 400);
            pictureBox1.TabIndex = 9;
            pictureBox1.TabStop = false;
            // 
            // Draw
            // 
            Draw.Location = new Point(421, 117);
            Draw.Name = "Draw";
            Draw.Size = new Size(400, 29);
            Draw.TabIndex = 10;
            Draw.Text = "Draw";
            Draw.UseVisualStyleBackColor = true;
            Draw.Click += Draw_Click;
            // 
            // DrawPeriod
            // 
            DrawPeriod.Location = new Point(827, 47);
            DrawPeriod.Name = "DrawPeriod";
            DrawPeriod.Size = new Size(161, 29);
            DrawPeriod.TabIndex = 11;
            DrawPeriod.Text = "Draw Period";
            DrawPeriod.UseVisualStyleBackColor = true;
            DrawPeriod.Click += DrawPeriod_Click;
            // 
            // textBox1
            // 
            textBox1.Location = new Point(827, 14);
            textBox1.Name = "textBox1";
            textBox1.Size = new Size(161, 27);
            textBox1.TabIndex = 12;
            // 
            // Task4
            // 
            Task4.Location = new Point(827, 82);
            Task4.Name = "Task4";
            Task4.Size = new Size(161, 29);
            Task4.TabIndex = 13;
            Task4.Text = "Task4";
            Task4.UseVisualStyleBackColor = true;
            Task4.Click += Task4_Click;
            // 
            // Clusters
            // 
            Clusters.Location = new Point(827, 155);
            Clusters.Name = "Clusters";
            Clusters.Size = new Size(161, 29);
            Clusters.TabIndex = 14;
            Clusters.Text = "Clusters";
            Clusters.UseVisualStyleBackColor = true;
            Clusters.Click += Clusters_Click;
            // 
            // ClusterSizeTB
            // 
            ClusterSizeTB.Location = new Point(827, 119);
            ClusterSizeTB.Name = "ClusterSizeTB";
            ClusterSizeTB.Size = new Size(161, 27);
            ClusterSizeTB.TabIndex = 15;
            // 
            // X
            // 
            X.HeaderText = "X";
            X.MinimumWidth = 6;
            X.Name = "X";
            X.Width = 125;
            // 
            // Y
            // 
            Y.HeaderText = "Y";
            Y.MinimumWidth = 6;
            Y.Name = "Y";
            Y.Width = 125;
            // 
            // Z
            // 
            Z.HeaderText = "Z";
            Z.MinimumWidth = 6;
            Z.Name = "Z";
            Z.Width = 125;
            // 
            // Cluster
            // 
            Cluster.HeaderText = "Cluster";
            Cluster.MinimumWidth = 6;
            Cluster.Name = "Cluster";
            Cluster.SortMode = DataGridViewColumnSortMode.Programmatic;
            Cluster.Width = 125;
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(999, 561);
            Controls.Add(ClusterSizeTB);
            Controls.Add(Clusters);
            Controls.Add(Task4);
            Controls.Add(textBox1);
            Controls.Add(DrawPeriod);
            Controls.Add(Draw);
            Controls.Add(pictureBox1);
            Controls.Add(dataGridView1);
            Controls.Add(PointGeneration);
            Controls.Add(CountPointLabel);
            Controls.Add(CountPointTrackBar);
            Controls.Add(SaveFile);
            Controls.Add(OpenFile);
            Controls.Add(richTextBox1);
            FormBorderStyle = FormBorderStyle.FixedToolWindow;
            Name = "Form1";
            Text = "Lab1Klimenko";
            ((System.ComponentModel.ISupportInitialize)CountPointTrackBar).EndInit();
            ((System.ComponentModel.ISupportInitialize)dataGridView1).EndInit();
            ((System.ComponentModel.ISupportInitialize)pictureBox1).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private RichTextBox richTextBox1;
        private Button OpenFile;
        private OpenFileDialog openFileDialog1;
        private Button SaveFile;
        private SaveFileDialog saveFileDialog1;
        private TrackBar CountPointTrackBar;
        private Label CountPointLabel;
        private Button PointGeneration;
        private DataGridView dataGridView1;
        private PictureBox pictureBox1;
        private Button Draw;
        private Button DrawPeriod;
        private TextBox textBox1;
        private Button Task4;
        private Button Clusters;
        private TextBox ClusterSizeTB;
        private DataGridViewTextBoxColumn X;
        private DataGridViewTextBoxColumn Y;
        private DataGridViewTextBoxColumn Z;
        private DataGridViewTextBoxColumn Cluster;
    }
}
