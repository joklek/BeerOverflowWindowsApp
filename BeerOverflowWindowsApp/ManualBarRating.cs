using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace BeerOverflowWindowsApp
{
    public partial class ManualBarRating : Control
    {
        List<Rectangle> beerGlassList;
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
        public int ImageSize{get;set;}
        //int ImageSize;

        public ManualBarRating()
        {
            this.DoubleBuffered = true;
            InitializeComponent();
            this.ImageSize = 100;
            this.MinimumSize = new Size(ImageSize * 6, ImageSize);
            this.beerGlassList = new List<Rectangle>(6);
            for(int i = 0; i < 6; i++)
            {
                beerGlassList.Add(new Rectangle(i * ImageSize, 0, ImageSize, ImageSize));
            }
            Refresh();
        }

        protected override void OnPaint(PaintEventArgs pe)
        {

            Graphics g = pe.Graphics;
            g.DrawRectangle(Pens.Black,this.ClientRectangle);
            var image = ResizeImage(Properties.Resources.BeerGlass, 100, 100);
            for (int i = 1; i <= numberOfGlasses; i++)
            {
                g.DrawImageUnscaledAndClipped(image, beerGlassList[i]);
            }
            for(int i = numberOfGlasses + 1; i < 6; i++)
            {
                g.DrawEllipse(Pens.Aqua, beerGlassList[i]);
            }
        }

        private int numberOfGlasses = 0;
        private void ManualBarRating_MouseClick(object sender, MouseEventArgs e)
        {
            if(e.Button == MouseButtons.Left)
                for(int i = 5; i >= 0; i--)
                {
                    if(beerGlassList[i].Contains(e.Location))
                    {
                        numberOfGlasses = i;
                        Refresh();
                        break;
                    }
                }
        }

        public static Bitmap ResizeImage(Image image, int width, int height)
        {
            var destRect = new Rectangle(0, 0, width, height);
            var destImage = new Bitmap(width, height);

            destImage.SetResolution(image.HorizontalResolution, image.VerticalResolution);

            using (var graphics = Graphics.FromImage(destImage))
            {
                graphics.CompositingMode = CompositingMode.SourceCopy;
                graphics.CompositingQuality = CompositingQuality.HighQuality;
                graphics.InterpolationMode = InterpolationMode.HighQualityBicubic;
                graphics.SmoothingMode = SmoothingMode.HighQuality;
                graphics.PixelOffsetMode = PixelOffsetMode.HighQuality;

                using (var wrapMode = new ImageAttributes())
                {
                    wrapMode.SetWrapMode(WrapMode.TileFlipXY);
                    graphics.DrawImage(image, destRect, 0, 0, image.Width, image.Height, GraphicsUnit.Pixel, wrapMode);
                }
            }

            return destImage;
        }
    }
}
