using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using System.Threading;

public class BeerPanel : System.Windows.Forms.Panel
{
    Font textFont = new Font("Times New Roman", 24);
    Point top;
    Point bottom;
    Rectangle rectangle;
    int height;

    public BeerPanel(int topX, int topY, int height)
    {
        this.DoubleBuffered = true;
        InitializeComponent();
        top = new Point(topX, topY);
        this.height = height;
        bottom = new Point(top.X, top.Y + height);
        rectangle = new Rectangle(bottom.X - 25, bottom.Y - 15, 50, 30);
    }

    private void BeerPanel_Paint(object sender, PaintEventArgs e)
    {
        Paint_Beer_Meter(e.Graphics);
    }
    private void InitializeComponent()
    {
        this.SuspendLayout();
        this.BackColor = System.Drawing.SystemColors.Window;
        this.Paint += new System.Windows.Forms.PaintEventHandler(this.BeerPanel_Paint);
        this.Paint += new System.Windows.Forms.PaintEventHandler(this.BeerPanel_Paint);
        this.MouseDown += new System.Windows.Forms.MouseEventHandler(this.BeerPanel_MouseDown);
        this.MouseMove += new System.Windows.Forms.MouseEventHandler(this.BeerPanel_MouseMove);
        this.MouseUp += new System.Windows.Forms.MouseEventHandler(this.BeerPanel_MouseUp);
        this.ResumeLayout(false);
    }

    private String text = "0 %";
    private void Paint_Beer_Meter(Graphics g)
    {
        g.DrawLine(System.Drawing.Pens.Black, top, bottom);
        g.DrawEllipse(System.Drawing.Pens.Black, rectangle);
        g.DrawString(text, new Font("Arial", 16), new SolidBrush(Color.Black), 100, 100);
    }

    int difference = 0;
    private Boolean mousePressed = false;
    private void BeerPanel_MouseDown(object sender, MouseEventArgs e)
    {
        if (rectangle.Contains(e.X, e.Y))
        {
            difference = e.Y - rectangle.Y;
            mousePressed = true;
        }
    }

    private void BeerPanel_MouseMove(object sender, MouseEventArgs e)
    {
        if (mousePressed)
        {


            rectangle.Y = e.Y - difference;
            if (rectangle.Y + rectangle.Height / 2 < top.Y - (height / 100))
                rectangle.Y = top.Y - 25 - (height / 100);
            if (rectangle.Y + rectangle.Height / 2 > bottom.Y)
                rectangle.Y = bottom.Y - rectangle.Height / 2;
            if ((bottom.Y - rectangle.Y - rectangle.Height / 2) / (height / 100) > 100)
                text = "BEER OVERFLOW!!!";
            else
                text = (bottom.Y - rectangle.Y - rectangle.Height / 2) / (height / 100) + " %";
            this.Refresh();
            Thread.Sleep(5);
        }
    }

    private void BeerPanel_MouseUp(object sender, MouseEventArgs e)
    {
        mousePressed = false;
    }
}
