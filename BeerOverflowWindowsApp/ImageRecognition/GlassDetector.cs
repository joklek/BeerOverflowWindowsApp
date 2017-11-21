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
using System.Drawing;

namespace BeerOverflowWindowsApp.ImageRecognition
{
    public static class GlassDetector
    {
        public static void Detect(Image<Bgr, byte> image, List<Rectangle> glasses)
        {
            using (CascadeClassifier glass = new CascadeClassifier("haarcascade_beerglass.xml"))
            {
                using (UMat ugray = new UMat())
                {
                    CvInvoke.CvtColor(image, ugray, Emgu.CV.CvEnum.ColorConversion.Bgr2Gray);

                    //normalizes brightness and increases contrast of the image
                    CvInvoke.EqualizeHist(ugray, ugray);

                    //Detect the glasses  from the gray scale image and store the locations as rectangle
                    //The first dimensional is the channel
                    //The second dimension is the index of the rectangle in the specific channel                     
                    Rectangle[] glassesDetected = glass.DetectMultiScale(
                       ugray,
                       1.1,
                       10,
                       new Size(20, 20));

                    glasses.AddRange(glassesDetected);

                    /* 
                     * to be implemented - beer detection
                     * 
                    foreach (Rectangle f in glassesDetected)
                    {
                        //Get the region of interest on the faces
                        using (UMat glassRegion = new UMat(ugray, f))
                        {
                               // beer detection code will go there
                        }
                    }
                    */
                }
            }
        }
    }
}
