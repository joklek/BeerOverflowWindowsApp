using Emgu.CV;
using Emgu.CV.Structure;
using Emgu.CV.CvEnum;
using Emgu.CV.UI;
using Emgu.CV.Util;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Reflection;
using System.Drawing;

namespace BeerOverflowWindowsApp.ImageRecognition
{
    class ImageRater
    {
        List<Rectangle> glasses = new List<Rectangle>();
        List<Rectangle> beers = new List<Rectangle>();
        private int beerPercent = 0;
        private string imageSource;

        Image<Bgr, byte> img;
        public ImageRater(string source)
        {
            imageSource = source;
            img = new Image<Bgr, byte>(imageSource).Resize(400, 400, Inter.Linear, true);   // loading image to memory
            GlassDetector.Detect(img, glasses);
            //  BeerDetector.Detect(img);          - possibly beer detection will go there (but mostly likely not)
            DrawBorders();
            // displayImage()       - to be implemented
            // calculatePercentage()        - to be implemented
        }

        private void DrawBorders()              // draw borders of rectangles
        {
            foreach (Rectangle face in glasses)
                CvInvoke.Rectangle(img, face, new Bgr(Color.Red).MCvScalar, 2);
            foreach (Rectangle eye in beers)
                CvInvoke.Rectangle(img, eye, new Bgr(Color.Blue).MCvScalar, 2);
        }

        public int GetBeerPercentage()                  // returns the fullness percentage of a beer glass
        {
            return beerPercent;
        }
    }
}
