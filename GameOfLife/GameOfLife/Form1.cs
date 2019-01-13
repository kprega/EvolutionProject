using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace GameOfLife
{
    public partial class Form1 : Form
    {
        private Playground playground;
        
        public Form1()
        {
            InitializeComponent();
        }

        private void btn_CreatePlayground_Click(object sender, EventArgs e)
        {
            playground = new Playground(x: 40, y: 40);
            splitContainer1.Refresh();
        }

        private void splitContainer1_Panel1_Paint(object sender, PaintEventArgs e)
        {
            this.DrawPlayground(e);
            this.DrawBacteria(e);
        }

        private void splitContainer1_Panel1_MouseClick(object sender, MouseEventArgs e)
        {
            if (playground != null)
            {
                var row = e.Location.X / playground.CellSize;
                var col = e.Location.Y / playground.CellSize;
                if (row < playground.Width && col < playground.Height)
                {
                    playground.CurrentState[row, col] = !playground.CurrentState[row, col];
                    splitContainer1.Refresh();
                }
            }
        }

        private void DrawPlayground(PaintEventArgs e)
        {
            if (playground != null)
            {
                var rects = new Rectangle[playground.Width * playground.Height];
                for (int h = 0; h < playground.Height; h++)
                {
                    for (int w = 0; w < playground.Width; w++)
                    {
                        var pt = new Point(w * playground.CellSize, h * playground.CellSize);
                        var singleRec = new Rectangle(pt, new Size(playground.CellSize, playground.CellSize));
                        rects[w + h * playground.Width] = singleRec;
                    }
                }
                e.Graphics.DrawRectangles(new Pen(Color.Silver), rects);
            }
        }

        private void DrawBacteria(PaintEventArgs e)
        {
            if (playground != null)
            {
                for (int h = 0; h < playground.Height; h++)
                {
                    for (int w = 0; w < playground.Width; w++)
                    {
                        if (playground.CurrentState[w, h])
                        {
                            var cs = playground.CellSize;
                            var brush = new SolidBrush(Color.Black);
                            var pen = new Pen(brush);
                            var rectangle = new Rectangle(new Point(w * cs, h * cs), new Size(cs, cs));
                            e.Graphics.DrawRectangle(pen, rectangle);
                            e.Graphics.FillRectangle(brush, rectangle);
                        }
                    }
                }
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (playground == null) return;
            var st = new System.Diagnostics.Stopwatch();
            st.Start();
            while(st.Elapsed.Seconds < 10)
            {
                playground.StepForward();
                this.Refresh();
                System.Threading.Thread.Sleep(100);
            }
            st.Stop();
        }
    }
}
