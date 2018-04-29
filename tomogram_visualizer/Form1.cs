using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace tomogram_visualizer
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        Bin bin = new Bin();
        View view = new View();
        int currentLayer;
        bool loaded = false;
        bool needReload = false;
        int FrameCount;
        public static int minBar;
        public static int widthBar;
        DateTime NextFPSUpdate = DateTime.Now.AddSeconds(1);

        private void открытьToolStripMenuItem_Click(object sender, EventArgs e)
        {
            OpenFileDialog dialog = new OpenFileDialog();
            if (dialog.ShowDialog() == DialogResult.OK)
            {
                string str = dialog.FileName;
                bin.readBIN(str);
                view.SetupView(glControl1.Width, glControl1.Height);
                loaded = true;
                glControl1.Invalidate();
                minBar = trackBar2.Value;
                widthBar = trackBar3.Value;
            }
        }

        void Application_Idle(object sender, EventArgs e)
        {
            while (glControl1.IsIdle)
            {
                displayFPS();
                glControl1.Invalidate();
            }
        }

        void displayFPS()
        {
            if (DateTime.Now >= NextFPSUpdate)
            {
                this.Text = String.Format("CT Visualiser (fps={0})", FrameCount);
                NextFPSUpdate = DateTime.Now.AddSeconds(1);
                FrameCount = 0;
            }
            FrameCount++;
        }

        private void glControl1_Paint(object sender, EventArgs e)
        {
            if (loaded)
            {
                if (needReload)
                {
                    view.generateTextureImage(currentLayer);
                    view.Load2DTexture();
                    needReload = false;
                }
                if (radioButton1.Checked) view.DrawQuads(currentLayer);
                if (radioButton2.Checked) view.DrawTexture();

                glControl1.SwapBuffers();
            }
        }

        private void trackBar1_Scroll(object sender, EventArgs e)
        {
            currentLayer = trackBar1.Value;
            glControl1_Paint(sender, e);
            needReload = true;
        }

        private void menuStrip1_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {

        }

        private void Form1_Load(object sender, EventArgs e)
        {
            Application.Idle += Application_Idle;
        }

        private void trackBar2_Scroll(object sender, EventArgs e)
        {
            minBar = trackBar2.Value;
            glControl1_Paint(sender, e);
            needReload = true;
        }

        private void trackBar3_Scroll(object sender, EventArgs e)
        {
            widthBar = trackBar3.Value;
            glControl1_Paint(sender, e);
            needReload = true;
        }
    }
}
