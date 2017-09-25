using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using System.Threading;

namespace BeerOverflowWindowsApp
{
    public class BeerPanel : Panel
    {
        Font textFont = new Font("Times New Roman", 24);
        private Point top;
        private Point bottom;
        private Rectangle marker;
        private int height;
        private static string rating;
        public static string Rating
        {
            get { return rating; }
        }
        private const int markerHeight = 30;
        private const int markerWidth = 50;

        public BeerPanel(int topX, int topY, int height)
        {
            this.DoubleBuffered = true;
            InitializeComponent();
            top = new Point(topX, topY);
            this.height = height;
            bottom = new Point(top.X, top.Y + height);
            marker = new Rectangle(bottom.X - markerWidth / 2, bottom.Y - markerHeight / 2, markerWidth, markerHeight);
        }

        private void BeerPanel_Paint(object sender, PaintEventArgs e)
        {
            Paint_Beer_Meter(e.Graphics);
        }
        private void InitializeComponent()
        {
            this.SuspendLayout();
            // 
            // BeerPanel
            // 
            this.BackColor = SystemColors.Window;
            this.Paint += new PaintEventHandler(this.BeerPanel_Paint);
            this.MouseDown += new MouseEventHandler(this.BeerPanel_MouseDown);
            this.MouseMove += new MouseEventHandler(this.BeerPanel_MouseMove);
            this.MouseUp += new MouseEventHandler(this.BeerPanel_MouseUp);
            this.ResumeLayout(false);
        }

        private String text = "0 %";
        private void Paint_Beer_Meter(Graphics g)
        {
            g.DrawLine(Pens.Black, top, bottom);
            g.DrawEllipse(Pens.Black, marker);
            g.DrawString(text, new Font("Arial", 16), new SolidBrush(Color.Black), top.X - 30 , top.Y - 45);
        }

        int difference = 0;
        private Boolean mousePressed = false;
        private void BeerPanel_MouseDown(object sender, MouseEventArgs e)
        {
            if (marker.Contains(e.X, e.Y))
            {
                difference = e.Y - marker.Y;
                mousePressed = true;
            }
        }

        private void BeerPanel_MouseMove(object sender, MouseEventArgs e)
        {
            if (mousePressed)
            {
                marker.Y = e.Y - difference;
                if (marker.Y + marker.Height / 2 < top.Y)
                    marker.Y = top.Y - marker.Height / 2;
                if (marker.Y + marker.Height / 2 > bottom.Y)
                    marker.Y = bottom.Y - marker.Height / 2;
                rating = (bottom.Y - marker.Y - marker.Height / 2) / (height / 100) + "";
                text = rating + " %";
                this.Refresh();
                Thread.Sleep(5);
            }
        }

        private void BeerPanel_MouseUp(object sender, MouseEventArgs e)
        {
            mousePressed = false;
        }
    }
}
